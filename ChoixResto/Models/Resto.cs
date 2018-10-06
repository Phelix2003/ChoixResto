using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace ChoixResto.Models
{
    [Table("Restos")]
    public class Resto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = " Oulala... Le champ ne peut pas être vide")]
        public string Nom { get; set; }

        [Display(Name = "Téléphone")]
        [RegularExpression(@"^0[0-9]{9}$", ErrorMessage = "Please enter a valid phone number")] // Numéro de télphone commence par 0, suivi de 9 chiffres, pas d'espaces, pas de / ni tirets.
        public string Telephone { get; set; }
    }
}