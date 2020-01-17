using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortener.Models
{
    public class ShortenedLinkDatabaseSettings : IShortenedLinkDatabaseSettings
    {
        public string ShortenedLinksCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IShortenedLinkDatabaseSettings
    {
        string ShortenedLinksCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
