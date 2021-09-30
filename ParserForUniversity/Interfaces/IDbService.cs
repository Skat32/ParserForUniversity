using System.Threading.Tasks;
using ParserForUniversity.Models;

namespace ParserForUniversity.Interfaces
{
    public interface IDbService
    {
        /// <summary>
        /// Сохраняет список объявлений
        /// </summary>
        /// <returns></returns>
        Task SaveAdvertisementsAsync(ApiModel data);

        /// <summary>
        /// Получаем список ссылок на объявления в которых не удалось спарсить ссылку на пользователя
        /// </summary>
        /// <returns></returns>
        Task<string[]> GetAdvertisementsUrlsAsync();
    }
}