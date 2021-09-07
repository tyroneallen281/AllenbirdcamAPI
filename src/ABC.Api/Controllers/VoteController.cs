using ABC.Api.Models;
using ABC.Domain.Entities;
using ABC.Domain.Models;
using ABC.DomainService.Interfaces;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ABC.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : BaseApiController
    {
        private readonly IVoteService _voteService;
        public VoteController(IVoteService voteService) 
        {
            _voteService = voteService;
        }

        [HttpPost]
        public async Task<VoteModel> Post(VotePostModel vote)
        {
            var model = new VoteModel()
            {
                SightingId = vote.SightingId,
                VoteEnum = vote.Vote,
                IPAddress = vote.IPAddress,
                City = vote.City,
                Country = vote.Country
            };
            return await  _voteService.CreateAsync(model);
        }

    }
}