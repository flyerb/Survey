using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Security.Claims;
using System.Threading.Tasks;
using MailKit.Net.Pop3;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using MimeKit.Text;
using Survey_Project.Data;
using Survey_Project.Models;



namespace Survey_Project.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin
        public ActionResult Index()
        {

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var admin = _context.Admins.Where(c => c.IdentityUserId == userId).FirstOrDefault();

            if (admin == null)
            {
                return RedirectToAction("Create");
            }
             var allsurveys = _context.Surveys.Where(c => c.AdminId == admin.AdminId).ToList();
                return View(allsurveys);
        
        }

        // GET: Admin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = _context.Admins
              .Include(e => e.IdentityUser)
              .FirstOrDefault(m => m.AdminId == id);


            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }


        // GET: Admin/Create
        public ActionResult Create()
        {
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdminId,FirstName,LastName")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                admin.IdentityUserId = userId;
                _context.Add(admin);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", admin.IdentityUserId);
            return View(admin);
        }


        // GET: Client/Create
        public ActionResult AddClient()
        {
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Client/Create
        [HttpPost]
        public async Task<IActionResult> AddClient(Customer customer) 
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

        // GET: Admin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }

            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", admin.IdentityUserId);
            return View(admin);
        }

        // POST: Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Admin admin)
        {
            if (id != admin.AdminId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(admin);
                    await _context.SaveChangesAsync(); 
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminExists(admin.AdminId))
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
            return View(admin);
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = _context.Admins
                .Include(c => c.IdentityUser)
                .FirstOrDefault(m => m.AdminId == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // POST: Admin/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var admin = _context.Customers.Find(id);
            _context.Customers.Remove(admin);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminExists(int id)
        {
            return _context.Admins.Any(e => e.AdminId == id);
        }

        public ActionResult ChooseCustomer(int id)
        {
            var allCustomers = _context.Customers.ToList();
            Response response = new Response();


            var thisSurvey = _context.Surveys.Where(s => s.SurveyId == id).FirstOrDefault();


            return View(allCustomers);
        }

        public ActionResult Compare(string FirstSelection)
        {
            var selectedQuarter = _context.Surveys.Where(s => s.Quarter == FirstSelection).ToList();

            return View(selectedQuarter);
        }


        public ActionResult GoogleCharts()
        {
            return View();
        }

        public IActionResult GetChartData()
        {
            //var x = _context.Responses.Where( r=> r.OptionsId )


            var data = _context.Responses.ToList();
            return Json(data);
        }


        // email mailkit stuff

        public ActionResult SendMail()
        {


            //InternetAddress test = new InternetAddress();  < --what is an InternetAddress ?

            MailboxAddress test2 = new MailboxAddress("Mike", "zarandazchi@gmail.com"); /* < --hardcoding an address-checking teh overloads to see what parameters work.*/
        

            List<MailboxAddress> addresses = new List<MailboxAddress>();
            var allCustomers = _context.Customers.ToList();

            // loop

            for (int i = 0; i < allCustomers.Count; i++)
            {
                MailboxAddress tempMailBox = new MailboxAddress("", "");

                tempMailBox.Name = allCustomers[i].FirstName;
                tempMailBox.Address = allCustomers[i].Email;

                addresses.Add(tempMailBox);
            }
   
            var message = new MimeMessage();
            //message.To.AddRange(emailMessage.ToAddresses.Select(x => new MailboxAddress("to email name", " to email address")));
            //message.From.AddRange(emailMessage.FromAddresses.Select(x => new MailboxAddress("from email name", "from email address")));
            message.To.AddRange(addresses);
            message.From.Add(test2);

            message.Subject = "here write email subject";

            message.Body = new TextPart(TextFormat.Html)
            {
                Text = "kjasdfhkjasdh" //changed this from an input box since all emails will be the same 
            };

            using (var emailClient = new SmtpClient())
            {
                emailClient.Connect("smtp.gmail.com", 465, true);

                emailClient.Authenticate("Survey.Company.Project@gmail.com", "Password_1");

                emailClient.Send(message);

                emailClient.Disconnect(true);

                return RedirectToAction("Index");

            }
        }

     }
}