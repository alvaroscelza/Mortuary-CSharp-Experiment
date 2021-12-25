using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace mortuary.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Display(Name = "Name", ResourceType = typeof(i18n.i18n))]
        [Required(ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "Required")]
        [StringLength(100, ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "OneHundredTooLongError")]
        public string Name { get; set; }

        [Display(Name = "Document", ResourceType = typeof(i18n.i18n))]
        public int? Document { get; set; }

        [Display(Name = "Phone", ResourceType = typeof(i18n.i18n))]
        [StringLength(50, ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "FiftyTooLongError")]
        public string Phone { get; set; }

        [Display(Name = "BirthDate", ResourceType = typeof(i18n.i18n))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Address", ResourceType = typeof(i18n.i18n))]
        [StringLength(50, ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "FiftyTooLongError")]
        public string Address { get; set; }
        
        [Display(Name = "PaymentAddress", ResourceType = typeof(i18n.i18n))]
        [StringLength(50, ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "FiftyTooLongError")]
        public string PaymentAddress { get; set; }

        [Display(Name = "Vendor", ResourceType = typeof(i18n.i18n))]
        public virtual ApplicationUser Vendor { get; set; }
        public string VendorId { get; set; }

        [Display(Name = "Products", ResourceType = typeof(i18n.i18n))]
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<int> ProductsIds { get; set; }
    }
}