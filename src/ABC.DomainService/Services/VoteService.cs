using LinqKit;
using ABC.Domain.Entities;
using ABC.Domain.Models;
using ABC.DomainService.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using ABC.Repository.Repos;

namespace ABC.DomainService.Services
{
    public sealed class VoteService : IVoteService
    {
       private readonly IVoteRepository _voteRepository;
        private readonly IConfiguration Configuration;

        public VoteService(IVoteRepository voteRepository)
        {
            _voteRepository = voteRepository;
         //   Configuration = configuration;
        }

        public async Task<VoteModel> CreateAsync(VoteModel model)
        {

            var vote = new Vote();
            vote.SightingId = model.SightingId;
            vote.VoteEnum = model.VoteEnum;
            vote.IPAddress = model.IPAddress;
            vote.City = model.City;
            vote.Country = model.Country;
            vote.CreatedByUser = "Test";
            vote.ModifiedByUser = "Test";
            vote = _voteRepository.Create(vote);
            return new VoteModel(vote);
        }

    }
}