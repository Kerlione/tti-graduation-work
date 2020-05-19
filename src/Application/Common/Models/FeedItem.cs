using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using tti_graduation_work.Application.Common.Mappings;

namespace tti_graduation_work.Application.Common.Models
{
    public class FeedItem: IMapFrom<SyndicationItem>
    {
        public DateTime PublishDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SyndicationItem, FeedItem>()
                .ForMember(f => f.PublishDate, opt => opt.MapFrom(i => i.PublishDate.UtcDateTime))
                .ForMember(f => f.Title, opt => opt.MapFrom(i => i.Title.Text))
                .ForMember(f => f.Description, opt => opt.MapFrom(i => i.Summary.Text))
                .ForMember(f => f.Link, opt => opt.MapFrom(i => i.Links.FirstOrDefault().Uri.ToString()));
        }
    }
}
