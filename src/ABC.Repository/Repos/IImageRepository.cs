using ABC.Data.DatabaseContext;
using ABC.Domain.Entities;
using ABC.Domain.Models;
using ABC.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ABC.Repository.Repos
{
    public interface IImageRepository : IRepository<Image, ABCDbContext>
    {
    }
}
