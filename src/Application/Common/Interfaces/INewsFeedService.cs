using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Text;
using tti_graduation_work.Domain.Entities;

namespace tti_graduation_work.Application.Common.Interfaces
{
    public interface INewsFeedService
    {
        public IEnumerable<SyndicationItem> GetNews();
    }
}
