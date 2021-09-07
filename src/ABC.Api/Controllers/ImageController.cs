using ABC.Api.Models;
using ABC.Domain.Entities;
using ABC.Domain.Models;
using ABC.DomainService.Interfaces;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ABC.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : BaseApiController
    {
        private readonly IImageService _imageService;
        public ImageController(IImageService imageService) 
        {
            _imageService = imageService;
        }

        // GET api/values/5
        [HttpGet]
        public ActionResult<ImageModel> Get()
        {
            return _imageService.GetRandom();
        }

    }
}