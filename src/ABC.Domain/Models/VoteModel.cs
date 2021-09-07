using ABC.Domain.Entities;
using ABC.Domain.Enums;
using System.Collections.Generic;

namespace ABC.Domain.Models
{
    public class VoteModel
    {
        public VoteModel()
        {
        }

        public VoteModel(Vote vote)
        {
            this.VoteId = vote.VoteId;
            this.VoteEnum = vote.VoteEnum;
            this.IPAddress = vote.IPAddress;
            this.City = vote.City;
            this.Country = vote.Country;
        }

        public int VoteId { get; set; }

        public VoteEnum VoteEnum { get; set; }
        public int? SightingId { get; set; }
        public string IPAddress { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public virtual Sighting Sighting { get; set; }
        public bool IsDeleted { get; set; }
    }
}