using System.Collections.Generic;
using GreenAtom.Models;

namespace GreenAtom.Repositories
{
    public interface ILinksRepository
    {
        public List<int> GetProductIds(int id);
        public OrderAndProductLink GetLinkByIds(int orderId, int productId);
        public void CreateLink(int orderId, int productId, int count);
        public void DeleteLink(int orderId, int productId);
        public void UpdateLink(int orderId, int productId, int count);
    }
}