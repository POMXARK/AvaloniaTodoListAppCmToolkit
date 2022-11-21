
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Reactive;
using Todo.Models;

namespace Todo.ViewModels
{
    public partial class AddItemViewModel : ViewModelBase
    {
        // реактивное обновляемое свойство описание ( привязано к чекбоксу)
        [Reactive] string Description { get; set; }

        public AddItemViewModel()
        {
            // слушать изменения в Description, вернуть если существует или не пустой, возвращает bool
            var okEnabled = this.WhenAnyValue(
                x => x.Description,
                x => !string.IsNullOrWhiteSpace(x));

            // возвращает, если доступно, новый экземпляр модели TodoItem
            Ok = ReactiveCommand.Create(
                () => new TodoItem { Description = Description },
                okEnabled);
            // возврощает нечего null?
            Cancel = ReactiveCommand.Create(() => { });
        }
        // Обьявление команд, эта принимает любое (Unit) и возвращает TodoItem
        public ReactiveCommand<Unit, TodoItem> Ok { get; }
        public ReactiveCommand<Unit, Unit> Cancel { get; }

    }
}
