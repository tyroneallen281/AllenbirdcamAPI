using ABC.Api.Models;
using ABC.Domain.Entities;
using ABC.Domain.Models;
using ABC.DomainService.Interfaces;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.NotificationHubs;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ABC.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : BaseApiController
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService) 
        {
            _fileService = fileService;
        }


        /// Post File
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <returns>Created file result.</returns>
        [HttpPost("Upload")]
        //[Authorize]
        public async Task<ActionResult<FileModel>> PostFile(FileModel fileModel)
        {
            string filename = Guid.NewGuid() + Path.GetExtension(fileModel.Name);
            var result = await _fileService.SaveFileAsync(filename, fileModel.Base64, "profile-images");
            return Ok(result);
        }

        
    }
}