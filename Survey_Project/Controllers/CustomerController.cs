using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1;
using Survey_Project.Data;
using Survey_Project.Models;

namespace Survey_Project.Controllers
{
   // [Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = _context.Customers.Where(c => c.IdentityUserId == userId).FirstOrDefault();


            if (customer ==  null)
            {
                return RedirectToAction("Create");
            }

            return View(customer) ;
        }

        public ActionResult ViewSurvey()
        {
            var allSurveys = _context.Surveys.ToList();
            return View(allSurveys);
        }



        // GET: Customers/Details/5 
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.Include(c => c.IdentityUser).FirstOrDefaultAsync(m =>m.CustomerId == id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer) //maybe bind?
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                customer.IdentityUserId = userId;
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", customer.IdentityUserId);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", customer.IdentityUserId);
            return View(customer);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Customer customer)
        {
           if( id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = _context.Customers
                .Include(c => c.IdentityUser)
                .FirstOrDefault(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }


        // POST: Customers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var customer = _context.Customers.Find(id);
            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }

        public ActionResult TakeSurvey(int id) ///pass in survey id
        {
            QuestionViewModel qvm = new QuestionViewModel();
            qvm.Question = new Question();
            qvm.Question.SurveyId = id;

            var foundQuestions = _context.Questions.Where(f => f.SurveyId == id).FirstOrDefault();
            qvm.Question.QuestionString = foundQuestions.QuestionString;


            var foundChoices = _context.Options.Where(f => f.QuestionId == foundQuestions.QuestionId).ToList();

            qvm.OptionOne = foundChoices[0].Choice;
            qvm.OptionTwo = foundChoices[1].Choice;
            qvm.OptionThree = foundChoices[2].Choice;
            qvm.OptionFour = foundChoices[3].Choice;
            
            return View(qvm);
        }
        
        [HttpPost]
        public ActionResult TakeSurvey(string choice)
        {
            Response response = new Response();
            
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = _context.Customers.Where(c => c.IdentityUserId == userId).FirstOrDefault();
            response.option = _context.Options.Where(o => o.Choice == choice).FirstOrDefault();
           // var optionId = response.option.OptionId;

            response.CustomerId = customer.CustomerId;
     
            response.option.Choice = choice;


            _context.Responses.Add(response);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}