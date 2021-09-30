using System;

namespace Models.Entities
{
    public class Advertisement : IUpdatableEntity
    {
        /// <summary>
        /// Идентификатор записи
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Ссылка на объявление
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// Ссылка на пользователя
        /// </summary>
        public string? UrlToUser { get; private set; }

        /// <summary>
        /// Тип объявления
        /// </summary>
        public TypeAdvertisement TypeAdvertisement { get; private set; }

        /// <summary>
        /// Ссылка на которой спарсили данное объявление
        /// </summary>
        public string UrlByParsed { get; private set; }
        
        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedAt { get; set; }
        
        /// <summary>
        /// Дата обновления (не используется)
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        public Advertisement(string url, string? urlToUser, TypeAdvertisement typeAdvertisement, string urlByParsed)
        {
            Url = url;
            UrlToUser = urlToUser;
            TypeAdvertisement = typeAdvertisement;
            UrlByParsed = urlByParsed;
            Id = Guid.NewGuid();
        }

        public void SetUserUrl(string url)
        {
            UrlToUser = url;
        }
    }

    /// <summary>
    /// Тип объявления
    /// </summary>
    public enum TypeAdvertisement
    {
        /// <summary>
        /// Квартира
        /// </summary>
        Flat = 1,
        
        /// <summary>
        /// Комната
        /// </summary>
        Room = 2,
        
        /// <summary>
        /// Дома, дачи, коттетжи
        /// </summary>
        HousesOrCottages = 3,
        
        /// <summary>
        /// Нежвижимость зарубежом
        /// </summary>
        RealEstateAbroad = 4
    }
}