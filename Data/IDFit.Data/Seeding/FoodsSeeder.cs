namespace IDFit.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using IDFit.Data.Models;

    // my initial adding foods in db
    public class FoodsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Foods.Any())
            {
                return;
            }

            var chickenWings = new Food()
            {
                Name = "Chicken Wings",
                Quantity = 4,
                Weight = 200,
            };

            await dbContext.Foods.AddAsync(chickenWings);

            var glassOfWater = new Food()
            {
                Name = "Glass Of Water",
                Quantity = 1,
                Weight = 400,
            };

            await dbContext.Foods.AddAsync(glassOfWater);
        }
    }
}
