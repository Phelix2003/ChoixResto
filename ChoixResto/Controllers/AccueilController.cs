﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChoixResto.Models;
using ChoixResto.ViewModels;

namespace ChoixResto.Controllers
{
    public class AccueilController : Controller
    {
        // GET: Accueil
        public ActionResult Index()
        {
            return View();
        }
    }
}