using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Web;

namespace mortuary.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Display(Name = "Name", ResourceType = typeof(i18n.i18n))]
        [Required(ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "Required")]
        [StringLength(50, ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "FiftyTooLongError")]
        public string Name { get; set; }

        [Display(Name = "Code", ResourceType = typeof(i18n.i18n))]
        [StringLength(50, ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "FiftyTooLongError")]
        public string Code { get; set; }

        [Display(Name = "SellPrice", ResourceType = typeof(i18n.i18n))]
        public int SellPrice { get; set; }
        
        [Display(Name = "BuyPrice", ResourceType = typeof(i18n.i18n))]
        public int BuyPrice { get; set; }

        [Display(Name = "ClientTaxesPercentage", ResourceType = typeof(i18n.i18n))]
        [Range(0, 100, ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "BetweenCeroAndOneHundrerError")]
        public int ClientTaxesPercentage { get; set; }

        [Display(Name = "Description", ResourceType = typeof(i18n.i18n))]
        [StringLength(200, ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "TwoHundredTooLongError")]
        public string Description { get; set; }

        [Required(ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "Required")]
        public int ProviderId { get; set; }
        [Display(Name = "Provider", ResourceType = typeof(i18n.i18n))]
        public virtual Provider Provider { get; set; }

        [Display(Name = "Image", ResourceType = typeof(i18n.i18n))]
        public byte[] Image { get; set; } = File.ReadAllBytes(HttpContext.Current.Server.MapPath("~/Media/default_image.png"));
        
        [Display(Name = "ImageUploader", ResourceType = typeof(i18n.i18n))]
        [NotMapped]
        [ImageValidator(ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "NotAnImageError")]
        public HttpPostedFileBase ImageUploader { get; set; }
        private class ImageValidator : RequiredAttribute
        {
            public override bool IsValid(object value)
            {
                if (value != null)
                {
                    HttpPostedFileBase image = value as HttpPostedFileBase;
                    return image.ContentType.Contains("image");
                }
                return true;
            }
        }
    }
}