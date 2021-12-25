using mortuary.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace mortuary.Controllers
{
    [Authorize(Roles = RoleNames.ADMINISTRATOR)]
    public class ProvidersController : GenericController<Provider>
    {
        protected override void PopulateViewBag() {}

        protected override void Prepare(Provider provider, bool updating) { }

        protected override void CheckValidDeletion(Provider provider)
        {
            if (Context.Products.Where(p => p.Provider.Id == provider.Id).Count() > 0)
                throw new InvalidOperationException("There are products with this provider.");
        }
    }
}
