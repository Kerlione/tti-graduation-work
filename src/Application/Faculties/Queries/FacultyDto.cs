using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using tti_graduation_work.Application.Common.Mappings;
using tti_graduation_work.Domain.Entities;

namespace tti_graduation_work.Application.Faculties.Queries
{
    public class FacultyDto: IMapFrom<Faculty>
    {
        public int Id { get; set; }
        public int ExternalId { get; set; }
        public string Title_RU { get; set; }
        public string Title_EN { get; set; }
        public string Title_LV { get; set; }
        public string ShortTitle_EN { get; set; }
        public string ShortTitle_LV { get; set; }
        public string ShortTitle_RU { get; set; }

        public IList<ProgrameDto> Programes { get; set; }
    }
}
