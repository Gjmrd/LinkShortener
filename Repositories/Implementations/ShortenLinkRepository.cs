using LinkShortener.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortener.Data.Repositories
{
    public class ShortenLinkRepository : IShortenedLinkRepository
    {
        private readonly IMongoCollection<ShortenedLink> _links;

        public ShortenLinkRepository(IShortenedLinkDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _links = database.GetCollection<ShortenedLink>(settings.ShortenedLinksCollectionName);
        }


        public Task<List<ShortenedLink>> GetAsync() => _links.Find(link => true).ToListAsync();

        public Task<ShortenedLink> GetAsync(string id) => _links.Find<ShortenedLink>(link => link.Id == id).FirstOrDefaultAsync();

        public async Task<ShortenedLink> AddAsync(ShortenedLink link)
        {
            await _links.InsertOneAsync(link);
            return link;
        }

        public Task<List<ShortenedLink>> GetByUserIdAsync(string userId) => _links.Find(link => link.UserId == userId).ToListAsync();

        public Task<ShortenedLink> GetByShortenedLinkAsync(string shortenedLink) => _links.Find(link => link.Link == shortenedLink).FirstOrDefaultAsync();

        public async Task IncreaseVisitCountAsync(string link)
        {
            var filter = Builders<ShortenedLink>.Filter.Eq("Link", link);

            var update = Builders<ShortenedLink>.Update.Inc("VisitCount", 1);

            await _links.UpdateOneAsync(filter, update);
        }
    } 
}
