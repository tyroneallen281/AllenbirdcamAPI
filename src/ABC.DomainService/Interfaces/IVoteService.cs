using ABC.Domain.Entities;
using ABC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ABC.DomainService.Interfaces
{
    public interface IVoteService
    {
        Task<VoteModel> CreateAsync(VoteModel model);
    }
}

