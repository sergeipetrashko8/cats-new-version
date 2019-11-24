﻿using System.Linq;
using Application.Core;
using Application.Infrastructure.DTO;
using LMP.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Application.Infrastructure.DPManagement
{
    public class UserService : IUserService
    {
        private readonly LazyDependency<IDpContext> context = new LazyDependency<IDpContext>();

        private IDpContext Context => context.Value;

        public UserData GetUserInfo(int userId)
        {
            var student = Context.Users.Include(x => x.Student).SingleOrDefault(x => x.Id == userId);
	        var lecturer = Context.Users.Include(x => x.Lecturer).SingleOrDefault(x => x.Id == userId);

	        var user = student ?? lecturer;

            return new UserData
            {
                UserId = user.Id,
                IsLecturer = user.Lecturer != null,
                IsStudent = user.Student != null,
                IsSecretary = user.Lecturer != null && user.Lecturer.IsSecretary,
                HasChosenDiplomProject = user.Student != null
                                         && Context.AssignedDiplomProjects.Any(x =>
                                             x.StudentId == user.Student.Id && !x.ApproveDate.HasValue),
                HasAssignedDiplomProject = user.Student != null
                                           && Context.AssignedDiplomProjects.Any(x =>
                                               x.StudentId == user.Student.Id && x.ApproveDate.HasValue)
            };
        }
    }
}