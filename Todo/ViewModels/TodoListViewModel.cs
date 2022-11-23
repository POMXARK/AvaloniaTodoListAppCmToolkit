
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Todo.Models;
using Todo.Services;

namespace Todo.ViewModels
{
    public class TodoListViewModel : ViewModelBase
    {
        public TodoListViewModel(IEnumerable<TodoItem> items, Database db = null)
        {
            TodoItems = new ObservableCollection<TodoItem>(items);
        }

        public ObservableCollection<TodoItem> TodoItems { get; }

        public async void UpdateItemCommand(TodoItem todoItem)
        {
            await Task.Run(() =>
            {
                using (var context = new Database())
                {
                    Thread.Sleep(1500); // test async
                    context.TodoItems.Attach(todoItem);
                    context.Entry(todoItem).State = EntityState.Modified;
                    context.SaveChangesAsync();
                }

            });
        }
    }
}