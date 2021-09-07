namespace ABC.Domain.Entities
{
    using ABC.Domain.Common;
    using ABC.Domain.Interfaces;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

 
    public class Image : AuditBase, ILogicalDelete
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImageId { get; set; }

        public string ImagePath { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public virtual List<Sighting> Sightings { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}