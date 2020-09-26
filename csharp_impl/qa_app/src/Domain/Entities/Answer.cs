namespace Domain.Entities
{
    public class Answer
    {
        public long AnswerId { get; set; }
        public User User { get; set; }
        public string Message { get; set; }

        public Answer(long answerId, User author, string msg)
        {
            this.AnswerId = answerId;
            this.User = author;
            this.Message = msg;
        }
    }
}
