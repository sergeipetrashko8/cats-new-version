using System;
using System.Configuration;
using System.Linq;
using Application.Core;
using Application.Infrastructure.FilesManagement;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Services.Models.Files;

namespace WebAPI.Controllers.Services.Files
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesServiceController : ControllerBase
    {
        private readonly LazyDependency<IFilesManagementService> _filesManagementService = 
            new LazyDependency<IFilesManagementService>();

        public IFilesManagementService FilesManagementService {
            get { return _filesManagementService.Value; }
        }

        public AttachmentResult GetFiles()
        {
            try
            {
                var attachments = new FilesManagementService().GetAttachments(null).ToList();
                string storageRoot = ConfigurationManager.AppSettings["FileUploadPath"];
                var result = new AttachmentResult
                {
                    Files = attachments,
                    ServerPath = storageRoot,
                    Message = "Данные успешно загружены",
                    Code = "200"
                };

                return result;
            }
            catch (Exception e)
            {
                return new AttachmentResult
                {
                    Message = "Произошла ошибка при получении данных",
                    Code = "500"
                };
            }
        }

    }
}