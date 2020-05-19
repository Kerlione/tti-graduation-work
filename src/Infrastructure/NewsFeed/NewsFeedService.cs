using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Xml;
using tti_graduation_work.Application.Common.Interfaces;
using tti_graduation_work.Domain.Entities;

namespace tti_graduation_work.Infrastructure.NewsFeed
{
    public class NewsFeedService : INewsFeedService
    {
        private IConfiguration _configuration;
        public NewsFeedService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IEnumerable<SyndicationItem> GetNews()
        {
            var url = _configuration.GetSection("AppSettings:RssUrl").Value;
            var reader = XmlReader.Create(url);
            var feed = SyndicationFeed.Load(reader);
            reader.Close();
            return feed.Items;
        }
    }
}
