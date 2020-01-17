using LinkShortener.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortener.Services
{
    public interface IShortenedLinkService
    {
        /// <summary>
        /// Creates shortened link from full link 
        /// </summary>
        /// <param name="fullLink">full link</param>
        /// <param name="userId">Id of user created link</param>
        /// <exception cref="Exception">when full link or user id is nul or empty</exception>
        /// <returns>shortened link attached to user ID</returns>
        Task<ShortenedLink> CreateShortenedLinkAsync(string fullLink, string userId);

        /// <summary>
        /// Returns list of shortened links of certain user
        /// </summary>
        /// <param name="userId">user's ID</param>
        /// <exception cref="Exception">when user Id is null or empty</exception>
        /// <returns>list of shortened links</returns>
        Task<List<dynamic>> GetShortenedLinksByUserIdAsync(string userId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shortenedLink"></param>
        /// <returns></returns>
        Task<string> GetLinkByShortenedLinkAsync(string shortenedLink);
    }
}
