using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Flurl.Http;
using Flurl.Util;
using ParserForUniversity.Interfaces;
using ParserForUniversity.Models;

namespace ParserForUniversity.Services
{
    public class ParserApi : IParser
    {
        public async Task<IEnumerable<ParsedComments>> ParseAsync(string urlToPost)
        {
            var postNumber = Regex.Match(urlToPost, @"\/([0-9]+)").Groups[1];
            var url = $"https://m.habr.com/kek/v2/articles/{postNumber}/comments/?fl=ru&hl=ru";

            var test2 = await url.GetJsonAsync();

            var comm = ((object) test2).ToKeyValuePairs()
                .Single(x => x.Key == "comments").Value.ToKeyValuePairs().ToArray();

            var result = new List<ParsedComments>();

            foreach (var obj in comm.Select(x => x.Value).Select(x => x.ToKeyValuePairs()).ToArray())
            {
                var valueTuples = obj as (string Key, object Value)[] ?? obj.ToArray();

                var user = new ParsedUser();
                try
                {
                    var author = valueTuples.Single(o => o.Key == "author").Value?.ToKeyValuePairs()?.ToArray();

                    user = new ParsedUser
                    {
                        Alias = (string) author.Single(o => o.Key == "alias").Value,
                        Id = int.Parse((string) author.Single(o => o.Key == "id").Value),
                        Speciality = author.Single(o => o.Key == "speciality").Value?.ToString(),
                        AvatarUrl = author.Single(o => o.Key == "avatarUrl").Value?.ToString(),
                        FullName = author.Single(o => o.Key == "fullname").Value?.ToString()
                    };
                }
                catch (Exception e)
                {
                    //ignore
                }

                var message = Regex.Replace((string) valueTuples.Single(o => o.Key == "message").Value,
                    @"<(.|\n)*?>", string.Empty);

                var parentId = (string) valueTuples.Single(o => o.Key == "parentId").Value;

                var changed = valueTuples.Single(o => o.Key == "timeChanged").Value?.ToString();

                result.Add(new ParsedComments
                {
                    Id = int.Parse((string) valueTuples.Single(o => o.Key == "id").Value),
                    Message = message,
                    ParentId = parentId is null ? null : int.Parse(parentId),
                    TimeChanged = changed is null ? null : DateTime.Parse(changed),
                    TimePublished =
                        DateTime.Parse(valueTuples.Single(o => o.Key == "timePublished").Value?.ToString() ??
                                       "01.01.0001"),
                    IsPostAuthor =
                        bool.Parse(valueTuples.Single(o => o.Key == "isPostAuthor").Value?.ToString() ?? "false"),
                    Author = user
                });
            }
            
            return result;
        }
    }
}