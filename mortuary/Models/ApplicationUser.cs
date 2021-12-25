using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace mortuary.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "Required")]
        [StringLength(50, ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "FiftyTooLongError")]
        public override string Email { get; set; }
        
        [Display(Name = "EmailConfirmed", ResourceType = typeof(i18n.i18n))]
        public override bool EmailConfirmed { get; set; }
        
        [Display(Name = "LockoutEnabled", ResourceType = typeof(i18n.i18n))]
        public override bool LockoutEnabled { get; set; }
        
        [Display(Name = "AccessFailedCount", ResourceType = typeof(i18n.i18n))]
        public override int AccessFailedCount { get; set; }

        [Display(Name = "Name", ResourceType = typeof(i18n.i18n))]
        [Required(ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "Required")]
        [StringLength(50, ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "FiftyTooLongError")]
        public string Name { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            ClaimsIdentity userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}