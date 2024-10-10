using WebApp.Strategy.Models;

namespace WebApp.Strategy.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetById(int id);
        Task<List<Product>> GetAllByUserId(string userId);

        Task<Product> Save(Product product);
        Task Update(Product product);
        Task Delete(Product product);
    }
}
