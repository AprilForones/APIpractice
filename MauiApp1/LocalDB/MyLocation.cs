﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.LocalDB
{
    public class MyLocation
    {
        [Key]

        public int Id { get; set; }
        public double Latitude { get; set; }

        public double Longitude { get; set; }
       
       
    }
}
