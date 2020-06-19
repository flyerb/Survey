﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [ValidateAntiForgeryToken]
        public ActionResult Create(Survey survey, int id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var admin = _context.Admins.Where(c => c.IdentityUserId == userId).FirstOrDefault();
            survey.AdminId = admin.AdminId;

            if (ModelState.IsValid)
            {
                _context.Add(survey);
                _context.SaveChanges();
                return RedirectToAction();
            }
   
            return View(survey);
        }

        public ActionResult SurveyQuestions(Survey survey, Question question)
        {
            var questions = _context.Questions.Where(q => q.QuestionId == survey.SurveyId).ToList();
            var options = _context.Options.Where(o => o.QuestionId == question.QuestionId).ToList();
            QuestionViewModel qvm = new QuestionViewModel();
            qvm.Question = questions;
            qvm.Option = options;
            return View(qvm);
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