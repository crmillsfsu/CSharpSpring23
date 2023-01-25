﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.TaskManagement.Models
{
    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual string Display => $"{Name} - {Description}";


    }
}
