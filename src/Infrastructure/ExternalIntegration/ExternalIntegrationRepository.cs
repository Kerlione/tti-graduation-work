using System;
using System.Collections.Generic;
using System.Text;
using tti_graduation_work.Application.Common.Interfaces;
using tti_graduation_work.Domain.Entities;

namespace tti_graduation_work.Infrastructure.ExternalIntegration
{
    public class ExternalIntegrationRepository : IExternalIntegrationService
    {
        private static List<Faculty> _faculties => new List<Faculty>
        {
        };
        public List<Faculty> GetFaculties()
        {
            throw new NotImplementedException();
        }
        private static List<Student> _students => new List<Student>
        {
        };
        public List<Student> GetStudents()
        {
            throw new NotImplementedException();
        }
        private static List<Supervisor> _supervisors => new List<Supervisor>
        {
            new Supervisor
            {
                    Degree = "Mg.sc.ing",
                    Email = "savrasovs.m@tsi.lv",
                    FacultyId = 0,
                    ExternalId = 1,
                    JobPositionId = 0,
                    Name = "Mihails",
                    Surname = "Savrasovs",
                    StudentLimit = 5,
                    Phone = "+37125739845",
            }
        };
        public List<Supervisor> GetSupervisors()
        {
            throw new NotImplementedException();
        }
    }
}
