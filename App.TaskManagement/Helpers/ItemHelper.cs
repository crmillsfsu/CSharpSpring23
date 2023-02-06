using Library.TaskManagement.Models;
using Library.TaskManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.TaskManagement.Helpers
{
    public class ItemHelper
    {
        private ToDoService toDoService;

        public ItemHelper()
        {
            this.toDoService = ToDoService.Current;
        }

        public List<Item> Items
        {
            get
            {
                return toDoService.Tasks.ToList();
            }
        }

        public void Add()
        {
            var isToDo = true;
            Console.WriteLine("Is this task a Calendar Appointment?");
            var response = Console.ReadLine() ?? string.Empty;
            if (response.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                isToDo = false;
            }

            var newTask = new Item();

            Console.WriteLine("Enter a name:");
            newTask.Name = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Enter a description:");
            newTask.Description = Console.ReadLine() ?? string.Empty;

            if (isToDo)
            {
                var newTodo = new ToDo(newTask);

                newTodo.IsComplete = false;
                toDoService.AddToDo(newTodo);
            }
            else
            {
                var newAppointment = new Appointment(newTask);

                newAppointment.Start = DateTime.Now;
                newAppointment.End = DateTime.Now.AddHours(1);

                Console.WriteLine("Add any attendees by email address (Q to quit):");
                var input = Console.ReadLine() ?? string.Empty;
                while (!input.Equals("Q", StringComparison.InvariantCultureIgnoreCase))
                {
                    newAppointment.Attendees.Add(input);
                    input = Console.ReadLine() ?? string.Empty;
                }

                toDoService.Tasks.Add(newAppointment);
            }
        }
    }
}
