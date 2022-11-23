using ReactiveUI.Fody.Helpers;

namespace Todo.Models
{
    public class TodoItem
    {
        public uint Id { get; set; }
        public string? Description { get; set; }
        public bool IsChecked { get; set; }

    }
}
