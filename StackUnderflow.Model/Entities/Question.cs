namespace StackUnderflow.Persistence.Entities
{
    public class Question
    {
        public int Id { get; private set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public User Author { get; set; }
    }
}