﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using LMPlatform.Models;
//using LMPlatform.Data.Repositories;
using Application.Core.Data;
using Application.Core;
using Application.Infrastructure.FilesManagement;
using System.Configuration;
//using iTextSharp.text.pdf;
using System.IO;
using LMP.Models;
//using WMPLib;
using LMP.Data;

namespace Application.Infrastructure.WatchingTimeManagement
{
    public class WatchingTimeService : IWatchingTimeService
    {
        private readonly LazyDependency<IFilesManagementService> _filesManagementService = 
            new LazyDependency<IFilesManagementService>();

        public IFilesManagementService FilesManagementService => _filesManagementService.Value;

        public int GetEstimatedTime(string container)
        {
            var attachments = FilesManagementService.GetAttachments(container);
            if (attachments.Count == 0)
                return 0;

            var path = string.Empty;//todo # ConfigurationManager.AppSettings["FileUploadPath"] + attachments[0].PathName + "\\" + attachments[0].FileName;


            if (!File.Exists(path))
                return 0;
            try
            {
                //todo # 
                //PdfReader pdfReader = new PdfReader(path);
                //int numberOfPages = pdfReader.NumberOfPages;
                //return numberOfPages * 30; //30 сек страница временно тут
                return 0;
            }
            catch
            {
                //todo # 
                //var player = new WindowsMediaPlayer();
                //var clip = player.newMedia(path);
                //return (int)clip.duration;
                return 0;
            }
        }

        public void SaveWatchingTime(WatchingTime item)
        {
            using var repositoriesContainer = new LmPlatformRepositoriesContainer();

            var watchingTime = repositoriesContainer.WatchingTimeRepository.GetByUserConceptIds(item.UserId, item.ConceptId);
            //var watchingTime = repositoriesContainer.WatchingTimeRepository.GetByUserConceptIds(item.UserId,item.Concept.Id);
            if (watchingTime is { })
            {
                watchingTime.Time += item.Time;
                repositoriesContainer.WatchingTimeRepository.Save(watchingTime);
            }
            else
            {
                repositoriesContainer.WatchingTimeRepository.Save(item);
            }
            repositoriesContainer.ApplyChanges();
        }

        public WatchingTime GetByConceptSubject(int conceptId, int userId)
        {
            using var repositoriesContainer = new LmPlatformRepositoriesContainer();

            return repositoriesContainer.WatchingTimeRepository.GetAll(new Query<WatchingTime>().AddFilterClause(u => u.ConceptId == conceptId & u.UserId == userId)).ToList()[0];
            //return repositoriesContainer.WatchingTimeRepository.GetAll(new Query<WatchingTime>().AddFilterClause(u => u.Concept.Id == conceptId & u.UserId == userId)).ToList()[0];
        }

        public List<WatchingTime> GetAllRecords(int conceptId, int? studentId = null)
        {
            using var repositoriesContainer = new LmPlatformRepositoriesContainer();

            return repositoriesContainer.WatchingTimeRepository.GetAll(new Query<WatchingTime>().AddFilterClause(u => u.ConceptId == conceptId && u.UserId == (studentId ?? u.UserId))).ToList();
            //return repositoriesContainer.WatchingTimeRepository.GetAll(new Query<WatchingTime>().AddFilterClause(u => u.Concept.Id == conceptId)).ToList();
        }
    }
}
