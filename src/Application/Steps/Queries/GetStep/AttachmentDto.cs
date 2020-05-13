using tti_graduation_work.Application.Common.Mappings;
using tti_graduation_work.Domain.Entities;

namespace tti_graduation_work.Application.Steps.Queries.GetStep
{
    public class AttachmentDto: IMapFrom<Attachment>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Data { get; set; }
    }
}