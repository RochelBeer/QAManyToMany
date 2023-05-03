using Microsoft.AspNetCore.Mvc;
using QAManyToMany.Data;
using QAManyToMany55.Web.Models;
using System.Diagnostics;

namespace QAManyToMany55.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString;
        public HomeController(IConfiguration configuration)
        {
           _connectionString = configuration.GetConnectionString("ConStr");
        }  
        public IActionResult Index()
        {
            QATRepo repo = new(_connectionString);

            ViewModel viewModel = new()
            {
                Questions = repo.GetQuestions()
            };
            return View(viewModel);
        }
      

       
    }
}