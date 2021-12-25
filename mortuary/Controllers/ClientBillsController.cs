using mortuary.Models;
using System.Globalization;
using System.IdentityModel;
using System.Web.Mvc;

namespace mortuary.Controllers
{
    [Authorize(Roles = RoleNames.ADMINISTRATOR)]
    public class ClientBillsController : GenericController<ClientBill>
    {
        protected override void PopulateViewBag()
        {
            ViewBag.Clients = new SelectList(Context.Clients, "Id", "Name");
            ViewBag.Products = Context.Products;
        }

        protected override void Prepare(ClientBill clientBill, bool updating)
        {
            AssignTotalToPay(clientBill);
            if (!updating)
                AssignBillLines(clientBill);
        }

        private void AssignTotalToPay(ClientBill clientBill)
        {
            clientBill.TotalToPay = float.Parse(Request.Form["total-input"], CultureInfo.InvariantCulture.NumberFormat);
        }

        private void AssignBillLines(ClientBill clientBill)
        {
            int totalRows = int.Parse(Request.Form["total-rows"]);
            for (int currentRow = 0; currentRow <= totalRows; currentRow++)
            {
                if (RowExists(currentRow))
                {
                    BillLine billLine = CreateBillLine(currentRow);
                    clientBill.BillLines.Add(billLine);
                }
            }
        }

        private bool RowExists(int currentTableRow)
        {
            string htmlProductSelectName = string.Format("row-{0}-product-select", currentTableRow);
            return Request.Form[htmlProductSelectName] != null;
        }

        private BillLine CreateBillLine(int currentTableRow)
        {
            string htmlProductSelectName = string.Format("row-{0}-product-select", currentTableRow);
            string htmlNotesInputName = string.Format("row-{0}-notes-input", currentTableRow);
            string htmlAmountInputName = string.Format("row-{0}-amount-input", currentTableRow);
            string htmlPriceInputName = string.Format("row-{0}-price-input", currentTableRow);
            string htmlTaxesInputName = string.Format("row-{0}-taxes-input", currentTableRow);
            string htmlSubtotalInputName = string.Format("row-{0}-subtotal-input", currentTableRow);
            CheckData(htmlAmountInputName, htmlPriceInputName, htmlTaxesInputName);

            int selectedProductId = int.Parse(Request.Form[htmlProductSelectName]);
            string notes = Request.Form[htmlNotesInputName];
            int amount = int.Parse(Request.Form[htmlAmountInputName]);
            float price = float.Parse(Request.Form[htmlPriceInputName], CultureInfo.InvariantCulture.NumberFormat);
            int taxes = int.Parse(Request.Form[htmlTaxesInputName]);
            float subtotal = float.Parse(Request.Form[htmlSubtotalInputName], CultureInfo.InvariantCulture.NumberFormat);

            return new BillLine { ProductId = selectedProductId, Notes = notes, Amount = amount, Price = price,
                TaxesPercentage = taxes, Subtotal = subtotal };
        }

        private void CheckData(string htmlAmountInputName, string htmlPriceInputName, string htmlTaxesInputName)
        {
            bool missingAmount = string.IsNullOrEmpty(Request.Form[htmlAmountInputName]);
            bool missingPrice = string.IsNullOrEmpty(Request.Form[htmlPriceInputName]);
            bool missingTaxes = string.IsNullOrEmpty(Request.Form[htmlTaxesInputName]);
            if (missingAmount || missingPrice || missingTaxes)
            {
                ModelState.AddModelError("BillLines", i18n.i18n.BillLineMissingRequiredField);
                throw new BadRequestException();
            }
        }
        
        protected override void CheckValidDeletion(ClientBill clientBill) { }
    }
}