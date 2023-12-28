using CebuFitApi.Interfaces;
using CebuFitApi.Models;
using System.Xml.Linq;

namespace CebuFitApi.Repositories
{
    public class MealRepository : IMealRepository
    {
        public Task CreateAsync(Meal blogPost)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Meal>> GetAllAsync()
        {
            var meals = new List<Meal>
            {
                new Meal { Id = Guid.NewGuid(), Name = "Spaghetti Carbonara", Eaten = false, Doable = true,
                    Ingredients = new List<Ingredient>{
                        new Ingredient {
                            Id = Guid.NewGuid(),
                            Quantity = 1,
                            AssociatedProduct = new Product {
                                Id = Guid.NewGuid(),
                                Name ="SMTH",
                                Calories=200,
                                Importance = Helpers.Enums.ImportanceEnum.Medium,
                                UnitWieght = 20,
                                Category = new Category{ 
                                    Id = Guid.NewGuid(),
                                    Name = "Chlep"}
                            } 
                        } 
                    } 
                },
            };

            return Task.FromResult(meals);
        }


        public Task<Meal> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Meal blogPost)
        {
            throw new NotImplementedException();
        }
    }
}
