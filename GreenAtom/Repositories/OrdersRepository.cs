using GreenAtom.Data;
using GreenAtom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenAtom.Repositories
{
    public class OrdersRepository : IOrdersRepository, IDisposable
    {
        private OrdersContext context;

        public OrdersRepository(OrdersContext context)
        {
            this.context = context;
        }

        public Order GetOrderById(int id)
        {
            return context.Orders.FirstOrDefault(o => o.Id.Equals(id));
        }

        public List<Order> GetOrders()
        {
            return context.Orders.ToList();
        }

        public void InsertOrder(Order order)
        {
            context.Orders.Add(order);
            context.SaveChanges();
        }

        public void DeleteOrder(int id)
        {
            context.Orders.Remove(GetOrderById(id));
            context.SaveChanges();
        }

        public void UpdateOrder(int id, Order newOrder)
        {
            DeleteOrder(id);
            InsertOrder(newOrder);
            context.SaveChanges();
        }

        public void AddProductToOrder(int orderId, int productId, int count)
        {
            context.Links.Add(new OrderAndProductLink() {Count = count, OrderId = orderId, ProductId = productId});
            context.SaveChanges();
        }

        public void DeleteProductFromOrder(int orderId, int productId)
        {
            var link = context.Links.FirstOrDefault(l => l.OrderId.Equals(orderId) && l.ProductId.Equals(productId));
            if(link is null)
                return;
            context.Links.Remove(link);
        }

        public List<Product> GetProductsById(int id)
        {
            var productsIds = context.Links.Where(o => o.OrderId.Equals(id)).Select(o => o.ProductId).ToList();
            return context.Products.Where(p => productsIds.Contains(p.Id)).ToList();
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
