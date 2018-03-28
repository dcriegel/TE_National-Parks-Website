using Capstone.Web.DAL;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Controllers
{
    public class SurveyController : Controller
    {

        private ISurveyDAL _dal;
        public SurveyController(ISurveyDAL dal)
        {
            _dal = dal;
        }

        public ActionResult SurveyResult()
        {
            return View(_dal.GetAllSurveys());
        }

        [HttpPost]
        public ActionResult Survey(Survey newSurvey)
        {
            _dal.SaveNewSurvey(newSurvey);
            return RedirectToAction("SurveyResult");
        }

        public ActionResult Survey()
        {
            return View();
        }


        

        


    }
}