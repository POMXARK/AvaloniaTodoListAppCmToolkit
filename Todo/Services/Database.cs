using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using Todo.Models;

namespace Todo.Services
{
    public class Database : DbContext
    {
        //TODO: EF
        public IEnumerable<TodoItem> GetItems() => TodoItems;

        public DbSet<TodoItem> TodoItems { get; set; }

        public string DbPath { get; }

        public Database()
        {
            DbPath = Path.Join(AppDomain.CurrentDomain.BaseDirectory, "Todo.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>(entity => { entity.Property(x => x.IsChecked).IsRequired(); });

            #region TodoItemSeed
            modelBuilder.Entity<TodoItem>()
                .HasData(
                    new TodoItem { Id = 1, Description = "Walk the dog", IsChecked = false },
                    new TodoItem { Id = 2, Description = "Buy some milk", IsChecked = false },
                    new TodoItem { Id = 3, Description = "Learn Avalonia", IsChecked = true });
            #endregion

        }

    }


}
