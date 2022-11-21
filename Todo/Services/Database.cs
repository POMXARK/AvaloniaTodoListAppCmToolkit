using System.Collections.Generic;
using Todo.Models;

namespace Todo.Services
{
    public class Database
    {
        //TODO: EF
        public IEnumerable<TodoItem> GetItems() => new[]
        {
            new TodoItem { Description = "Walk the dog" },
            new TodoItem { Description = "Buy some milk" },
            new TodoItem { Description = "Learn Avalonia", IsChecked = true },
        };
    }
}
