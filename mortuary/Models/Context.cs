using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Linq;

namespace mortuary.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Provider> Providers { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<ClientBill> ClientBills { get; set; }
        public virtual DbSet<ProviderBill> ProviderBills { get; set; }
        public virtual DbSet<BillLine> BillLines { get; set; }
        public virtual DbSet<Bill> Bills { get; set; }

        public ApplicationDbContext() : base("Context", throwIfV1Schema: false) { }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasRequired(p => p.Provider).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Client>().HasRequired(c => c.Vendor).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Client>().HasMany(c => c.Products).WithMany();
        }

        public void CreateRole(RoleManager<IdentityRole> manager, string roleName)
        {
            if (!Roles.Any(r => r.Name == roleName.ToString()))
            {
                var role = new IdentityRole { Name = roleName.ToString() };
                manager.Create(role);
            }
        }

        public void CreateUser(string email, string passwordHash, string roleName)
        {
            var user = new ApplicationUser { Email = email, PasswordHash = passwordHash};
            CreateUser(user, roleName);
        }

        public void CreateUser(ApplicationUser user, string roleName = RoleNames.NORMAL)
        {
            if (!Users.Any(u => u.Email == user.Email))
            {
                user.UserName = user.Email;
                user.EmailConfirmed = true;
                var store = new UserStore<ApplicationUser>(this);
                var manager = new UserManager<ApplicationUser>(store);
                manager.Create(user);
                manager.AddToRole(user.Id, roleName.ToString());
            }
        }

        public void ChangeUserRole(ApplicationUser user, string newRole)
        {
            var store = new UserStore<ApplicationUser>(this);
            var manager = new UserManager<ApplicationUser>(store);
            var roleToRemove = Roles.Find(user.Roles.First().RoleId);
            manager.RemoveFromRole(user.Id, roleToRemove.Name);
            manager.AddToRole(user.Id, newRole);
        }
    }
}
