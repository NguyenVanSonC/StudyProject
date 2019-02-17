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
            //CreateProductCategorySample(context);
            CreateProductSample(context);
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

        private void CreateProductSample(TeduShop.Data.TeduShopDbContext context)
        {
            if (context.Products.Count() == 0)
            {
                List<Product> listProducts = new List<Product>()
                {
                    new Product(){Name = "Tủ Lạnh", CategoryID = 4, Alias = "do-gia-dung",Price = 100, Status = true},
                    new Product(){Name = "Ti Vi", CategoryID = 4, Alias = "do-gia-dung",Price = 500, Status = true},
                    new Product(){Name = "Bàn ghế", CategoryID = 4, Alias = "do-gia-dung",Price = 200, Status = true},
                    new Product(){Name = "Xe máy", CategoryID = 4, Alias = "do-gia-dung",Price = 400, Status = true},
                    new Product(){Name = "ô tô", CategoryID = 4, Alias = "do-gia-dung",Price = 200, Status = true},
                    new Product(){Name = "Loa", CategoryID = 4, Alias = "do-gia-dung",Price = 700, Status = true}
                };

                context.Products.AddRange(listProducts);
                context.SaveChanges();
            }
        }
    }
}
