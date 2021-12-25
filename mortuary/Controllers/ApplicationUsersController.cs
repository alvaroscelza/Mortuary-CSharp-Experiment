using mortuary.Models;
using mortuary.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace mortuary.Controllers
{
    [Authorize(Roles = RoleNames.ADMINISTRATOR)]
    public class ApplicationUsersController : InternationalizationController
    {
        protected object Locker { get; } = new object();
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ApplicationUsers
        public ActionResult Index()
        {
            var users = db.Users.ToList();
            var usersViewModels = new List<UserViewModel>();
            foreach (var user in users)
            {
                var userViewModel = new UserViewModel(user);
                usersViewModels.Add(userViewModel);
            }
            return View(usersViewModels);
        }

        // GET: ApplicationUsers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
                return HttpNotFound();
            var userViewModel = new UserViewModel(applicationUser);
            return View(userViewModel);
        }

        // GET: ApplicationUsers/Create
        public ActionResult Create()
        {
            PopulateViewBag();
            return View();
        }

        private void PopulateViewBag()
        {
            ViewBag.Roles = new SelectList(db.Roles, "Id", "Name");
        }

        // POST: ApplicationUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Email,Name")] ApplicationUser applicationUser)
        {
            string roleId = Request.Form["Role"];
            var selectedRole = db.Roles.Find(roleId);
            if (selectedRole != null)
            {
                if (ModelState.IsValid)
                {
                    db.CreateUser(applicationUser, selectedRole.Name);
                    return RedirectToAction("Index");
                }
            }
            PopulateViewBag();
            return View(applicationUser);
        }

        // GET: ApplicationUsers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
                return HttpNotFound();
            PopulateViewBag();
            return View(applicationUser);
        }

        // POST: ApplicationUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Name,Email")] ApplicationUser applicationUser)
        {
            string userId = Request.Form["Id"];
            string roleId = Request.Form["Role"];
            var selectedRole = db.Roles.Find(roleId);
            var applicationUserToUpdate = db.Users.Find(userId);
            if (selectedRole != null && applicationUserToUpdate != null)
            {
                db.ChangeUserRole(applicationUserToUpdate, selectedRole.Name);
                applicationUserToUpdate.Name = applicationUser.Name;
                applicationUserToUpdate.Email = applicationUser.Email;
                db.Entry(applicationUserToUpdate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            PopulateViewBag();
            return View(applicationUser);
        }

        // GET: ApplicationUsers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
                return HttpNotFound();
            return View(applicationUser);
        }

        // POST: ApplicationUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            lock (Locker)
            {
                ApplicationUser applicationUser = db.Users.Find(id);
                try
                {
                    CheckValidDeletion(applicationUser);
                    db.Users.Remove(applicationUser);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (InvalidOperationException ex)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
                }
            }
        }

        protected void CheckValidDeletion(ApplicationUser applicationUser)
        {
            if (db.Clients.Where(p => p.Vendor.Id == applicationUser.Id).Count() > 0)
                throw new InvalidOperationException("There are clients with this user as vendor.");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}
