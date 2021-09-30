using System;
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
        
        public async Task<ParsedAdvertisement[]> ParseAsync(string urlToPost)
        {
            GoToUrl(urlToPost);

            ScrollToTheEnd();
            
            var commentElements =
                BaseParser.TakeElementsFromClassNameElements(Driver.PageSource,
                    "tm-comment tm-comment-thread-functional");
            
            return commentElements.Select(x => new ParsedAdvertisement("","")).ToArray();
            
        }

        public Task<string> GetNexPageAsync()
        {
            //  найти кнопку след страницы, нажать на нее и вернуть ссылку на новую страницу

            throw new NotImplementedException();
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

    }
}