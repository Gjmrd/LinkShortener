using LinkShortener.Extensions;
using LinkShortener.Models;
using LinkShortener.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortener.Services.Implementations
{
    public class ShortenedLinkService : IShortenedLinkService
    {
        private readonly IShortenedLinkRepository _shortenedLinkRepository;

        private static Random random = new Random();
       
        public ShortenedLinkService(IShortenedLinkRepository shortenedLinkRepository)
        {
            _shortenedLinkRepository = shortenedLinkRepository;
        }

        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task<ShortenedLink> CreateShortenedLinkAsync(string fullLink, string userId)
        {
            if (fullLink.IsNullOrEmpty())
                throw new Exception("full link is null or empty");

            if (userId.IsNullOrEmpty())
                throw new Exception("UserId is null or empty");

            var shortenedLink = await _shortenedLinkRepository.AddAsync(new ShortenedLink()
            {
                FullLink = fullLink,
                UserId = userId,
                Link = RandomString(5)
            }); 

            return shortenedLink;
        }

        public async Task<List<object>> GetShortenedLinksByUserIdAsync(string userId)
        {
            if (userId.IsNullOrEmpty())
                throw new Exception("UserId is null or empty");

            var links = await _shortenedLinkRepository.GetByUserIdAsync(userId);

            return links.Select(link => (dynamic) new
            { 
                link.Link,
                link.VisitCount
            }).ToList();
        }

        public async Task<string> GetLinkByShortenedLinkAsync(string shortenedLink)
        {
            var link = await _shortenedLinkRepository.GetByShortenedLinkAsync(shortenedLink);
            if (link != null)
                await _shortenedLinkRepository.IncreaseVisitCountAsync(shortenedLink);

            return link?.FullLink;
        }
    }
}
