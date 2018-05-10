using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SeniorProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace SeniorProject.Models
{
    public class SiteContext : IdentityDbContext<Users>
    {
        public DbSet<Member> Members { get; set; }
        public DbSet<Amount> Amounts { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<MemberIngredient> MemberIngredients { get; set; }

        public DbSet<CollectionRecipe> CollectionRecipes { get; set; }

        public SiteContext (DbContextOptions<SiteContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        
            // Shorten key length for Identity 
            modelBuilder.Entity<Users>(entity => { 
            entity.Property(m => m.Email).HasMaxLength(127); 
            entity.Property(m => m.NormalizedEmail).HasMaxLength(127); 
            entity.Property(m => m.NormalizedUserName).HasMaxLength(127); 
            entity.Property(m => m.UserName).HasMaxLength(127); 
            }); 
            modelBuilder.Entity<IdentityRole>(entity => { 
                entity.Property(m => m.Name).HasMaxLength(127); 
                entity.Property(m => m.NormalizedName).HasMaxLength(127); 
            }); 
            modelBuilder.Entity<IdentityUserLogin<string>>(entity => 
            { 
                entity.Property(m => m.LoginProvider).HasMaxLength(127); 
                entity.Property(m => m.ProviderKey).HasMaxLength(127); 
            }); 
            modelBuilder.Entity<IdentityUserRole<string>>(entity => 
            { 
                entity.Property(m => m.UserId).HasMaxLength(127); 
                entity.Property(m => m.RoleId).HasMaxLength(127); 
            }); 
            modelBuilder.Entity<IdentityUserToken<string>>(entity => 
            { 
                entity.Property(m => m.UserId).HasMaxLength(127); 
                entity.Property(m => m.LoginProvider).HasMaxLength(127); 
                entity.Property(m => m.Name).HasMaxLength(127); 
            
            }); 
        }
    }
}

