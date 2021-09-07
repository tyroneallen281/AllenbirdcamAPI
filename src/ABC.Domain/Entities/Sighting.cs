namespace ABC.Domain.Entities
{
    using ABC.Domain.Common;
    using ABC.Domain.Interfaces;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

 
    public class Sighting : AuditBase, ILogicalDelete
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
    }
}