using System;
using System.Collections.Generic;
using System.Linq;
using GreenAtom.Data;
using GreenAtom.Models;

namespace GreenAtom.Repositories
{
    public class ProductsRepository:IProductsRepository, IDisposable
    {
        private OrdersContext context;

        public ProductsRepository(OrdersContext context)
        {
            this.context = context;
        }

        public Product GetProductById(int id)
        {
            return context.Products.FirstOrDefault(p => p.Id.Equals(id));
        }

        public void InsertProduct(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            context.Products.Remove(GetProductById(id));
        }

        public void UpgradeProduct(int id, Product product)
        {
            var oldProduct = context.Products.FirstOrDefault(p => p.Id.Equals(id));
            if(oldProduct is not null)
                context.Products.Remove(product);
            if(product is null)
                return;
            context.Products.Add(product);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
                if (disposing)
                    context.Dispose();
            disposed = true;
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}