using System.Collections.Generic;
using GreenAtom.Models;

namespace GreenAtom.Repositories
{
    public interface IProductsRepository
    {
        public Product GetProductById(int id);
        public void InsertProduct(Product product);
        public void DeleteProduct(int id);
        public void UpgradeProduct(int id, Product product);
    }
}