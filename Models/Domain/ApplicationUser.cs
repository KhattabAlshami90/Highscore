using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Highscore.Models.Domain
{
    public class ApplicationUser:IdentityUser
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string? FirstName { get; set; }

        [MaxLength(50)]
        public string? LastName { get; set; }

        [MaxLength(250)]
        public string? Address { get; set; }
    }
}
