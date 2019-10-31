﻿using Application.Core;
using LMP.Data.Repositories;
using LMP.Data.Repositories.RepositoryContracts;

namespace LMP.Data.Infrastructure
{
    public class DataModule
    {
        public static IUnityContainerWrapper Initialize(IUnityContainerWrapper containerWrapper)
        {
            containerWrapper.Register<IUsersRepository, UsersRepository>();
            containerWrapper.Register<IBugsRepository, BugsRepository>();
            containerWrapper.Register<IGroupsRepository, GroupsRepository>();
            containerWrapper.Register<IProjectsRepository, ProjectsRepository>();
            containerWrapper.Register<IStudentsRepository, StudentsRepository>();
            containerWrapper.Register<ISubjectRepository, SubjectRepository>();
            containerWrapper.Register<ITestsRepository, TestsRepository>();
            containerWrapper.Register<IModulesRepository, ModulesRepository>();
            containerWrapper.Register<IMessageRepository, MessageRepository>();
            containerWrapper.Register<IDpContext, LmPlatformModelsContext>();
            containerWrapper.Register<ICpContext, LmPlatformModelsContext>();
            //containerWrapper.Register<IProjectMatrixRequirementsRepository, ProjectMatrixRequirementsRepository>();
            return containerWrapper;
        }
    }
}