namespace IDFit.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using IDFit.Data.Models;

    public class ToolsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Tools.Any())
            {
                return;
            }

            var tools = new List<string> { "dumbbell10", "dumbbell15", "dumbbell20", "dumbbell25", "dumbbell30" };

            int kg = 5;

            foreach (var tool in tools)
            {
                await dbContext.Tools.AddAsync(new Tool
                {
                    Name = $"Dumbbell {kg += 5}",
                    Details = $"Dumbbell with weight {kg}kg",
                    ImageUrl = "https://cdn2.coachmag.co.uk/sites/coachmag/files/styles/16x9_480/public/shutterstock_151938761.jpg?itok=qXH_U7PM&timestamp=1417110661",
                });
            }
        }
    }
}
