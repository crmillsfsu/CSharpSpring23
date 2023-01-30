
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
                Console.WriteLine("2. Delete a task");
                Console.WriteLine("3. List all the tasks");
                Console.WriteLine("4. Search for tasks");
                Console.WriteLine("5. Exit");

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
                            var newTodo = new ToDo(newTask);

                            newTodo.IsComplete = false;
                            taskList.Add(newTodo);
                        } else
                        {
                            var newAppointment = new Appointment(newTask);

                            newAppointment.Start = DateTime.Now;
                            newAppointment.End = DateTime.Now.AddHours(1);

                            taskList.Add(newAppointment);
                        }

                        
                    } else if(choiceInt == 2)
                    {
                        Console.WriteLine("Which of the tasks would you like to remove?");
                        taskList.ForEach(number => Console.WriteLine(number));
                        int toDelete;
                        while (!int.TryParse(Console.ReadLine(), out toDelete))
                        {
                            Console.WriteLine("Invalid Selection. Please try again.");
                            taskList.ForEach(number => Console.WriteLine(number));

                            int.TryParse(Console.ReadLine(), out toDelete);
                        }

                        taskList.RemoveAt(toDelete);
                    }
                    else if (choiceInt == 3)
                    {
                        taskList.ForEach(number => Console.WriteLine(number));
                    }
                    else if (choiceInt == 4)
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
                    else if (choiceInt == 5)
                    {
                        cont = false;
                    }
                }
            }
        }
    }
}