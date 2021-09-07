using System;
using System.Collections.Generic;
using System.Text;

namespace ABC.Domain.Interfaces
{
    public interface ILogicalDelete
    {
        bool IsDeleted { get; set; }
    }
}
