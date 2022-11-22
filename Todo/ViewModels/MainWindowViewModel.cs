

using ReactiveUI.Fody.Helpers;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Todo.Models;
using Todo.Services;

namespace Todo.ViewModels
{
    /// <summary>
    /// MainWindowVM используется, как агрегатор событий всех форм 
    /// </summary>
    public partial class MainWindowViewModel : ViewModelBase
    {
        private Database _db;

        // реактивное ( автообновляемое ) свойство , привязано как контекст 
        [Reactive] ViewModelBase Content { get; set; }

        // использует подключение аргумента из App.cs, можно заменить на DI 
        public MainWindowViewModel(Database db)
        {
            _db = db;
            // присвоение одного свойства другому с получением списка данных из "базы"
            Content = List = new TodoListViewModel(db.GetItemsAsync().Result);
        }

        // свойство с получением данных из другой VM
        public TodoListViewModel List { get; }

        // призана к действию , кнопок...
        public void AddItem()
        {
            // создать экземпляр другой VM
            var vm = new AddItemViewModel();

            // Обьединяет прослушивание команд из внешней VM
            Observable.Merge(
                vm.Ok,
                // Cancel нечего не возвращает , преобразовать в пустой обьект
                vm.Cancel.Select(_ => (TodoItem)null))
                // взять один
                .Take(1)
                // подписывается на изменения TodoItem 
                .Subscribe(async model =>
                {
                    if (model != null)
                    {
                        // получить список из внешней модели и дополнить
                        List.Items.Add(model);
                        await Task.Run(() => _db.SaveAsync(model));
                    }
                    // перезаписать контент
                    Content = List;
                });
            // в любом случие вернуть AddItemViewModel?
            Content = vm;
        }
    } 
}
