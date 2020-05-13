using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace tti_graduation_work.Application.News.Queries.GetNews
{

	public class GetNewsQuery : IRequest<NewsVm>
	{
	}

	public class GetNewsQueryHandler : IRequestHandler<GetNewsQuery, NewsVm>
	{
		public async Task<NewsVm> Handle(GetNewsQuery request, CancellationToken cancellationToken)
		{
			// TODO: implement Infrastructure layer for RSS feed access
			throw new NotImplementedException();
		}
	}
}
