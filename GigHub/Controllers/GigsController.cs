﻿using GigHub.Models;
using GigHub.ViewModel;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }
        
        public ActionResult Create()

        {
            var viewmodel = new GigFormViewModel
            {
                 Genres = _context.Genres.ToList()
            };
            return View(viewmodel);
        }
    }
}