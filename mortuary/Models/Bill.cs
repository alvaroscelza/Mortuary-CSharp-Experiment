using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace mortuary.Models
{
    public abstract class Bill
    {
        public int Id { get; set; }

        [Display(Name = "Date", ResourceType = typeof(i18n.i18n))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "Required")]
        public DateTime Date { get; set; } = DateTime.Now;

        [Display(Name = "TotalToPay", ResourceType = typeof(i18n.i18n))]
        public float TotalToPay { get; set; }

        [Display(Name = "Notes", ResourceType = typeof(i18n.i18n))]
        [StringLength(100, ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "OneHundredTooLongError")]
        public string Notes { get; set; }

        [Display(Name = "BillLines", ResourceType = typeof(i18n.i18n))]
        [Required(ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "Required")]
        public virtual ICollection<BillLine> BillLines { get; set; } = new List<BillLine>();
    }

    public class ClientBill : Bill
    {
        [Required(ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "Required")]
        public int ClientId { get; set; }
        [Display(Name = "Client", ResourceType = typeof(i18n.i18n))]
        public virtual Client Client { get; set; }
    }

    public class ProviderBill : Bill
    {
        [Required(ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "Required")]
        public int ProviderId { get; set; }
        [Display(Name = "Provider", ResourceType = typeof(i18n.i18n))]
        public virtual Provider Provider { get; set; }
    }

    public class BillLine
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "Required")]
        public int ProductId { get; set; }
        [Display(Name = "Product", ResourceType = typeof(i18n.i18n))]
        public virtual Product Product { get; set; }

        [Display(Name = "Notes", ResourceType = typeof(i18n.i18n))]
        [StringLength(100, ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "OneHundredTooLongError")]
        public string Notes { get; set; }

        [Display(Name = "Amount", ResourceType = typeof(i18n.i18n))]
        [Required(ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "Required")]
        [Range(1, int.MaxValue, ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "MinOneError")]
        public int Amount { get; set; }

        [Display(Name = "Price", ResourceType = typeof(i18n.i18n))]
        [Required(ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "Required")]
        public float Price { get; set; }
        
        [Display(Name = "TaxesPercentage", ResourceType = typeof(i18n.i18n))]
        [Range(0, 100, ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "BetweenCeroAndOneHundrerError")]
        [Required(ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "Required")]
        public int TaxesPercentage { get; set; }

        [Display(Name = "Subtotal", ResourceType = typeof(i18n.i18n))]
        [Required(ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "Required")]
        public float Subtotal { get; set; }

        [Required(ErrorMessageResourceType = typeof(i18n.i18n), ErrorMessageResourceName = "Required")]
        public int BillId { get; set; }
        [Display(Name = "Bill", ResourceType = typeof(i18n.i18n))]
        public virtual Bill Bill { get; set; }
    }
}