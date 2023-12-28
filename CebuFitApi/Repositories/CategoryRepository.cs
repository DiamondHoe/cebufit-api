using CebuFitApi.Interfaces;
using CebuFitApi.Models;

namespace CebuFitApi.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public Task<Category> AddAsync(Category category)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Category>> GetAllAsync()
        {
            var categories =  new List<Category> 
            {
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Pieczywo"
                }
            };
            return Task.FromResult(categories);
        }

        public Task<Category> GetByIdAsync(Guid categoryId)
        {
            var category = new Category { Id = categoryId, Name = "Pieczywko" };
            return Task.FromResult(category);
        }

        public Task UpdateAsync(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
