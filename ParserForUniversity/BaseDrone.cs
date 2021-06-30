using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AngleSharp.Dom;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;

namespace ParserForUniversity
{
    public class BaseDrone
    {
        #region | Fields |

        protected RemoteWebDriver Driver;

        /// <summary> Стартовая страница сайта который мы парсим </summary>
        private readonly string _urlHome;
        
        #endregion
        
        protected BaseDrone(string urlHome)
        {
            _urlHome = urlHome;

            InitDrone();
        }
        
        #region | Protected methods |

        /// <summary>
        /// Инициализация драйвера 
        /// </summary>
        /// <returns></returns>
        protected void InitDrone()
        {
            Driver = DroneFactory.GetDriver();
            Driver.Manage().Window.Maximize();
            GoToUrl(_urlHome);
            
            Thread.Sleep(4000);
                
            RefreshPage();
          
            Thread.Sleep(2000);  //необходимо дождаться загрузки страницы
        }
        
        /// <summary>
        /// Переход на страницу
        /// </summary>
        /// <param name="url"></param>
        protected void GoToUrl(string url)
        {
            MoveToUrl(url);
        }

        #endregion

        #region | Virtual methods |

        /// <summary>
        /// Скроллинг страницы
        /// </summary>
        protected virtual void PageScrolling()
        {
            IJavaScriptExecutor js = Driver;
            js.ExecuteScript("window.scrollTo(0, 4000)");
            Thread.Sleep(2000);
            js.ExecuteScript("window.scrollTo(4000, 6000)");
            Thread.Sleep(2000);
            js.ExecuteScript("window.scrollTo(6000, 8000)");
            Thread.Sleep(2000);
            js.ExecuteScript("window.scrollTo(8000, 10000)");
        }

        /// <summary>
        /// Небольшой скроллинг страницы
        /// </summary>
        protected virtual void MicroScroll()
        {
            IJavaScriptExecutor js = Driver;
            js.ExecuteScript("window.scrollTo(0, 500)");
            Thread.Sleep(2000);
            js.ExecuteScript("window.scrollTo(1000, 2000)");
            Thread.Sleep(2000);
        }
        
        /// <summary>
        /// Скроллинг страницы до конца
        /// </summary>
        protected virtual void ScrollToTheEnd()
        {
            string html;
            IJavaScriptExecutor js = Driver;
            long scroll = 0;
            do
            {
                html = Driver.PageSource;
                
                js.ExecuteScript($"window.scrollTo({scroll}, {scroll += 2000})");
                Thread.Sleep(2000);
            } while (html != Driver.PageSource);
        }
        
        /// <summary>
        /// Скроллинг в открывшемся окне
        /// </summary>
        /// <param name="scrollClassName"></param>
        /// <param name="predicate"></param>
        protected virtual IEnumerable<IElement> ScrollToTheEnd(string scrollClassName, Func<IElement, bool> predicate)
        {
            var items = BaseParser.TakeElements(Driver.PageSource, predicate);
            IJavaScriptExecutor js = Driver;
            var step = 0;
            int count;
            
            do
            {
                js.ExecuteScript($"document.getElementsByClassName('{scrollClassName}')[0].scrollTo(0,{step += 50000})");
                Thread.Sleep(2000);
                count = items.Count;
                items = BaseParser.TakeElements(Driver.PageSource, predicate);
            }
            while (count != items.Count);

            return items.ToList();
        }

        /// <summary>
        /// Скроллинг в открывшемся окне
        /// </summary>
        /// <param name="scrollClassName"></param>
        protected virtual void ScrollToTheEnd(string scrollClassName)
        {
            IJavaScriptExecutor js = Driver;
            string html;
            var step = 0;

            do
            {
                html = Driver.PageSource;

                js.ExecuteScript($"document.getElementsByClassName('{scrollClassName}')[0].scrollTo(0,{step += 50000})");
                Thread.Sleep(2000);
            } while (html != Driver.PageSource);

        }

        /// <summary>
        /// Скроллинг страницы, пока мы не соберем количество элементов равных - countElement
        /// </summary>
        /// <param name="countElement"> максимальный объем элементов который нас интересует </param>
        /// <param name="predicate"> условие отбора элементов </param>
        /// <returns> список элементов удовлетворяющий условию </returns>
        protected virtual IEnumerable<IElement> ScrollToCountElement(int countElement, Func<IElement, bool> predicate)
        {
            var count = 0;
            var items = BaseParser.TakeElements(Driver.PageSource, predicate);
            
            while (items.Count < countElement && count != items.Count )
            {
                PageScrolling();
                count = items.Count;
                items = BaseParser.TakeElements(Driver.PageSource, predicate);
            }

            return items.Take(countElement).ToList();
        }
        
        /// <summary>
        /// Клик на элемент по xPathClick с использованием Actions
        /// (Используется за частую, когда не получается нажать на элемент используя просто Driver)
        /// </summary>
        /// <param name="xPathClick"> Элемент по xPath </param>
        /// <param name="index"> Номер элемента (При необходимости) </param>
        /// <exception cref="NoSuchElementException"></exception>
        protected virtual void ClickWithAction(string xPathClick, int index = 0)
        {
            var element = Driver.FindElementsByXPath(xPathClick);
            
            if (element is null || !element.Any())
                throw new NoSuchElementException();

            var action = new Actions(Driver);

            action.MoveToElement(element[index]).Perform();
            action.MoveToElement(element[index]).Click().Perform();

            Thread.Sleep(2000);
        }
        
        /// <summary>
        /// Клик на элементы элементы удовлетворяющие xPathClick, пока они не кончатся
        /// </summary>
        /// <param name="xPathClick"> Элемент по xPath </param>
        /// <returns> Удалось ли кликнуть хотя бы раз </returns>
        protected virtual bool ClickToEnd(string xPathClick)
        {
            var result = false;
            try
            {
                string html;
                do
                {
                    html = Driver.PageSource;
                    ClickWithAction(xPathClick);
                    result = true; // Если удалось хотя бы один раз кликнуть, возвращаем true
                } while (html != Driver.PageSource);
            }
            catch
            {
                Console.WriteLine($"No more for click for xath : '{xPathClick}'");
            }

            return result;
        }

        /// <summary>
        /// Закрытие окна браузера
        /// (Рекомендуется переопределять реализацию метода для управления состояниями ботов)
        /// </summary>
        public virtual void CloseDrone()
        {
            Driver.Dispose();
        }

        /// <summary>
        /// Обновление страницы
        /// </summary>
        protected virtual void RefreshPage()
        {
            Driver.Navigate().Refresh();
        }

        /// <summary>
        /// Удаление шапок на странице
        /// </summary>
        protected virtual void RemoveElements() { }
        
        #endregion

        #region | Private methods |

        private void MoveToUrl(string url)
        {
            try
            {

                if (Driver.Url == url)
                    return; // если мы уже находимся на данной странице, то не надо на неё переходить
                
                Driver.Navigate().GoToUrl(url);
                Thread.Sleep(2000);
                
                RemoveElements();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Cannot open page. Url: {url}");
            }
        }

        #endregion
    }
}