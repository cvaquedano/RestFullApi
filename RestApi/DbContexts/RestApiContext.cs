using Microsoft.EntityFrameworkCore;
using RestApi.Entities;
using System;

namespace RestApi.DbContexts
{
    public class RestApiContext : DbContext
    {
        public RestApiContext(DbContextOptions<RestApiContext> options)
           : base(options)
        {

        }

        public RestApiContext()
        {

        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // seed the database with dummy data
            modelBuilder.Entity<Author>().HasData(
                new Author()
                {
                    Id = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                    FirstName = "Carlos",
                    LastName = "Vaquedano",

                },
                new Author()
                {
                    Id = Guid.Parse("da2fd609-d754-4feb-8acd-c4f9ff13ba96"),
                    FirstName = "Nancy",
                    LastName = "Mateo",

                },
                new Author()
                {
                    Id = Guid.Parse("2902b665-1190-4c70-9915-b9c2d7680450"),
                    FirstName = "Byron",
                    LastName = "Sommardahl",

                },
                new Author()
                {
                    Id = Guid.Parse("102b566b-ba1f-404c-b2df-e2cde39ade09"),
                    FirstName = "Jonathan",
                    LastName = "Salazar",

                }
              );

            modelBuilder.Entity<Book>().HasData(
               new Book
               {
                   Id = Guid.Parse("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b"),
                   AuthorId = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                   Title = "Carlos book 1",
                   Description = "Carlos book 1 description"
               },
               new Book
               {
                   Id = Guid.Parse("d8663e5e-7494-4f81-8739-6e0de1bea7ee"),
                   AuthorId = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                   Title = "Carlos book 2",
                   Description = "Carlos book 2 description"
               },
               new Book
               {
                   Id = Guid.Parse("d173e20d-159e-4127-9ce9-b0ac2564ad97"),
                   AuthorId = Guid.Parse("da2fd609-d754-4feb-8acd-c4f9ff13ba96"),
                   Title = "Nancy book 1",
                   Description = "Nancy book 1 description"
               },
               new Book
               {
                   Id = Guid.Parse("40ff5488-fdab-45b5-bc3a-14302d59869a"),
                   AuthorId = Guid.Parse("2902b665-1190-4c70-9915-b9c2d7680450"),
                   Title = "Byron book 1",
                   Description ="Byron book 1"
               }
               );

            base.OnModelCreating(modelBuilder);
        }

    }
}
