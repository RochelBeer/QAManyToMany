using Microsoft.AspNetCore.Mvc;
using QAManyToMany.Data;
using QAManyToMany55.Web.Models;

namespace QAManyToMany55.Web.Controllers
{
    public class QuestionsController : Controller
    {
        private string _connectionString;
        public QuestionsController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }
        public IActionResult AskAQuestion()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/account/login");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Add(Question question, List<string> tags)
        {
            QATRepo repo = new(_connectionString);
            question.UserId = GetCurrentUserId();
            repo.AddQuestion(question, tags);
            return Redirect("/home/index");
        }
        public IActionResult ViewQuestion(int id)
        {
            QATRepo repo = new(_connectionString);
            ViewModel viewModel = new()
            {
                Question = repo.GetQuestion(id)
            };
            return View(viewModel);
        }
        public IActionResult AddAnswer(Answer answer, int questionId)
        {
            QATRepo repo = new(_connectionString);
            answer.QuestionId = questionId;
            answer.UserId = GetCurrentUserId();
            answer.DatePosted = DateTime.Now;
            repo.AddAnswer(answer);
            return Redirect($"/questions/viewQuestion?id={questionId}");
        }
        private int GetCurrentUserId()
        {
            UserRepo repo = new(_connectionString);
            if (!User.Identity.IsAuthenticated)
            {
                return default;
            }
            User user = repo.GetUserByEmail(User.Identity.Name);
            if (user == null)
            {
                return default;
            }
            return user.Id;
        }
    }
}
