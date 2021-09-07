using System;
using System.Collections.Generic;
using System.Text;

namespace ABC.Domain.Common
{
    public interface IAuditableEntity
    {
        DateTime DateCreated { get; set; }

        DateTime DateModified { get; set; }

        string CreatedByUser { get; set; }

        string ModifiedByUser { get; set; }
    }
}
