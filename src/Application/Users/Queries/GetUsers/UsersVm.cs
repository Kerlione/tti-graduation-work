using System;
using System.Collections.Generic;
using System.Text;

namespace tti_graduation_work.Application.Users.Queries.GetUsers
{
    public class UsersVm
    {
        public IList<StatusDto> Statuses { get; set; }
        public IList<RoleDto> Roles { get; set; }
        public IList<UserDto> Users { get; set; }
        public int Total { get; set; }
    }
}
