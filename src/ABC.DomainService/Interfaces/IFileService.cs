using ABC.Domain.Entities;
using ABC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ABC.DomainService.Interfaces
{
    public interface IFileService
    {
        Task<FileModel> SaveFileAsync(string name, string base64, string container);
    }
}

