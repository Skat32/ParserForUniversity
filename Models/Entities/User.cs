using System;
using System.Collections.Generic;

namespace Models.Entities
{
    public class User : Base, IUpdatableEntity
    {
        private string _alias;
        private string _fullName;
        private int _idBySystem;
        private string _speciality;
        private string _urlToImage;

        /// <summary>
        /// Идентификатор в распаршиваемой системе
        /// </summary>
        public int IdBySystem
        {
            get => _idBySystem;
            set
            {
                if (_idBySystem == default || value != default)
                    _idBySystem = value;
            }
        }

        /// <summary>
        /// Имя
        /// </summary>
        public string FullName
        {
            get => _fullName;
            set
            {
                if (string.IsNullOrEmpty(_fullName) || !string.IsNullOrEmpty(value))
                    _fullName = value;
            }
        }

        /// <summary>
        /// Псевдоним
        /// </summary>
        public string Alias
        {
            get => _alias;
            set
            {
                if (string.IsNullOrEmpty(_alias) || !string.IsNullOrEmpty(value))
                    _alias = value;
            }
        }

        /// <summary>
        /// Специальнотсть/описание
        /// </summary>
        public string Speciality
        {
            get => _speciality;
            set
            {
                if (string.IsNullOrEmpty(_speciality) || !string.IsNullOrEmpty(value))
                    _speciality = value;
            }
        }

        /// <summary>
        /// Ссылка на пользователя
        /// </summary>
        public string UrlToUser => $"https://habr.com/ru/users/{Alias}/";

        /// <summary>
        /// Сссылка на аватар пользователя
        /// </summary>
        public string UrlToImage
        {
            get => _urlToImage;
            set
            {
                if (string.IsNullOrEmpty(_urlToImage) || !string.IsNullOrEmpty(value))
                    _urlToImage = value;
            }
        }

        /// <summary>
        /// Посты пользователя
        /// </summary>
        public IEnumerable<Post> Posts { get; set; }

        /// <summary>
        /// Комментарии пользователя
        /// </summary>
        public IEnumerable<Comment> Comments { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}