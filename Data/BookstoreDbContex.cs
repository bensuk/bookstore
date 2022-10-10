﻿using bookstore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace bookstore.Data
{
    public class BookstoreDbContex : DbContext
    {
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookstoreDB");
        }
    }
}
