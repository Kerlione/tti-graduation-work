using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        private ILogger<INewsFeedService> _logger;
        public NewsFeedService(IConfiguration configuration, ILogger<INewsFeedService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        public IEnumerable<SyndicationItem> GetNews()
        {
            var url = _configuration.GetSection("AppSettings:RssUrl").Value;
            try
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.DtdProcessing = DtdProcessing.Parse;
                var reader = XmlReader.Create(url, settings);
                var feed = SyndicationFeed.Load(reader);
                reader.Close();
                return feed.Items;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get news");
            }
            return null;
        }
    }
}
