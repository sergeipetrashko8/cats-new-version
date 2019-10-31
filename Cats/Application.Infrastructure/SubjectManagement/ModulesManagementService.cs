﻿using System.Collections.Generic;
using System.Linq;
using LMP.Data;
using LMP.Models;

namespace Application.Infrastructure.SubjectManagement
{
    public class ModulesManagementService : IModulesManagementService
    {
        public ICollection<Module> GetModules()
        {
            using var repositoriesContainer = new LmPlatformRepositoriesContainer();
            
            return repositoriesContainer.ModulesRepository.GetAll().ToList();
        }

        public IEnumerable<Module> GetModules(int subjectId)
        {
            using var repositoriesContainer = new LmPlatformRepositoriesContainer();
            
            return repositoriesContainer.ModulesRepository.GetAll()
                .Where(s => s.SubjectModules.Any(sm => sm.SubjectId == subjectId))
                .ToList();
        }
    }
}