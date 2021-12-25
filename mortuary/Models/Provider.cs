using System.ComponentModel.DataAnnotations;

namespace mortuary.Models
{
    public class Provider
    {
        public int Id { get; set; }

        [Display(Name = "Name", ResourceType = typeof(i18n.i18n))]
        [Required(ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "Required")]
        [StringLength(50, ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "FiftyTooLongError")]
        public string Name { get; set; }
        
        [Display(Name = "CompanyName", ResourceType = typeof(i18n.i18n))]
        [StringLength(50, ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "FiftyTooLongError")]
        public string CompanyName { get; set; }

        [Display(Name = "Address", ResourceType = typeof(i18n.i18n))]
        [StringLength(100, ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "OneHundredTooLongError")]
        public string Address { get; set; }

        [Display(Name = "Phone", ResourceType = typeof(i18n.i18n))]
        [StringLength(50, ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "FiftyTooLongError")]
        public string Phone { get; set; }
        
        [Display(Name = "BankAccountInformation", ResourceType = typeof(i18n.i18n))]
        [StringLength(100, ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "OneHundredTooLongError")]
        public string BankAccountInformation { get; set; }

        [Display(Name = "Notes", ResourceType = typeof(i18n.i18n))]
        [StringLength(200, ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "TwoHundredTooLongError")]
        public string Notes { get; set; }
    }
}