using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ChoixResto.Models
{
    public class Utilisateur
    {
        public int Id { get; set; }
        [Required, MaxLength(80)]
        public string Prenom { get; set; }
        [Required, MaxLength(80)]
        public string MDP { get; set; }
    }
}