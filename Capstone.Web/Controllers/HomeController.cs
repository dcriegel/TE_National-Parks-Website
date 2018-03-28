﻿using Capstone.Web.DAL;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        private string connectionString;

        public HomeController(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private IParkDAL _dal;

        public HomeController(IParkDAL dal)
        {
            _dal = dal;
        }


        public ActionResult AllParks()
        {
            IList<Park> parkList = _dal.GetAllParks();

            return View("AllParks", parkList);
        }

        // GET: Home
        public ActionResult Index()
        {
            return View("Index");
        }


    }
}