#nullable enable
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ParserForUniversity.Models
{
    public class ApiModels
    {
        [JsonProperty("comments")]
        public IEnumerable<KeyValuePair<string,ParsedComments>> Comments { get; set; }
        
        
        [JsonProperty("comments")]
        public object Comments2 { get; set; }
    }

    public class ParsedComments
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("parentId")]
        public int? ParentId { get; set; }

        [JsonProperty("timeChanged")]
        public DateTime? TimeChanged { get; set; }

        [JsonProperty("timePublished")]
        public DateTime TimePublished { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("author")]
        public ParsedUser Author { get; set; }

        [JsonProperty("isPostAuthor")]
        public bool IsPostAuthor { get; set; }
    }

    public class ParsedUser
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("alias")]
        public string Alias { get; set; }

        [JsonProperty("fullname")]
        public string? FullName { get; set; }

        [JsonProperty("avatarUrl")]
        public string? AvatarUrl { get; set; }

        [JsonProperty("speciality")]
        public string? Speciality { get; set; }
    }
}