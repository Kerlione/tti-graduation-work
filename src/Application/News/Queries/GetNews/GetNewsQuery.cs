using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using tti_graduation_work.Application.Common.Interfaces;
using tti_graduation_work.Application.Common.Models;

namespace tti_graduation_work.Application.News.Queries.GetNews
{

	public class GetNewsQuery : IRequest<NewsVm>
	{
	}

	public class GetNewsQueryHandler : IRequestHandler<GetNewsQuery, NewsVm>
	{
		private INewsFeedService _newsFeedService;
		private IMapper _mapper;

		public GetNewsQueryHandler(INewsFeedService newsFeedService, IMapper mapper)
		{
			_newsFeedService = newsFeedService;
			_mapper = mapper;
		}

		public async Task<NewsVm> Handle(GetNewsQuery request, CancellationToken cancellationToken)
		{
			var data = _newsFeedService.GetNews();
			return new NewsVm
			{
				FeedItems = data?.AsQueryable().ProjectTo<FeedItem>(_mapper.ConfigurationProvider).ToList()
			};
		}
	}
}
