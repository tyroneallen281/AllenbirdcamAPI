using ABC.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ABC.Domain.Models
{
    public class ImageModel
    {
        public ImageModel()
        {
        }

        public ImageModel(Image image)
        {
            this.ImageId = image.ImageId;
            this.ImagePath = image.ImagePath;
            this.Sightings = image.Sightings.Select(_ => new SightingModel(_)).ToList();
        }
        public string Base64 { get; set; }
        public int ImageId { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string ImagePath { get; set; }
      
        public List<SightingModel> Sightings { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}