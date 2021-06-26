#nullable enable
using System;
using System.Collections.Generic;

namespace ParserForUniversity.Responses
{
    public class PostResponse
    {
        public IEnumerable<CommentResponse> Comments { get; }

        public PostResponse(IEnumerable<CommentResponse> comments)
        {
            Comments = comments;
        }
    }

    public class CommentResponse
    {
        // public CommentResponse? Parent { get; }

        public string Id { get; }

        public IEnumerable<string> Children { get; }

        public string Message { get; }

        public DateTime TimeChanged { get; }

        public DateTime TimePublished { get; }
        
        public bool IsPostAuthor { get; }
        
        public UserResponse User { get; }

        public CommentResponse(string id,
            string message,
            DateTime timeChanged, 
            DateTime timePublished,
            bool isPostAuthor,
            UserResponse user, 
            IEnumerable<string> children)
        {
            Message = message;
            TimeChanged = timeChanged;
            TimePublished = timePublished;
            IsPostAuthor = isPostAuthor;
            User = user;
            Children = children;
            Id = id;
        }
    }

    public class UserResponse
    {
        public int Id { get; }

        public string Alias { get; }
        
        public string? FullName { get; }

        public string? AvatarUrl { get; }

        public string? Speciality { get; }

        public UserResponse(int id,
            string alias,
            string? fullName,
            string? avatarUrl,
            string? speciality)
        {
            Id = id;
            Alias = alias;
            FullName = fullName;
            AvatarUrl = avatarUrl;
            Speciality = speciality;
        }
    }
}