using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JobSearch.Domain.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Display(Name="Name", ResourceType = typeof(JobSearch.Domain.Utility.Language.Fields))]
        [Required(ErrorMessageResourceType = typeof(JobSearch.Domain.Utility.Language.Messages), ErrorMessageResourceName = "MSG_E001")]
        public string Name { get; set; }

        [Display(Name = "Email", ResourceType = typeof(JobSearch.Domain.Utility.Language.Fields))]
        
        public string Email { get; set; }

        [Display(Name = "Password", ResourceType = typeof(JobSearch.Domain.Utility.Language.Fields))]
        [Required(ErrorMessageResourceType = typeof(JobSearch.Domain.Utility.Language.Messages), ErrorMessageResourceName = "MSG_E003")]
        
        public string Password { get; set; }
    }
}
