
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SaladCart.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public string? FullName { get; set; } 
   
         public string? Address { get; set; }


        public string? EmailAddress { get; set; }

    }

}
