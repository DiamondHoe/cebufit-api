using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CebuFitApi.Models
{
    public class User : BaseModel
    {
        public User()
        {
               
        }
        public string Role { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public bool Gender { get; set; } //False = Man, True = Woman
        public DateTime BirthDate { get; set; }
        public int KcalDemand { get; set; }

        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public List<StorageItem> StorageItems { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<Recipe> Recipes { get; set; }
        public List<Meal> Meals { get; set; }
        public List<Day> Days { get; set; }
        public List<Request> CreatedRequests { get; set; }
        public List<Request> ApprovedRequests { get; set; }

    }
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(user => user.Products)
                    .WithOne(product => product.User);

            builder.HasMany(user => user.Categories)
                    .WithOne(category => category.User);

            builder.HasMany(user => user.StorageItems)
                    .WithOne(storageItem => storageItem.User);

            builder.HasMany(user => user.Ingredients)
                    .WithOne(ingredient => ingredient.User);

            builder.HasMany(user => user.Recipes)
                    .WithOne(recipe => recipe.User);

            builder.HasMany(user => user.Meals)
                    .WithOne(meal => meal.User);

            builder.HasMany(user => user.Days)
                    .WithOne(day => day.User);
            
            builder.HasMany(user => user.CreatedRequests)
                    .WithOne(request => request.Requester);
            
            builder.HasMany(user => user.ApprovedRequests)
                    .WithOne(request => request.Approver);
        }
    }
}