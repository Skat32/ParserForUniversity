using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using ParserForUniversity.Interfaces;
using ParserForUniversity.Models;

namespace ParserForUniversity.Services
{
    public class DbService : IDbService
    {
        private readonly DataDbContext _context;
        
        public DbService(string[] args)
        {
            _context = new DataDbContextFactory().CreateDbContext(args);;
        }
        
        public async Task SaveCommentsAsync(ParsedComments[] comments, string postUrl)
        {
            var postId = int.Parse(Regex.Match(postUrl, @"\/([0-9]+)").Groups[1].Value);

            var post = _context.Posts.FirstOrDefault(x => x.IdBySystem == postId);
            
            if (post is null)
            {
                post = new Post
                {
                    UrlToPost = postUrl,
                    IdBySystem = postId
                };

                await _context.Posts.AddAsync(post);
            }

            var commentIds = await _context.Comments.Select(x => x.IdBySystem).ToListAsync();
            comments = comments.Where(x => !commentIds.Contains(x.Id)).ToArray();

            var commentsForDb = new List<Comment>();
            
            foreach (var comment in comments)
            {
                var user = _context.Users.FirstOrDefault(x => x.IdBySystem == comment.Author.Id);

                if (user is null)
                {
                    user = new User
                    {
                        Alias = comment.Author.Alias,
                        Speciality = comment.Author.Speciality,
                        FullName = comment.Author.FullName,
                        IdBySystem = comment.Author.Id,
                        UrlToImage = comment.Author.AvatarUrl
                    };
                }
                else
                {
                    user.Alias = comment.Author.Alias;
                    user.Speciality = comment.Author.Speciality;
                    user.FullName = comment.Author.FullName;
                    user.UrlToImage = comment.Author.AvatarUrl;
                }

                var commentDb = _context.Comments.FirstOrDefault(x => x.IdBySystem == comment.Id);
                
                if (commentDb is null)
                    commentsForDb.Add(new Comment
                    {
                        User = user,
                        CommentText = comment.Message,
                        TimeChanged = comment.TimeChanged,
                        TimePublished = comment.TimePublished,
                        IsPostAuthor = comment.IsPostAuthor,
                        CommentParentIdInSystem = comment.ParentId,
                        Post = post,
                        IdBySystem = comment.Id
                    });
                else
                {
                    commentDb.CommentText = comment.Message;
                    commentDb.TimeChanged = comment.TimeChanged;
                }
            }

            await _context.Comments.AddRangeAsync(commentsForDb);

            await _context.SaveChangesAsync();
            
            var commentsDb = await _context.Comments.ToListAsync();
            
            foreach (var comment in commentsDb)
            {
                comment.CommentParent = commentsDb.FirstOrDefault(x => x.IdBySystem == comment.IdBySystem);
            }

            await _context.SaveChangesAsync();
        }
    }
}