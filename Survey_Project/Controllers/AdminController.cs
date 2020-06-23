using System;
using System.Collections.Generic;
using System.Linq;
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




        // email mailkit stuff

        //public ActionResult SendMail()
        //{
        //    var message = new MimeMessage();
        //    message.To.AddRange(emailMessage.ToAddresses.Select(x => new MailboxAddress("to email name", " to email address")));
        //    message.From.AddRange(emailMessage.FromAddresses.Select(x => new MailboxAddress("from email name", "from email address")));

        //    message.Subject = "here write email subject";

        //    message.Body = new TextPart(TextFormat.Html)
        //    {
        //        Text = emailMessage.Content
        //    };

        //    using (var emailClient = new SmtpClient())
        //    {
        //        emailClient.Connect("smtp.gmail.com", 465, true);

        //        emailClient.Authenticate("User@mydomain.com", "pass123");

        //        emailClient.Send(message);

        //        emailClient.Disconnect(true);

        //        return "";

        //    }
        //}

        //public List<EmailMessage> ReceiveEmail(int maxCount = 15)
        //{
        //    using (var emailClient = new Pop3Client())
        //    {
        //        emailClient.Connect("Pop Server", "Pop Port", true);

        //        emailClient.AuthenticationMechanisms.Remove("XOAUTH2"); emailClient.Authenticate("Pop Username", "Pop Password");

        //        List<EmailMessage> emails = new List<EmailMessage>();
        //        for (int i = 0; i < emailClient.Count && i < maxCount; i++)
        //        {
        //            var message = emailClient.GetMessage(i);
        //            var emailMessage = new EmailMessage
        //            {
        //                Content = !string.IsNullOrEmpty(message.HtmlBody) ?
        //                message.HtmlBody : message.TextBody,
        //                Subject = message.Subject
        //            };
        //            emailMessage.ToAddresses.AddRange(message.To.Select(x => (MailboxAddress)x).Select(x => new EmailAddress { Address = x.Address, Name = x.Name }));
        //            emailMessage.FromAddresses.AddRange(message.From.Select(x => (MailboxAddress)x).Select(x => new EmailAddress { Address = x.Address, Name = x.Name }));
        //        }
        //        return emails;
        //    }
        //}
    }
}