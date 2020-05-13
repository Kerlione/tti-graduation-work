using System;
using System.Collections.Generic;
using System.Text;
using tti_graduation_work.Domain.Common;

namespace tti_graduation_work.Domain.Entities
{
    public class Attachment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Content { get; set; }
        public int StepId { get; set; }
        public Step Step { get; set; }
    }
}
