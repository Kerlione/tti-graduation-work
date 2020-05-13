using tti_graduation_work.Application.Common.Interfaces;
using System;

namespace tti_graduation_work.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
