namespace Library.TaskManagement.Models
{
    public class ToDo : Item
    {
        public bool IsComplete { get; set; }

        public ToDo()
        {

        }

        public override string ToString()
        {
            return Display;
        }

        public ToDo(Item i)
        {
            Name = i.Name;
            Description = i.Description;
        }
    }
}