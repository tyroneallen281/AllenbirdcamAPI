using ABC.Domain.Entities;
using System.Collections.Generic;

namespace ABC.Domain.Models
{
    public class FileModel
    {
        public FileModel()
        {
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string ContentType { get; set; }
        public int Size { get; set; }
        public string Base64 { get; set; }
    }
}