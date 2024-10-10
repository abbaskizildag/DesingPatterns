using BaseProject.Models;
using Microsoft.EntityFrameworkCore;
using WebApp.Strategy.Models;

namespace WebApp.Strategy.Repositories
{
    public class ProductRepositoryFromSqlServer(AppIdentityDbContext context) : IProductRepository
    {
        public async Task Delete(Product product)
        {
            context.Products.Remove(product);

            await context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllByUserId(string userId)
        {
            return await context.Products.Where(x=>x.UserId == userId).ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await context.Products.FindAsync(id);
        }

        public async Task<Product> Save(Product product)
        {
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

            return product;
        }

        public async Task Update(Product product)
        {
            context.Products.Update(product);
            await context.SaveChangesAsync();
        }
    }
}
