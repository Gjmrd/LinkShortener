using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortener.Models
{
    public class ShortenedLink
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        public string Link { get; set; }

        public string FullLink { get; set; }

        public string UserId { get; set; }

        public int VisitCount { get; set; }
    }
}
