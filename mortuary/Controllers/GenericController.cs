using mortuary.Models;
using System;
using System.Data.Entity;
using System.IdentityModel;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace mortuary.Controllers
{
    public abstract class GenericController<TEntity> : InternationalizationController where TEntity : class
    {
        protected ApplicationDbContext Context { get; }
        protected DbSet<TEntity> DbSet { get; }
        protected object Locker { get; }

        public GenericController()
        {
            Context = new ApplicationDbContext();
            DbSet = Context.Set<TEntity>();
            Locker = new object();
        }

        public virtual ActionResult Index()
        {
            return View(DbSet.ToList());
        }

        public virtual ActionResult Details(int? id)
        {
            return ProcessView(id, false);
        }

        protected ActionResult ProcessView(int? id, bool populateViewBag)
        {
            try
            {
                TEntity entity = GetEntity(id);
                if (populateViewBag)
                    PopulateViewBag();
                return View(entity);
            }
            catch (ArgumentNullException)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catch (EntryPointNotFoundException)
            {
                return HttpNotFound();
            }
        }

        private TEntity GetEntity(int? id)
        {
            if (id == null)
                throw new ArgumentNullException();
            TEntity entity = DbSet.Find(id);
            if (entity == null)
                throw new EntryPointNotFoundException();
            return entity;
        }

        public ActionResult Create()
        {
            PopulateViewBag();
            return View();
        }

        protected abstract void PopulateViewBag();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(TEntity entity)
        {
            return CreateOrUpdate(entity, false);
        }

        protected virtual ActionResult CreateOrUpdate(TEntity entity, bool updating)
        {
            try
            {
                lock (Locker)
                    PerformAction(entity, updating);
                return RedirectToAction("Index");
            }
            catch (BadRequestException)
            {
                PopulateViewBag();
                return View(entity);
            }
        }

        protected void PerformAction(TEntity entity, bool updating)
        {
            Prepare(entity, updating);
            CheckModelState();
            if (updating)
                Context.Entry(entity).State = EntityState.Modified;
            else
                DbSet.Add(entity);
            Context.SaveChanges();
        }

        private void CheckModelState()
        {
            if (!ModelState.IsValid)
                throw new BadRequestException();
        }

        protected abstract void Prepare(TEntity entity, bool updating);

        public ActionResult Edit(int? id)
        {
            return ProcessView(id, true);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TEntity entity)
        {
            return CreateOrUpdate(entity, true);
        }

        public virtual ActionResult Delete(int? id)
        {
            return ProcessView(id, false);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(int id)
        {
            lock (Locker)
            {
                TEntity entity = DbSet.Find(id);
                try
                {
                    CheckValidDeletion(entity);
                    DbSet.Remove(entity);
                    Context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (InvalidOperationException ex)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
                }
            }
        }

        protected abstract void CheckValidDeletion(TEntity entity);

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                Context.Dispose();
            base.Dispose(disposing);
        }
    }
}