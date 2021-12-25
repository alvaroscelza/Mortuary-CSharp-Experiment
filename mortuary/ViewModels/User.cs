using Microsoft.AspNet.Identity.EntityFramework;
using mortuary.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace mortuary.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel(ApplicationUser user)
        {
            Id = user.Id;
            Name = user.Name;
            Email = user.Email;
            EmailConfirmed = user.EmailConfirmed;
            LockoutEnabled = user.LockoutEnabled;
            AccessFailedCount = user.AccessFailedCount;

            var userRole = user.Roles.FirstOrDefault();
            if(userRole != null)
            {
                ApplicationDbContext db = new ApplicationDbContext();
                Role = db.Roles.Find(userRole.RoleId);
            }
    }

        public string Id { get; set; }

        [Display(Name = "Name", ResourceType = typeof(i18n.i18n))]
        public string Name { get; set; }
        
        public string Email { get; set; }

        [Display(Name = "EmailConfirmed", ResourceType = typeof(i18n.i18n))]
        public bool EmailConfirmed { get; set; }

        [Display(Name = "LockoutEnabled", ResourceType = typeof(i18n.i18n))]
        public bool LockoutEnabled { get; set; }

        [Display(Name = "AccessFailedCount", ResourceType = typeof(i18n.i18n))]
        public int AccessFailedCount { get; set; }

        [Display(Name = "Role", ResourceType = typeof(i18n.i18n))]
        public IdentityRole Role { get; }
    }
}