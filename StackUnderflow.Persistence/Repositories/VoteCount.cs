namespace StackUnderflow.Persistence.Repositories
{
    public class VoteCount
    {
        public VoteCount(long thumbUps, long thumbDowns)
        {
            checked
            {
                ThumbUps = (int) thumbUps;
                ThumbDowns = (int) thumbDowns;
            }
        }

        public int ThumbUps { get; set; }
        public int ThumbDowns { get; set; }
    }
}