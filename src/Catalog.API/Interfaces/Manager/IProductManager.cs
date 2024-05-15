using Catalog.API.Models;
using MongoRepo.Interfaces.Manager;

namespace Catalog.API.Interfaces.Manager
{
    public interface IProductManager : ICommonManager<Product>
    {
        //Custom method
        public List<Product> GetByCategory(string category); // public is not mandetory
    }
}
