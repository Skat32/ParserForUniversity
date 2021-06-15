using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ParserForUniversity.Models
{
    public class ApiModels
    {
        [JsonPropertyName("comments")]
        public IEnumerable<ParsedComments> Comments { get; set; }
    }

    public class ParsedComments
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("parentId")]
        public int? ParentId { get; set; }

        [JsonPropertyName("timeChanged")]
        public DateTime TimeChanged { get; set; }

        [JsonPropertyName("timePublished")]
        public DateTime TimePublished { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("author")]
        public ParsedUser Author { get; set; }

        [JsonPropertyName("isPostAuthor")]
        public bool IsPostAuthor { get; set; }

        [JsonPropertyName("children")]
        public IEnumerable<string> Children { get; set; }
        
    }

    public class ParsedUser
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("v")]
        public string Alias { get; set; }

        [JsonPropertyName("fullname")]
        public string FullName { get; set; }

        [JsonPropertyName("avatarUrl")]
        public string AvatarUrl { get; set; }

        [JsonPropertyName("speciality")]
        public string Speciality { get; set; }
    }
}