using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace IPAddressManagement.Models
{
    public abstract class AuditableEntity
    {
        public DateTime CreatedAt { get; set; }
        [BindNever]
        [ValidateNever]
        public string CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }
        [BindNever]
        [ValidateNever]
        public string UpdatedBy { get; set; }
    }
}
