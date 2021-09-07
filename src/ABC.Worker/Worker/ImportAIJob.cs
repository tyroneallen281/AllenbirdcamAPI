using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    public class ImportAIJob : IImportAIJob
    {
        public string[] labelMapItems = { "Collared Dove", "Burchells Coucal", "Glossy Starling", "Green Woodhoopoe", "Genet", "Red Winged Starling", "Hadeda", "Bush Baby", "Hornbill", "Grey Lourie", "Black Collared Barbet", "Weaver", "Lovebird", "Mousebird", "Fruit Bat", "Arrow marked babbler", "Dark capped bulbul", "Sparrow", "Cut throat finch", "Karoo thrush", "White eye", "Speckled pigeon", "Southern Red Bishop", "Laughing dove", "Southern boubou", "Blue Budgie", "White-winged Widowbird", "Grey-headed Bushhsrike", "Golden-tailed woodpecker", "Wattled starling", "Crested barbet", "Pied Crow", "Ring-necked parakeets" };
        public string[] igroneList = { "1.0", "3.0", "4.0", "6.0", "8.0", "12.0", "13.0", "14.0", "18.0", "15.0" , "24.0", "20.0", "11.0", "22.0" };
        public static string directory = @"F:\AllenBirdCam\AI\Crowdsource Import";

        private readonly IImageService _imageService;

        public ImportAIJob(IImageService imageService)
        {
            _imageService = imageService;
        }
      
    
        public async Task ImportFolderImagesAsync()
        {
            string[] filePaths = Directory.GetFiles(directory, "*.json");
            foreach (var file in filePaths)
            {
               await  ImportImageFileAsync(file);
            }
        }

        public async Task ImportImageFileAsync(string file)
        {
            try
            {
                string text = File.ReadAllText(file);

                var sightingList = JsonConvert.DeserializeObject<List<FileSightingModel>>(text);
                
                var imageModel = new ImageModel()
                {
                    //Base64 = base64ImageRepresentation,
                    Height = 1440,
                    Width = 2560,
                };
                imageModel.Sightings = new List<SightingModel>();
                foreach (var sighting in sightingList)
                {
                    if (igroneList.Any(_ => _ == sighting.Code))
                    {
                        continue;
                    }
                    var sightingModel = new SightingModel()
                    {
                        Code = Convert.ToInt32(Convert.ToDouble(sighting.Code)),
                        Name = labelMapItems[Convert.ToInt32(Convert.ToDouble(sighting.Code)) - 1],
                        X1 = Convert.ToInt32(sighting.X1 * imageModel.Width),
                        X2 = Convert.ToInt32(sighting.X2 * imageModel.Width),
                        Y1 = Convert.ToInt32(sighting.Y1 * imageModel.Height),
                        Y2 = Convert.ToInt32(sighting.Y2 * imageModel.Height),
                    };
                    imageModel.Sightings.Add(sightingModel);
                }
                if (imageModel.Sightings.Any())
                {
                    var filename = Path.GetFileName(file);
                    var imagePath = Path.Combine(directory, filename.Replace(".json", ".jpg"));
                    byte[] imageArray = System.IO.File.ReadAllBytes(imagePath);
                    string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                    imageModel.Base64 = base64ImageRepresentation;
                    await _imageService.CreateAsync(imageModel);

                }
              
            }
            catch (Exception ex)
            {


            }

        }
    }
}