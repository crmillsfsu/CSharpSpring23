
using Library.TaskManagement.Models;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var taskList = new List<Item>();
            bool cont = true;
            while (cont)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Add a task");
                Console.WriteLine("2. List all the tasks");
                Console.WriteLine("3. Search for tasks");
                Console.WriteLine("4. Exit");

                string choice = Console.ReadLine() ?? string.Empty;

                if (int.TryParse(choice, out int choiceInt))
                {
                    if (choiceInt == 1)
                    {
                        var isToDo = true;
                        Console.WriteLine("Is this task a Calendar Appointment?");
                        var response = Console.ReadLine() ?? string.Empty;
                        if(response.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
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
                            var newTodo = newTask as ToDo;

                            newTodo.IsComplete = false;
                            taskList.Add(newTodo);
                        } else
                        {
                            var newAppointment = newTask as Appointment;

                            newAppointment.Start = DateTime.Now;
                            newAppointment.End = DateTime.Now.AddHours(1);

                            taskList.Add(newAppointment);
                        }

                        
                    }
                    else if (choiceInt == 2)
                    {
                        taskList.ForEach(number => Console.WriteLine(number));
                    }
                    else if (choiceInt == 3)
                    {
                        Console.WriteLine("Enter a search term:");
                        var query = Console.ReadLine();

                        var filteredTasks = taskList
                            .Where(t => 
                            ((t is Appointment) || ((t is ToDo) && (t as ToDo).IsComplete)) &&
                            (t.Name.Contains(query, StringComparison.InvariantCultureIgnoreCase)
                            || t.Description.Contains(query, StringComparison.InvariantCultureIgnoreCase))
                            );

                        //note that ToList() here is OK because we are just temporarily using the deep copy!
                        filteredTasks.ToList().ForEach(task => Console.WriteLine(task));
                    }
                    else if (choiceInt == 4)
                    {
                        cont = false;
                    }
                }
            }
        }
    }
}