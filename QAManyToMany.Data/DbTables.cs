using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace QAManyToMany.Data
{

    public class User
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string HashPassword { get; set; }
        public string Email { get; set; }

    }
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime DatePosted { get; set; }

        public List<Answer> Answers { get; set; } = new();


        public List<QuestionTags> QuestionsTags { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
    public class Tag
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public List<QuestionTags> QuestionsTags { get; set; }
    }
    public class QuestionTags
    {
        public int QuestionId { get; set; }
        public int TagId { get; set; }
        public Question Question { get; set; }
        public Tag Tag { get; set; }
    }
    public class Answer
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime DatePosted { get; set; }
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public User User { get; set; }
        public Question Question { get; set; }
    }
}

