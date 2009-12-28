namespace StackUnderflow.Persistence.Repositories
{
    public class VoteCount
    {
        public VoteCount(int thumbUps, int thumbDowns)
        {
            ThumbUps = thumbUps;
            ThumbDowns = thumbDowns;
        }

        public int ThumbUps { get; set;}
        public int ThumbDowns { get; set;}
    }
}