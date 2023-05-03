using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QAManyToMany.Data
{
    public class QATRepo
    {
        private string _connectionString;
        public QATRepo(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddQuestion(Question question, List<string> tags)
        {
            using var context = new QADbContext(_connectionString);
            question.DatePosted = DateTime.Now;
            context.Questions.Add(question);
            context.SaveChanges();
            foreach(string tag in tags)
            {
                Tag t = GetTag(tag);
                int tagId;
                if(t == null)
                {
                    tagId = AddTag(tag);
                }
                else
                {
                    tagId = t.Id;
                }
                context.QuestionTags.Add(new QuestionTags
                {
                    QuestionId = question.Id,
                    TagId = tagId
                });
            }
            context.SaveChanges();
        }
        private Tag GetTag(string name)
        {
            using var context = new QADbContext(_connectionString);
            return context.Tags.FirstOrDefault(t => t.Name == name);
        }
        private int AddTag(string name)
        {

            using var context = new QADbContext(_connectionString);
            Tag tag = new() { Name = name };
            context.Tags.Add(tag);
            context.SaveChanges();
            return tag.Id;

        }
        public List<Question> GetQuestions()
        {
            using var context = new QADbContext(_connectionString);
            return context.Questions.Include(q => q.Answers).Include(q => q.User)
                .Include(q => q.QuestionsTags).ThenInclude(qt => qt.Tag).ToList();
        }
        public Question GetQuestion(int id)
        {
            using var context = new QADbContext(_connectionString);
            return context.Questions.Include(q => q.Answers).Include(q => q.User).FirstOrDefault(q => q.Id == id);
        }
        public void AddAnswer(Answer answer)
        {
            using var context = new QADbContext(_connectionString);
            context.Answers.Add(answer);
            context.SaveChanges();
        }
    }
}
