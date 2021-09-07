using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ABC.Domain.Common
{
    public abstract class AuditBase : IAuditableEntity
    {
        [Required]
        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }

        [Required]
        [Display(Name = "Date Modified")]
        public DateTime DateModified { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Created By")]
        public string CreatedByUser { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Modified By")]
        public string ModifiedByUser { get; set; }
    }
}
