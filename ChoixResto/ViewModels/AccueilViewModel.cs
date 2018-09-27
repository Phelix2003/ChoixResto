using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChoixResto.Models;

namespace ChoixResto.ViewModels
{
    public class AccueilViewModel
    {
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public Resto Restos { get; set; }
    }
}