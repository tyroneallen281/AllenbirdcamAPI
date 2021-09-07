namespace ABC.Domain.Entities
{
    using ABC.Domain.Common;
    using ABC.Domain.Enums;
    using ABC.Domain.Interfaces;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

 
    public class Vote : AuditBase, ILogicalDelete
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VoteId { get; set; }

        public VoteEnum VoteEnum { get; set; }

        public string IPAddress { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int? SightingId { get; set; }
        public virtual Sighting Sighting { get; set; }
        public bool IsDeleted { get; set; }
    }
}