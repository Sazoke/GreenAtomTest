using System;
using System.Collections.Generic;
using System.Linq;
using GreenAtom.Data;
using GreenAtom.Models;

namespace GreenAtom.Repositories
{
    public class LinksRepository : ILinksRepository, IDisposable
    {
        private OrdersContext context;

        public LinksRepository(OrdersContext context)
        {
            this.context = context;
        }

        public List<int> GetProductIds(int id)
        {
            return context.Links.Where(l => l.OrderId.Equals(id))
                .Select(l => l.ProductId)
                .ToList();
        }

        public OrderAndProductLink GetLinkByIds(int orderId, int productId)
        {
            return context.Links
                .FirstOrDefault(l => l.OrderId.Equals(orderId) && l.ProductId.Equals(productId));
        }

        public void CreateLink(int orderId, int productId, int count)
        {
            context.Links.Add(new OrderAndProductLink() {Count = count, OrderId = orderId, ProductId = productId});
            context.SaveChanges();
        }

        public void DeleteLink(int orderId, int productId)
        {
            context.Links.Remove(GetLinkByIds(orderId, productId));
            context.SaveChanges();
        }

        public void UpdateLink(int orderId, int productId, int count)
        {
            DeleteLink(orderId,productId);
            CreateLink(orderId, productId, count);
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