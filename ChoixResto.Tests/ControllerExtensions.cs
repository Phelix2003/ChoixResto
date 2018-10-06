﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChoixResto.Controllers;
using ChoixResto.Models;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;


namespace ChoixResto.Tests
{
    public static class ControllerExtensions
    {
        public static void ValideLeModele<T>(this Controller controller, T modele)
        {
            controller.ModelState.Clear();

            ValidationContext context = new ValidationContext(modele, null, null);
            List<ValidationResult> validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(modele, context, validationResults, true);

            foreach (ValidationResult result in validationResults)
            {
                foreach (string memberName in result.MemberNames)
                {
                    controller.ModelState.AddModelError(memberName, result.ErrorMessage);
                }
            }
        }
    }
}
