namespace StackUnderflow.Model.Entities.Rich
{
    public class RichQuestion
    {
        public RichQuestion(Question question, int votes)
        {
            Question = question;
            Votes = votes;
        }

        public Question Question { get; private set; }
        public int Votes { get; private set; }
    }
}
