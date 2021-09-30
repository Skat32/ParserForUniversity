using System;
using System.Linq;
using System.Threading;
using OpenQA.Selenium.Interactions;
using ParserForUniversity.Interfaces;
using ParserForUniversity.Models;

namespace ParserForUniversity.Services
{
    public class ParserHtml : BaseDrone, IParser
    {
        public ParserHtml(string urlHome) : base(urlHome) { }
        
        public ParsedAdvertisement[] Parse(string urlToPost)
        {
            GoToUrl(urlToPost);
            ScrollToTheEnd();
            
            var elements =
                BaseParser.TakeElementsFromClassNameElements(Driver.PageSource,
                    "iva-item-root-Nj_hb photo-slider-slider-_PvpN iva-item-list-H_dpX iva-item-redesign-nV4C4 iva-item-responsive-gIKjW items-item-My3ih items-listItem-Gd1jN js-catalog-item-enum");
            
            return elements.Select(x => new ParsedAdvertisement(ParseUrl(x.InnerHtml), ParseUserUrl(x.InnerHtml))).ToArray();
        }

        public string GetNexPage(int index)
        {
            try
            {
                var elements = Driver.FindElementsByXPath(".//*[@class='pagination-item-JJq_j pagination-item_arrow-Sttbt']")[index];
                var action = new Actions(Driver);
                action.MoveToElement(elements).Perform();
                action.MoveToElement(elements).Click().Perform();
                Thread.Sleep(1000);
                return Driver.Url;
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        public string ParseUserLink(string advertisement)
        {
            GoToUrl(advertisement);

            var element =
                BaseParser.TakeElementFromClassNameElement(Driver.PageSource, "seller-info-name js-seller-info-name");

            return element.Children[0].Attributes.GetNamedItem("href")
                ?.Value ?? string.Empty;
        }

        #region Comment

        private static string ParseUserUrl(string html) =>
            BaseParser.TakeUrlFromClassNameElement(html, "link-link-MbQDP link-design-inherited-Ys4mw link-novisited-UCnee ");

        private static string ParseUrl(string html) =>
            BaseParser.TakeUrlFromClassNameElement(html, "iva-item-sliderLink-bJ9Pv");

        #endregion

    }
}