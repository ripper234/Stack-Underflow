using NUnit.Framework;

namespace StackUnderflow.Tests.Persistence
{
    [TestFixture]
    public class VoteRepositoryTests : RepositoryTestBase
    {
        [Test]
        public void UnvotedQuestion_CountVotesIsZero()
        {
            var user = SaveUser();
            var question = SaveQuestion(user);

            Assert.Equals(0, VoteRepository.GetVoteCount(question.Id));
        }
    }
}
