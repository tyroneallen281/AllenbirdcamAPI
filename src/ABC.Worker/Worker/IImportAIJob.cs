using ABC.Domain.Entities;
using ABC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Worker.Interfaces
{
    public interface IImportAIJob
    {
        Task ImportFolderImagesAsync();
    }
}

