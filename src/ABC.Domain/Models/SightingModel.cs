using ABC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ABC.Domain.Models
{
    public class SightingModel
    {
        public SightingModel()
        {
        }

        public SightingModel(Sighting sighting)
        {
            this.SightingId = sighting.SightingId;
            this.Code = sighting.Code;
            this.Name = sighting.Name;
            this.X1 = sighting.X1;
            this.X2 = sighting.X2;
            this.Y1 = sighting.Y1;
            this.Y2 = sighting.Y2;
            this.ImgWidth = sighting.Image.Width;
            this.ImgHeight = sighting.Image.Height;
            this.PassedVotes = sighting.Votes?.Count(_ => _.VoteEnum == Enums.VoteEnum.Yes) > 5;
        }

        public int SightingId { get; set; }

        public int Code { get; set; }

        public string Name { get; set; }

        public int X1 { get; set; }
        public int X2 { get; set; }
        public int Y1 { get; set; }
        public int Y2 { get; set; }
        public int? ImageId { get; set; }
        public virtual Image Image { get; set; }
        public virtual List<Vote> Votes { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool PassedVotes { get; set; }

        public int ImgWidth { get; set; }
        public int ImgHeight { get; set; }
        public string X { 
            get
            {
                double val = (double)X1 / (double)ImgWidth * 100;
                return $"{Convert.ToInt32(val)}%";
            }
        }
        public string Y
        {
            get
            {
                double val = (double)Y1 / (double)ImgHeight * 100;
                return $"{Convert.ToInt32(val)}%";
            }
        }
        public string Width
        {
            get
            {
                double val =(double)(X2 - X1) / ImgWidth * 100;
                return $"{Convert.ToInt32(val)}%";
            }
        }
        public string Height
        {
            get
            {
                double val = (double)(Y2 - Y1) / ImgHeight * 100;
                return $"{Convert.ToInt32(val)}%";
            }
        }
    }
}