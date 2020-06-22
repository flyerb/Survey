using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Survey_Project.Data;
using Survey_Project.Models;

namespace Survey_Project.Controllers
{
    public class SurveyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SurveyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Survey
        public ActionResult Index()
        {
            
            return View();
        
        }

        // GET: Survey/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var options = _context.Options.Find(i =>i.OptionId).
            //var survey = _context.Surveys.Where(s => s.SurveyId ==

            return View();
        }

        // GET: Survey/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Survey/Create
        [HttpPost]
        public ActionResult Create(Survey survey)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var admin = _context.Admins.Where(c => c.IdentityUserId == userId).FirstOrDefault();
            survey.AdminId = admin.AdminId;

            if (ModelState.IsValid)
            {
                _context.Surveys.Add(survey);
                _context.SaveChanges();
           
            }
   
            return RedirectToAction("SurveyQuestions", new { id = survey.SurveyId });
        }

        public ActionResult SurveyQuestions(int id, Question question)
        {
            
            QuestionViewModel qvm = new QuestionViewModel();

            qvm.Question = new Question();
           
            qvm.Question.SurveyId = id;

      

            return View(qvm);
        }


        [HttpPost , ActionName ("SurveyQuestions")]
        public ActionResult SurveyQuestions(QuestionViewModel qvm)
        {
            

            if (ModelState.IsValid)
            {

                _context.Add(qvm.Question);
                _context.SaveChanges();

                Option optionOne = new Option();
                Option optionTwo = new Option();
                Option optionThree = new Option();
                Option optionFour = new Option();

                optionOne.Choice = qvm.OptionOne;
                optionTwo.Choice = qvm.OptionTwo;
                optionThree.Choice = qvm.OptionThree;
                optionFour.Choice = qvm.OptionFour;

                optionOne.QuestionId = qvm.Question.QuestionId;
                optionTwo.QuestionId = qvm.Question.QuestionId;
                optionThree.QuestionId = qvm.Question.QuestionId;
                optionFour.QuestionId = qvm.Question.QuestionId;


                _context.Add(optionOne);
                _context.Add(optionTwo);
                _context.Add(optionThree);
                _context.Add(optionFour);
                _context.SaveChanges();
                // instantiate three Option objects
                // assign the new Option objects their values
                // add options to DB and save changes again

            }
            return View();


            //var questions = _context.Questions.Where(q => q.QuestionId == survey.SurveyId).FirstOrDefault();
            //var options = _context.Options.Where(o => o.QuestionId == question.QuestionId).ToList();

            //var set = _context.Options.Include(s => s.QuestionId == question.QuestionId).ToList();

            //QuestionViewModel qvm = new QuestionViewModel();
            //qvm.Questions = questions;
            //qvm.Options = options;

            //return View(qvm);
        }

        public ActionResult AddOption()
        {
           
            return View();
        }

        [HttpPost, ActionName("AddOption")]  

        public ActionResult AddOption(Question question)
        {
            var option = _context.Options.Where(s => s.OptionId == 0).FirstOrDefault();
            option.OptionId = question.QuestionId;
            _context.Add(option);
            _context.SaveChanges();

            return RedirectToAction("SurveyQuestions"); 

        }
         

        // GET: Survey/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Survey/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Survey/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Survey/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}