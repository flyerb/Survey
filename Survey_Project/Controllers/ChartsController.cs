using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Survey_Project.Controllers
{
    public class ChartsController : Controller
    {
        [Route("product")]
        public class ProductController : Controller
        {
            public IActionResult Index()
            {

                return View();
            }
        }
    }
}