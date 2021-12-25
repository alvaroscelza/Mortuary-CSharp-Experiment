using Microsoft.Ajax.Utilities;
using mortuary.Models;
using System;
using System.Data.Entity;
using System.IdentityModel;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace mortuary.Controllers
{
    public class BillLinesController : GenericController<BillLine>
    {
        public float OldBillLineSubtotal { get; set; }

        public override ActionResult Index()
        {
            throw new NotImplementedException("Bill lines are not supposed to be listed without a bill.");
        }

        public override ActionResult Details(int? id)
        {
            throw new NotImplementedException("Bill lines are not supposed to be detailed.");
        }

        protected override ActionResult CreateOrUpdate(BillLine entity, bool updating)
        {
            try
            {
                lock (Locker)
                {
                    SaveOldSubtotalWhenUpdating(entity, updating);
                    PerformAction(entity, updating);
                    Bill bill = GetBill(entity);
                    UpdateBillTotalToPay(entity, bill, updating);
                    return RedirectToBillEditView(entity, bill);
                }
            }
            catch (BadRequestException)
            {
                PopulateViewBag();
                return View(entity);
            }
        }

        private void SaveOldSubtotalWhenUpdating(BillLine entity, bool updating)
        {
            if (updating)
            {
                var storedEntity = Context.BillLines.Find(entity.Id);
                OldBillLineSubtotal = storedEntity.Subtotal;
                Context.Entry(storedEntity).State = EntityState.Detached;
            }
        }

        private Bill GetBill(BillLine entity)
        {
            return Context.Bills.Find(entity.BillId);
        }

        private void UpdateBillTotalToPay(BillLine entity, Bill bill, bool updating, bool deleting=false)
        {
            if (updating)
            {
                bill.TotalToPay -= OldBillLineSubtotal;
                bill.TotalToPay += entity.Subtotal;
            }
            else if (deleting)
                bill.TotalToPay -= entity.Subtotal;
            else
                bill.TotalToPay += entity.Subtotal;
            Context.SaveChanges();
        }

        private ActionResult RedirectToBillEditView(BillLine entity, Bill bill)
        {
            if (typeof(ClientBill).IsAssignableFrom(bill.GetType()))
                return RedirectToAction("Edit", "ClientBills", new { id = entity.BillId });
            else
                return RedirectToAction("Edit", "ProviderBills", new { id = entity.BillId });
        }

        protected override void PopulateViewBag()
        {
            ViewBag.Products = new SelectList(Context.Products, "Id", "Name");
            ViewBag.BillId = int.Parse(Request.QueryString["billId"]);
            ViewBag.PreviousURL = Request.UrlReferrer.AbsoluteUri;
        }

        public override ActionResult Delete(int? id)
        {
            ViewBag.PreviousURL = Request.UrlReferrer.AbsoluteUri;
            return ProcessView(id, false);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public override ActionResult DeleteConfirmed(int id)
        {
            lock (Locker)
            {
                BillLine entity = DbSet.Find(id);
                try
                {
                    CheckValidDeletion(entity);
                    DbSet.Remove(entity);
                    Context.SaveChanges();
                    Bill bill = GetBill(entity);
                    UpdateBillTotalToPay(entity, bill, false, true);
                    return RedirectToBillEditView(entity, bill);
                }
                catch (InvalidOperationException ex)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
                }
            }
        }

        protected override void CheckValidDeletion(BillLine entity) { }

        protected override void Prepare(BillLine entity, bool updating) { }
    }
}
