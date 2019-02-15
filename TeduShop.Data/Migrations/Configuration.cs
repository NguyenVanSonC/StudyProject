using System.Collections.Generic;

namespace TeduShop.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TeduShop.Model.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TeduShop.Data.TeduShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TeduShop.Data.TeduShopDbContext context)
        {
            /*var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new TeduShopDbContext()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new TeduShopDbContext()));

            var user = new ApplicationUser()
            {
                UserName = "tedu",
                Email = "tedu.international@gmail.com",
                EmailConfirmed = true,
                BirthDay = DateTime.Now,
                FullName = "Technology Education"

            };

            manager.Create(user, "123654$");

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "User" });
            }

            var adminUser = manager.FindByEmail("tedu.international@gmail.com");

            manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
            */
            CreateProductCategorySample(context);
        }

        private void CreateProductCategorySample(TeduShop.Data.TeduShopDbContext context)
        {
            if (context.ProductCategories.Count()==0)
            {
                List<ProductCategory> listProductCategories = new List<ProductCategory>()
                {
                    new ProductCategory(){Name = "Điện Lạnh", Alias = "dien-lanh", Status = true},
                    new ProductCategory(){Name = "Viễn Thông", Alias = "vien-thong", Status = true},
                    new ProductCategory(){Name = "Đồ gia dụng", Alias = "do-gia-dung", Status = true},
                    new ProductCategory(){Name = "Mỹ Phẩm", Alias = "my-pham", Status = true}
                };

                context.ProductCategories.AddRange(listProductCategories);
                context.SaveChanges();
            }
        }
    }
}
