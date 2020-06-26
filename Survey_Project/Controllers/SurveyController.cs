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

            var survey = _context.Surveys.Where(s => s.SurveyId == id).FirstOrDefault();
            var questions = _context.Questions.Where(q => q.QuestionId == survey.SurveyId).ToList();
            //var options = _context.Options.Where(o => o.QuestionId == questions

            return View(questions);
        }

        // GET: Survey/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Survey/Create
        [HttpPost]
        public ActionResult Create(Survey survey, string quarter)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var admin = _context.Admins.Where(c => c.IdentityUserId == userId).FirstOrDefault();
            survey.AdminId = admin.AdminId;
            survey.Quarter = quarter;

            if (ModelState.IsValid)
            {
                _context.Surveys.Add(survey);
                _context.SaveChanges();
           
            }
   
            return RedirectToAction("SurveyQuestions", new { id = survey.SurveyId });
        }


        // GET: SurveyQuestions/Create
        public ActionResult SurveyQuestions(int id, Question question)
        {
            
            QuestionViewModel qvm = new QuestionViewModel();

            qvm.Question = new Question();
            qvm.Question.SurveyId = id;

            return View(qvm);
        }

        // POST: SurveyQuestions/Create
        [HttpPost , ActionName ("SurveyQuestions")]
        public ActionResult SurveyQuestions(QuestionViewModel qvm, bool addAnotherQuestion)
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

                if (addAnotherQuestion == true)
                {
                    return View(qvm);
                }
                else
                {
                    return RedirectToAction("Index", "Admin");
                }

            }
            return RedirectToAction("SurveyQuestions", new { id = qvm.Question.SurveyId });

        }


        // GET: Survey/Edit/5
        public ActionResult Edit(int id)
        {

            return View();

        }

        // POST: Survey/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Question question)
        {

            if (ModelState.IsValid)
            {
                _context.Update(question);
                _context.SaveChangesAsync();

            }
            return RedirectToAction("Index", "Admin");
        }
       

        // GET: Survey/Delete/5
        public ActionResult Delete(int? id)
        {
            if( id == null)
            {
                return NotFound();
            }

            var survey = _context.Surveys.Where(s => s.SurveyId == id).FirstOrDefault();

            return View(survey);
        }

        // POST: Survey/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {

            var survey = _context.Surveys.Where(s => s.SurveyId == id).FirstOrDefault();
            _context.Surveys.Remove(survey);
            _context.SaveChanges();
            return RedirectToAction("Index", "Admin");


        }
    }
}