using LinkShortener.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortener.Repositories
{
    public interface IShortenedLinkRepository
    {
        Task<List<ShortenedLink>> GetAsync();
        Task<ShortenedLink> GetAsync(string id);
        Task<ShortenedLink> GetByShortenedLinkAsync(string shortenedLink);
        Task<List<ShortenedLink>> GetByUserIdAsync(string userId);

        Task<ShortenedLink> AddAsync(ShortenedLink link);
        Task IncreaseVisitCountAsync(string link);
    }
}
