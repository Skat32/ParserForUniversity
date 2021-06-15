using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;

namespace ParserForUniversity
{
     /// <summary>
    /// Базовый класс со стандартными методами работы с html
    /// </summary>
    public static class BaseParser
    {
        private static readonly IConfiguration Config = Configuration.Default;
        private static readonly IBrowsingContext Context = BrowsingContext.New(Config);
    
        private static IHtmlAllCollection GetDocument(string html)
        {
            return Context.GetService<IHtmlParser>()!.ParseDocument(html).All;
        }

        /// <summary>
        /// Получаем список элементов подходящих по условию из html строки
        /// </summary>
        /// <param name="html"> html строка </param>
        /// <param name="predicate"> Условие </param>
        /// <returns></returns>
        public static List<IElement> TakeElements(string html, Func<IElement, bool> predicate)
        {
            return GetDocument(html).Where(predicate).ToList();
        }

        /// <summary>
        /// Получаем первый элемент подходящий по условию из html строки
        /// </summary>
        /// <param name="html"> html строка </param>
        /// <param name="predicate"> Условие </param>
        /// <returns></returns>
        public static IElement TakeElement(string html, Func<IElement, bool> predicate)
        {
            return GetDocument(html).FirstOrDefault(predicate);
        }

        /// <summary>
        /// Получаем последний элемент подходящий по условию из html строки
        /// </summary>
        /// <param name="html"> html строка </param>
        /// <param name="predicate"> Условие </param>
        /// <returns></returns>
        public static IElement TakeLastElement(string html, Func<IElement, bool> predicate)
        {
            return GetDocument(html).LastOrDefault(predicate);
        }

        /// <summary>
        /// Получаем элемент по индексу подходящий по условию из html строки
        /// </summary>
        /// <param name="html"> html строка </param>
        /// <param name="predicate"> Условие </param>
        /// <param name="index"> Индекс </param>
        /// <returns></returns>
        public static IElement TakeElementFromNumber(string html, Func<IElement, bool> predicate, int index)
        {
            return GetDocument(html).Where(predicate).ToArray()[index];
        }

        /// <summary>
        /// Получаем элемент с выбранным классом из html строки
        /// </summary>
        /// <param name="html"> html строка </param>
        /// <param name="className"> Полное имя класса </param>
        /// <returns></returns>
        public static IElement TakeElementFromClassNameElement(string html, string className)
        {
            return GetDocument(html).FirstOrDefault(x => x.ClassName == className);
        }

        /// <summary>
        /// Получаем список элементов с выбранным классом из html строки
        /// </summary>
        /// <param name="html"> html строка </param>
        /// <param name="className"> Полное имя класса </param>
        /// <returns></returns>
        public static IEnumerable<IElement> TakeElementsFromClassNameElements(string html, string className)
        {
            return GetDocument(html).Where(x => x.ClassName == className);
        }

        /// <summary>
        /// Получаем текст из элемента с выбранным классом из html строки
        /// </summary>
        /// <param name="html"> html строка </param>
        /// <param name="className"> Полное имя класса </param>
        /// <returns></returns>
        public static string TakeTextFromClassNameElement(string html, string className)
        {
            return GetDocument(html).FirstOrDefault(x => x.ClassName == className)?.TextContent?.Trim() ?? string.Empty;
        }

        /// <summary>
        /// Получаем список строк из элементов с выбранным классом из html строки
        /// </summary>
        /// <param name="html"> html строка </param>
        /// <param name="className"> Полное имя класса </param>
        /// <returns></returns>
        public static IEnumerable<string> TakeTextsFromClassNameElements(string html, string className)
        {
            return GetDocument(html).Where(x => x.ClassName == className).Select(x => x.TextContent?.Trim() ?? string.Empty);
        }

        /// <summary>
        /// Получаем ссылку на изображение из элемента с выбранным классом из html строки
        /// </summary>
        /// <param name="html"> html строка </param>
        /// <param name="className"> Полное имя класса </param>
        /// <returns></returns>
        public static string TakeUrlToImageFromClassNameElement(string html, string className)
        {
            return GetDocument(html).FirstOrDefault(x => x.ClassName == className)?.Attributes.GetNamedItem("src")
                ?.Value ?? string.Empty;
        }

        /// <summary>
        /// Получаем ссылку на изображение из элемента по условию из html строки
        /// </summary>
        /// <param name="html"> html строка </param>
        /// <param name="predicate"> Условие </param>
        /// <returns></returns>
        public static string TakeUrlToImage(string html, Func<IElement, bool> predicate)
        {
            return GetDocument(html).FirstOrDefault(predicate)?.Attributes.GetNamedItem("src")?.Value ?? string.Empty;
        }

        /// <summary>
        /// Получаем ссылки из элементов с выбранным классом из html строки
        /// </summary>
        /// <param name="html"> html строка </param>
        /// <param name="className"> Полное имя класса </param>
        /// <returns></returns>
        public static IEnumerable<string> TakeUrlsFromClassNameElements(string html, string className)
        {
            return GetDocument(html).Where(x => x.ClassName == className)
                .Select(x => x.Attributes.GetNamedItem("href")?.Value ?? string.Empty);
        }

        /// <summary>
        /// Получаем ссылки из элементов по условию из html строки
        /// </summary>
        /// <param name="html"> html строка </param>
        /// <param name="predicate"> Условие </param>
        /// <returns></returns>
        public static IEnumerable<string> TakeUrls(string html, Func<IElement, bool> predicate)
        {
            return GetDocument(html).Where(predicate)
                .Select(x => x.Attributes.GetNamedItem("href")?.Value ?? string.Empty);
        }
        
        /// <summary>
        /// Получаем ссылку из элемента с выбранным классом из html строки
        /// </summary>
        /// <param name="html"> html строка </param>
        /// <param name="className"> Полное имя класса </param>
        /// <returns></returns>
        public static string TakeUrlFromClassNameElement(string html, string className)
        {
            return GetDocument(html).FirstOrDefault(x => x.ClassName == className)?.Attributes.GetNamedItem("href")
                ?.Value ?? string.Empty;
        }

        /// <summary>
        /// Получаем ссылку из элемента по условию из html строки
        /// </summary>
        /// <param name="html"> html строка </param>
        /// <param name="predicate"> Условие </param>
        /// <returns></returns>
        public static string TakeUrl(string html, Func<IElement, bool> predicate)
        {
            return GetDocument(html).FirstOrDefault(predicate)?.Attributes.GetNamedItem("href")?.Value ?? string.Empty;
        }
    }
}