using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChoixResto.Models;

namespace ChoixResto.Controllers
{
    public class RestaurantController : Controller
    {
        // GET: Restaurant
        public ActionResult Index()
        {
            using (IDal dal = new Dal())
            {
                List<Resto> listeDesRestaurants = dal.ObtientTousLesRestaurants();
                return View(listeDesRestaurants);
            }
        }
    }
}