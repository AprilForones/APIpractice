﻿using Microsoft.EntityFrameworkCore;

namespace APIpractice
{
    public class BooksDbContext:DbContext
    {
        public BooksDbContext(DbContextOptions<BooksDbContext> options):base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("Server=localhost,3306;Database=booksdb;Uid=root;Pwd=root;");
        }
    }
    
}