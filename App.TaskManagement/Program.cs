
using App.TaskManagement.Helpers;
using Library.TaskManagement.Models;
using Library.TaskManagement.Services;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var todoHelper = new ItemHelper();
            var todoHelper2 = new ItemHelper();
            
            bool cont = true;
            while (cont)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Add a task");
                Console.WriteLine("2. Delete a task");
                Console.WriteLine("3. Update a task");
                Console.WriteLine("4. List all the tasks");
                Console.WriteLine("5. Search for tasks");
                Console.WriteLine("6. Exit");

                string choice = Console.ReadLine() ?? string.Empty;

                if (int.TryParse(choice, out int choiceInt))
                {
                    if (choiceInt == 1)
                    {
                        todoHelper.Add();

                        
                    } else if(choiceInt == 2)
                    {
                        Console.WriteLine("Which of the tasks would you like to remove?");
                        todoHelper.Items.ForEach(number => Console.WriteLine(number));
                        int toDelete;
                        while (!int.TryParse(Console.ReadLine(), out toDelete))
                        {
                            Console.WriteLine("Invalid Selection. Please try again.");
                            todoHelper.Items.ForEach(number => Console.WriteLine(number));

                            int.TryParse(Console.ReadLine(), out toDelete);
                        }

                        todoHelper.Items.RemoveAt(toDelete);
                    } else if (choiceInt == 3)
                    {
                        Console.WriteLine("Which of the tasks would you like to edit?");

                        todoHelper.Items.ForEach(number => Console.WriteLine(number));
                        int toDelete;
                        while (!int.TryParse(Console.ReadLine(), out toDelete))
                        {
                            Console.WriteLine("Invalid Selection. Please try again.");
                            todoHelper.Items.ForEach(number => Console.WriteLine(number));

                            int.TryParse(Console.ReadLine(), out toDelete);
                        }

                        var taskToEdit = todoHelper.Items.ElementAt(toDelete);

                        Console.WriteLine("What property do you want to edit?");
                        Console.WriteLine("1. Name");
                        Console.WriteLine("2. Description");

                        if (taskToEdit is Appointment)
                        {
                            Console.WriteLine("3. Start");
                            Console.WriteLine("4. End");
                        } else
                        {

                        }

                        var propertyChoice = int.Parse(Console.ReadLine() ?? "0");
                        switch(propertyChoice)
                        {
                            case 1:
                                Console.WriteLine("What is the new Name?");
                                taskToEdit.Name = Console.ReadLine() ?? string.Empty;
                                break;
                            case 2:
                                Console.WriteLine("What is the new description?");
                                taskToEdit.Description = Console.ReadLine() ?? string.Empty;
                                break;
                            default:
                                Console.WriteLine("Sorry, that functionality hasn't been implemented yet!");
                                break;
                        }
                    }
                    else if (choiceInt == 4)
                    {
                        todoHelper.Items.ForEach(number => Console.WriteLine(number));
                    }
                    else if (choiceInt == 5)
                    {
                        Console.WriteLine("Enter a search term:");
                        var query = Console.ReadLine();

                        var filteredTasks = todoHelper.Items
                            .Where(t => 
                            ((t is Appointment) || ((t is ToDo) && (t as ToDo).IsComplete)) &&
                            (t.Name.Contains(query, StringComparison.InvariantCultureIgnoreCase)
                            || t.Description.Contains(query, StringComparison.InvariantCultureIgnoreCase))
                            );

                        //note that ToList() here is OK because we are just temporarily using the deep copy!
                        filteredTasks.ToList().ForEach(task => Console.WriteLine(task));
                    }
                    else if (choiceInt == 6)
                    {
                        cont = false;
                    }
                }
            }
        }
    }
}