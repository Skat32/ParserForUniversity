using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ParserForUniversity.Interfaces;
using ParserForUniversity.Models;

namespace ParserForUniversity.Services
{
    public class ParserHtml : BaseDrone, IParser
    {
        public ParserHtml(string urlHome) : base(urlHome) { }
        
        public async Task<IEnumerable<ParsedComments>> ParseAsync(string urlToPost)
        {
            GoToUrl(urlToPost);

            ScrollToTheEnd();
            
            var commentElements =
                BaseParser.TakeElementsFromClassNameElements(Driver.PageSource,
                    "tm-comment tm-comment-thread-functional");
            
            return commentElements.Select(x =>
            {
                var dateTimeChanged = ParseTimeChanged(x.InnerHtml);
                
                return new ParsedComments
                {
                    Id = ParseId(x.InnerHtml),
                    Message = ParseMessage(x.InnerHtml),
                    TimeChanged = dateTimeChanged,
                    TimePublished = dateTimeChanged, // из-за отсутствия возможности просмотреть дату создания
                    IsPostAuthor = ParseIsAuthorPost(x.InnerHtml),
                    Author = ParseAuthor(x.InnerHtml),
                    ParentId = ParseParentId(x.OuterHtml)
                };
            });
        }

        #region Comment

        private static int ParseId(string html)
        {
            return int.Parse(BaseParser.TakeElementFromClassNameElement(html, "tm-comment__indent_l-0")
                .Attributes.Single(x => x.Name == "data-comment-body").Value);
        }

        private static string ParseMessage(string html)
        {
            return BaseParser.TakeElement(html,
                    element => element.Attributes.Any(x =>
                        x.Name == "xmlns" && x.Value == "http://www.w3.org/1999/xhtml"))
                .TextContent;
        }

        private static DateTime ParseTimeChanged(string html)
        {
            var date = BaseParser.TakeElementFromClassNameElement(html, "tm-comment-thread-functional__comment-link")
                .TextContent;

            var groups = Regex.Match(date, @"(\w+.\w+.\w+) в (\w+:\w+)").Groups;
            
            return DateTime.Parse(groups[1].Value + groups[2].Value);
        }

        private static bool ParseIsAuthorPost(string html)
        {
            return BaseParser.TakeElementFromClassNameElement(html, "tm-comment__header tm-comment__header_is-by-op") is
                not null;
        }

        private static int? ParseParentId(string html)
        {
            var element = BaseParser.TakeElementFromClassNameElement(html, "tm-comment-thread-functional__children")
                ?.ParentElement?.InnerHtml;
            
            return element is null ? null : ParseId(element);
        }
        
        #endregion
        
        #region Auhtor

        private static ParsedUser ParseAuthor(string html)
        {
            return new ParsedUser
            {
                Alias = ParseAlias(html),
                Id = 0, // нет возможности спарсить
                Speciality = string.Empty, // нет возможности спарсить
                FullName = string.Empty, // нет возможности спарсить
                AvatarUrl = ParseUrlToImage(html)
            };
        }

        private static string ParseAlias(string html)
        {
            return BaseParser.TakeTextFromClassNameElement(html, "tm-user-info__username");
        }

        private static string ParseUrlToImage(string html)
        {
            return BaseParser.TakeElementFromClassNameElement(html, "tm-entity-image__pic")?.Attributes
                .FirstOrDefault(x => x.Name == "src")?.Value;
        }
        
        #endregion
     
    }
}