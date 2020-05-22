using System;
using System.Collections.Generic;
using System.Text;
using tti_graduation_work.Application.Users.Commands.CreateUser;

namespace tti_graduation_work.Application.IntegrationTests.TestData
{
    public class Users
    {
        private enum Roles
        {
            Student,
            Supervisor, 
            Administrator
        }

        public static CreateUserCommand Student1User =>
            new CreateUserCommand
            {
                Username = "student1",
                Password = "test1",
                Role = (int)Roles.Student
            };


        public static CreateUserCommand Student2User =>
            new CreateUserCommand
            {
                Username = "student2",
                Password = "test1",
                Role = (int)Roles.Student
            };

        public static CreateUserCommand SupervisorUser =>
            new CreateUserCommand
            {
                Username = "supervisor1",
                Password = "test1",
                Role = (int)Roles.Supervisor
            };
    }
}
