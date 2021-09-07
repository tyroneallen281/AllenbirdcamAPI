using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ABC.Domain.Models;
using ABC.DomainService.Interfaces;
using ABC.Worker.Interfaces;
using ABC.Worker.Models;
using Newtonsoft.Json;
using Quartz;

namespace ABC.Worker.Worker
{
    public class OutputAIJob : IOutputAIJob
    {
        public string[] labelMapItems = { "Collared Dove", "Burchells Coucal", "Glossy Starling", "Green Woodhoopoe", "Genet", "Red Winged Starling", "Hadeda", "Bush Baby", "Hornbill", "Grey Lourie", "Black Collared Barbet", "Weaver", "Lovebird", "Mousebird", "Fruit Bat", "Arrow marked babbler", "Dark capped bulbul", "Sparrow", "Cut throat finch", "Karoo thrush", "White eye", "Speckled pigeon", "Southern Red Bishop", "Laughing dove", "Southern boubou", "Blue Budgie", "White-winged Widowbird", "Grey-headed Bushhsrike", "Golden-tailed woodpecker", "Wattled starling" };
        public static string baseImgUrl  = @"https://lmsblob.blob.core.windows.net/";
        public static string outputDirectory = @"F:\tensorflow1\models\research\object_detection\images\train";

        private readonly IImageService _imageService;

        public OutputAIJob(IImageService imageService)
        {
            _imageService = imageService;
        }

        public async Task OutputValidSigntings()
        {
            //await _imageService.DeleteUnwantedSightings();
            await _imageService.DisableIncorrectSightings();
            var validImageList = await  _imageService.GetCorrectSightings();
            foreach (var image in validImageList.Where(_ => _.Sightings.Any(s => s.PassedVotes)))
            {
                try
                {
                    saveOutputItems(image);
                    _imageService.DisableImage(image.ImageId);
                }
                catch (Exception ex)
                {

                }
               
            }
        }
        
         
        public void saveOutputItems(ImageModel image)
        {
            var imagePath = Path.Combine(outputDirectory, $"ABCAI_{image.ImageId}.jpg");
            SaveImage(baseImgUrl+image.ImagePath, imagePath);
            generateXmlFile(image);
        }
        public void generateXmlFile(ImageModel image)
        {
            try
            {
                var filePath = Path.Combine(outputDirectory, $"ABCAI_{image.ImageId}.xml");
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                File.WriteAllText(filePath, generateXmlString(image));
            }
            catch (Exception ex)
            {

            }

        }

        public static byte[] GetImage(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();

            using (Stream dataStream = response.GetResponseStream())
            {
                if (dataStream == null)
                    return null;
                using (var sr = new BinaryReader(dataStream))
                {
                    byte[] bytes = sr.ReadBytes(100000000);

                    return bytes;
                }
            }

            return null;
        }

        public static void SaveImage(string imgUrl, string path)
        {
            byte[] image = GetImage(imgUrl);
            using (var ms = new MemoryStream(image))
            {

                Directory.CreateDirectory(Path.GetDirectoryName(path));

                using (FileStream file = new FileStream(path, FileMode.Create, System.IO.FileAccess.Write))
                {
                    ms.CopyTo(file);
                }

            }

        }

        public string generateXmlString(ImageModel image)
        {
			var xml = $" <annotation>" +
					  "		<folder>train</folder>" +
                      $"	<filename>ABCAI_{image.ImageId}.jpg</filename>" +
                      $"	<path>{outputDirectory}\\ABCAI_{image.ImageId}.jpg</path>" +
                      "		<source>" +
					  "			<database>Unknown</database>" +
					  "		</source>" +
					  "		<size>" +
					  $"			<width>{image.Width}</width>" +
					  $"			<height>{image.Height}</height>" +
					  "			<depth>3</depth>" +
					  "		</size>" +
					  "		<segmented>0</segmented>";
            foreach (var sighting in image.Sightings.Where(s => s.PassedVotes))
            {
                xml += $"		<object>" +
					   $"			<name>{sighting.Name}</name>" +
					   $"			<pose>Unspecified</pose>" +
					   $"			<truncated>0</truncated>" +
					   $"			<difficult>0</difficult>" +
					   $"			<bndbox>" +
					   $"				<xmin>{sighting.X1}</xmin>" +
					   $"				<ymin>{sighting.Y1}</ymin>" +
					   $"				<xmax>{sighting.X2}</xmax>" +
					   $"				<ymax>{sighting.Y2}</ymax>" +
					   $"			</bndbox>" +
					   $"		</object>";
			}

			xml += "</annotation>";

			return xml;
		}
    }
}