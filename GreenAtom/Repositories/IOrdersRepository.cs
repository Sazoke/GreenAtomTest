using GreenAtom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenAtom.Repositories
{
    public interface IOrdersRepository
    {
        public List<Order> GetOrders();
        public Order GetOrderById(int id);
        public void InsertOrder(Order order);
        public void DeleteOrder(int id);
        public void UpdateOrder(int id, Order newOrder);
    }
}
