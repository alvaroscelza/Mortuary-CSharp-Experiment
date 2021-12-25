namespace mortuary.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using mortuary.Models;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "mortuary.Models.ApplicationDbContext";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var store = new RoleStore<IdentityRole>(context);
            var manager = new RoleManager<IdentityRole>(store);
            context.CreateRole(manager, RoleNames.ADMINISTRATOR);
            context.CreateRole(manager, RoleNames.NORMAL);
            var adminPassword = "ABEAu0ePGuXXH1mcLZtgXRjkI/DqVMT8PoEC1C2nWKaIPnibQjCFofjKRjgMSACcgA==";
            context.CreateUser("alvaroscelza@gmail.com", adminPassword, RoleNames.ADMINISTRATOR);
        }
    }
}
