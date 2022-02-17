using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using FitnessCenter.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FitnessCenter.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string UserType { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        //public string ItemName { get; set; }
        public virtual ICollection<ItemViewModel> Items { get; set; }
        public virtual ICollection<OrderModel> Orders { get; set; }
        public virtual ICollection<OrderDetailsModel> OrderDetails { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

      
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<FitnessCenter.Models.Course> Courses { get; set; }

        //public System.Data.Entity.DbSet<FitnessCenter.Models.RoleViewModel> RoleViewModels { get; set; }

        public System.Data.Entity.DbSet<FitnessCenter.Models.ApplyForCourse> ApplyForCourses { get; set; }
        public System.Data.Entity.DbSet<FitnessCenter.ViewModel.CategoryViewModel>Categories { get; set; }

       
        public System.Data.Entity.DbSet<FitnessCenter.ViewModel.ItemViewModel> Items { get; set; }
        public System.Data.Entity.DbSet<FitnessCenter.ViewModel.OrderModel> Orders { get; set; }
        public System.Data.Entity.DbSet<FitnessCenter.ViewModel.OrderDetailsModel> OrderDetails { get; set; }
    }
}