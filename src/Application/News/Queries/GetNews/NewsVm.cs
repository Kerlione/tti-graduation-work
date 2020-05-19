using System;
using System.Collections.Generic;
using System.Text;
using tti_graduation_work.Application.Common.Models;

namespace tti_graduation_work.Application.News.Queries.GetNews
{
    public class NewsVm
    {
        public IList<FeedItem> FeedItems { get; set; }
    }
}
