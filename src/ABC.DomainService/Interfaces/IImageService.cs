using ABC.Domain.Entities;
using ABC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ABC.DomainService.Interfaces
{
    public interface IImageService
    {
        ImageModel Get(int bannerId);
        ImageModel GetRandom();
        void DisableImage(int imageId);
        void EnableImage(int imageId);
        Task<List<ImageModel>> GetCorrectSightings();
        Task DisableIncorrectSightings();
        Task<bool> UpdateAsync(ImageModel bannerModel);
        Task<ImageModel> CreateAsync(ImageModel bannerModel);
        Task DeleteUnwantedSightings();
    }
}

