using mortuary.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace mortuary.Controllers
{
    [Authorize(Roles = RoleNames.ADMINISTRATOR)]
    public class ClientsController : GenericController<Client>
    {
        protected override void PopulateViewBag()
        {
            ViewBag.Vendors = new SelectList(Context.Users, "Id", "Name");
            ViewBag.Products = new SelectList(Context.Products, "Id", "Name");
        }

        protected override void Prepare(Client client, bool updating)
        {
            if (updating)
                UpdateClientProducts(client.Id, client.ProductsIds);
            else
                AddMissingProducts(client, client.ProductsIds);
        }

        private void AddMissingProducts(Client client, ICollection<int> productsIds)
        {
            if (ThereAreProductsSelected(productsIds))
                foreach (var productId in productsIds)
                    if (!client.Products.Any(p => p.Id == productId))
                    {
                        var product = Context.Products.Single(p => p.Id == productId);
                        client.Products.Add(product);
                    }
        }

        private void UpdateClientProducts(int clientId, ICollection<int> productsIds)
        {
            var client = Context.Clients.Include("Products").Single(c => c.Id == clientId);
            RemoveSurplussingProducts(client, productsIds);
            AddMissingProducts(client, productsIds);
            Context.SaveChanges();
            Context.Entry(client).State = EntityState.Detached;
        }

        private void RemoveSurplussingProducts(Client client, ICollection<int> productsIds)
        {
            if (ThereAreProductsSelected(productsIds))
                RemoveUnselectedProducts(client, productsIds);
            else
                EmptyClientProducts(client);
        }

        private bool ThereAreProductsSelected(ICollection<int> productsIds)
        {
            return productsIds != null;
        }

        private void EmptyClientProducts(Client client)
        {
            foreach (var product in client.Products.ToList())
                client.Products.Remove(product);
        }

        private void RemoveUnselectedProducts(Client client, ICollection<int> productsIds)
        {
            foreach (var product in client.Products.ToList())
            {
                if (!productsIds.Contains(product.Id))
                    client.Products.Remove(product);
            }
        }

        protected override void CheckValidDeletion(Client client) { }
    }
}
