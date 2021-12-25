using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace mortuary.ViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required(ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "Required")]
        [Display(Name = "Email", ResourceType = typeof(i18n.i18n))]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required(ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "Required")]
        public string Provider { get; set; }

        [Required(ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "Required")]
        [Display(Name = "Code", ResourceType = typeof(i18n.i18n))]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "RememberThisBrowser", ResourceType = typeof(i18n.i18n))]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required(ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "Required")]
        [Display(Name = "Email", ResourceType = typeof(i18n.i18n))]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "Required")]
        [Display(Name = "Email", ResourceType = typeof(i18n.i18n))]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "Required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(i18n.i18n))]
        public string Password { get; set; }

        [Display(Name = "RememberMe", ResourceType = typeof(i18n.i18n))]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "Required")]
        [EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(i18n.i18n))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "Required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(i18n.i18n))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(i18n.i18n))]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "Required")]
        [EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(i18n.i18n))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "Required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(i18n.i18n))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(i18n.i18n))]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "Required")]
        [EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(i18n.i18n))]
        public string Email { get; set; }
    }
}
