using mortuary.Models;
using System.IO;
using System.Web.Mvc;

namespace mortuary.Controllers
{
    [Authorize(Roles = RoleNames.ADMINISTRATOR)]
    public class ProductsController : GenericController<Product>
    {
        protected override void PopulateViewBag()
        {
            ViewBag.Providers = new SelectList(Context.Providers, "Id", "Name");
        }

        protected override void Prepare(Product product, bool updating)
        {
            if (product.ImageUploader != null)
            {
                MemoryStream memoryStream = new MemoryStream();
                product.ImageUploader.InputStream.CopyTo(memoryStream);
                product.Image = memoryStream.ToArray();
            }
        }

        protected override void CheckValidDeletion(Product product) { }
    }
}
