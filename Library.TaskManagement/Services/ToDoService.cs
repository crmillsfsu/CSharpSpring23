using Library.TaskManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.TaskManagement.Services
{
    public class ToDoService
    {
        private static object _lock = new object();
        private static ToDoService _instance;
        public static ToDoService Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ToDoService();
                }
                return _instance;
            }
        }

        public List<Item> Tasks { get; set; }
        private ToDoService()
        {
            Tasks = new List<Item>();
        }

        public void AddToDo(ToDo t)
        {
            Tasks.Add(t);
        }
    }
}
