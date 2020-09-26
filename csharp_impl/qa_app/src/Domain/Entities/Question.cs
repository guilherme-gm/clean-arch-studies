#nullable enable
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Question
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public List<Answer>? Answers { get; set; }
        public Answer? CorrectAnswer { get; set; }
        public User CreatedBy { get; set; }

        public Question(string title, string message, User creator) : this(0, title, message, creator) { }

        public Question(long id, string title, string message, User creator)
        {
            this.Id = id;
            this.Title = title;
            this.Message = message;
            this.CreatedBy = creator;
        }

        public void Validate()
        {
            if (this.Title.Length < 1)
                throw new Exception("Invalid title");
            if (this.Message.Length < 1)
                throw new Exception("Invalid message");
        }

    }
}