using LinqKit;
using ABC.Domain.Entities;
using ABC.Domain.Models;
using ABC.DomainService.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using ABC.Repository.Repos;
using System.Collections.Generic;

namespace ABC.DomainService.Services
{
    public sealed class ImageService : IImageService
    {
        private readonly IFileService _fileService;
        private readonly IImageRepository _imageRepository;
        private readonly ISightingRepository _sightingRepository;
        private readonly IConfiguration Configuration;

        public ImageService(IImageRepository imageRepository, ISightingRepository sightingRepository, IFileService fileService)
        {
            _fileService = fileService;
            _imageRepository = imageRepository;
            _sightingRepository = sightingRepository;
            //Configuration = configuration;
        }

        public ImageModel Get(int imageId)
        {
            var image = _imageRepository.GetById(imageId);
            return new ImageModel(image);
        }

    
        public ImageModel GetRandom()
        {
            var random = new Random(DateTime.Now.Millisecond * DateTime.Now.Second * DateTime.Now.Day * DateTime.Now.Month);
            var sightExpression = PredicateBuilder.New<Sighting>(true);
            sightExpression = sightExpression.And(_ => _.Image.IsActive);
            var sightQuery = _sightingRepository.GetQuery(sightExpression).GroupBy(_ => _.Code).Select(_ => _.Key);
            var groupdCount = sightQuery.Count();
            var code = random.Next(0, groupdCount);
            var groupCode = sightQuery.Skip(code).FirstOrDefault();

            var expression = PredicateBuilder.New<Image>(true);
            expression = expression.And(_ => _.IsActive);
            expression = expression.And(_ => _.Sightings.Any(_ => _.Code == code) &&  _.Sightings.Select(t =>  t.Votes.Count(_ => _.VoteEnum == Domain.Enums.VoteEnum.No) < 4 && t.Votes.Count() < 10).Any());
            var imageCount = _imageRepository.Count(expression);
            var itemIndex = random.Next(0, imageCount);
            var banners = _imageRepository.Get(expression);
            var banner = banners.Skip(itemIndex).FirstOrDefault();
            if (banner == null)
            {
                banner = _imageRepository.Get(_ => _.IsActive).FirstOrDefault();
                if (banner == null)
                {
                    return null;
                }
            }
            return new ImageModel(banner);
        }

        public async Task<List<ImageModel>> GetCorrectSightings()
        {
            try
            {
                var expression = PredicateBuilder.New<Sighting>(true);
                expression = expression.And(_ => _.Image.IsActive);
                expression = expression.And(_ => _.Votes.Count(t => t.VoteEnum == Domain.Enums.VoteEnum.Yes) > 5);
                var images = _sightingRepository.Get(expression).Select(_ => new ImageModel(_.Image)).Distinct().ToList();
               
                return images;
            }
            catch (Exception ex)
            {

                throw;
            }
          
        }

        public async Task<List<Image>> GetIncorrectSightings()
        {
            try
            {
                var expression = PredicateBuilder.New<Sighting>(true);
                expression = expression.And(_ => _.Image.IsActive);
                expression = expression.And(_ => _.Votes.Count(t => t.VoteEnum == Domain.Enums.VoteEnum.No || t.VoteEnum == Domain.Enums.VoteEnum.Unsure) >  5 );
                var images = _sightingRepository.Get(expression).Select(_ => _.Image).Distinct().ToList();

                return images;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task DeleteUnwantedSightings()
        {
            try
            {
                var expression = PredicateBuilder.New<Sighting>(true);
                expression = expression.And(_ => _.Code == 13);
                expression = expression.And(_ => _.Votes.Count() <  1);
                var images = _sightingRepository.Get(expression).Select(_ => _.Image).Distinct().ToList();
                foreach (var item in images)
                {
                    item.IsActive = false;

                    _imageRepository.Update(item);
                }

            }
            catch (Exception ex)
            {

                throw;
            }

        }


        public async Task DisableIncorrectSightings()
        {
            try
            {
                var images = await this.GetIncorrectSightings();

                foreach (var item in images)
                {
                    item.IsActive = false;

                    _imageRepository.Update(item);
                }

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public void DisableImage(int imageId)
        {
            var image = _imageRepository.GetById(imageId);
            image.IsActive = false;

            _imageRepository.Update(image);
        }
        public void EnableImage(int imageId)
        {
            var image = _imageRepository.GetById(imageId);
            image.IsActive = true;

            _imageRepository.Update(image);
        }

        public async Task<bool> UpdateAsync(ImageModel bannerModel)
        {
            //if (!string.IsNullOrWhiteSpace(bannerModel.BannerBase64))
            //{
            //    string filename = Guid.NewGuid() + ".jpg";
            //    var bannerFile = await _fileService.SaveFileAsync(filename, bannerModel.BannerBase64, "banner-images");
            //    bannerModel.BannerPath = bannerFile.Path;
            //}
            //var banner = _bannerRepository.GetById(bannerModel.BannerId);
            //banner.BannerPath = bannerModel.BannerPath;
            //banner.AdName = bannerModel.AdName;
            //banner.CompanyName = bannerModel.CompanyName;
            //banner.BannerPriorityLevel = bannerModel.BannerPriorityLevel;
            //banner.Link = bannerModel.Link;
            //banner.IsActive = bannerModel.IsActive;

            //_bannerRepository.Update(banner);
            return true;
        }


        public async Task<ImageModel> CreateAsync(ImageModel imgModel)
        {
            string filename = Guid.NewGuid() + ".jpg";
            var imageFile = await _fileService.SaveFileAsync(filename, imgModel.Base64, "abc-images");
            if (string.IsNullOrEmpty(imageFile?.Path))
            {
                return null;
            }

            var img = new Image();
            img.ImagePath = imageFile.Path;
            img.IsActive = true;
            img.Height = 1440;
            img.Width = 2560;

            img.CreatedByUser = "Test";
            img.ModifiedByUser = "Test";
            img = _imageRepository.Create(img);

            foreach(var sighting in imgModel.Sightings)
            {
                var sightingDB = new Sighting();
                sightingDB.ImageId = img.ImageId;
                sightingDB.Code = sighting.Code;
                sightingDB.Name = sighting.Name;
                sightingDB.X1 = sighting.X1;
                sightingDB.X2 = sighting.X2;
                sightingDB.Y1 = sighting.Y1;
                sightingDB.Y2 = sighting.Y2;
                sightingDB.CreatedByUser = "Test";
                sightingDB.ModifiedByUser = "Test";
                _sightingRepository.Create(sightingDB);
            }
            return new ImageModel(img);
        }

    }
}