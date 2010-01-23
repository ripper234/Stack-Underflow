using System;
using System.Collections.Generic;
using System.Linq;
using Castle.ActiveRecord;
using NHibernate;
using NHibernate.Exceptions;
using StackUnderflow.Common;
using StackUnderflow.Model.Entities;
using StackUnderflow.Model.Entities.DB;

namespace StackUnderflow.Persistence.Repositories
{
    public abstract class PostVoteRepository<TVoteOnPost, TPost> : RepositoryBase<TVoteOnPost> 
        where TVoteOnPost : VoteOnPost, new()
        where TPost : Post
    {
        protected PostVoteRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }

        public void CreateOrUpdateVote(User user, TPost post, VoteType voteType)
        {
            CreateOrUpdateVote(user.Id, post.Id, voteType);
        }

        public void CreateOrUpdateVote(int userId, int postId, VoteType voteType)
        {
            var key = new VoteKey {UserId = userId, PostId = postId};
            int existingVoteValue = GetExistingVoteValue(key);
            int voteDiff = GradeVote(voteType) - existingVoteValue;
            var vote = new TVoteOnPost
                           {
                               Key = key,
                               Vote = voteType
                           };
            try
            {
                ActiveRecordMediator<TVoteOnPost>.Create(vote);
            }
            catch (GenericADOException)
            {
                ActiveRecordMediator<TVoteOnPost>.Update(vote);
            }
            // todo - use this instead of the above
            // http://stackoverflow.com/questions/2077949/castle-activerecord-save-is-throwing-stalestateexception
            // ActiveRecordMediator<TVoteOnPost>.Save(vote);
            UpdateVotesOnAnswer(postId, voteDiff);
        }

        private void UpdateVotesOnAnswer(int postId, int voteDiff)
        {
            using (var session = SessionFactory.OpenSession())
            {
                var query =
                    session.CreateQuery("UPDATE " + PostTableName +
                                        " set Votes = Votes + :voteDiff where Id = :postId");
                query.SetInt32("voteDiff", voteDiff);
                query.SetInt32("postId", postId);
                query.ExecuteUpdate();
            }
        }

        private static int GradeVote(TVoteOnPost vote)
        {
            if (vote == null)
                return 0;

            return GradeVote(vote.Vote);
        }

        private static int GradeVote(VoteType vote)
        {
            switch (vote)
            {
                case VoteType.ThumbUp:
                    return 1;

                case VoteType.ThumbDown:
                    return -1;

                default:
                    throw new Exception("Illegal vote type: " + vote);
            }
        }

        public void RemoveVote(int voterId, int postId)
        {
            var key = new VoteKey(voterId, postId);
            var voteOnQuestion = new TVoteOnPost {Key = key};
            try
            {
                int votesDelta = - GetExistingVoteValue(key);
                ActiveRecordMediator<TVoteOnPost>.Delete(voteOnQuestion);
                UpdateVotesOnAnswer(postId, votesDelta);
            }
            catch (ObjectNotFoundException)
            {
                // swallow it, the deleted object is really not there
            }
        }

        public VoteCount GetVoteCount(int postId)
        {
            try
            {
                using (var session = SessionFactory.OpenSession())
                {
                    var query =
                        session.CreateQuery(
                            "SELECT Vote, COUNT(*) FROM " + VotesTableName + " WHERE PostId = :postId GROUP BY vote");
                    query.SetInt32("postId", postId);
                    var result = query.List().Cast<object[]>().ToDictionary(
                        x => (VoteType) x[0],
                        x => (long) x[1]);
                    return new VoteCount(result.TryGetValueWithDefault(VoteType.ThumbUp),
                                         result.TryGetValueWithDefault(VoteType.ThumbDown));
                }
            }
            catch (Exception e)
            {
                throw new Exception("Failed to get vote count for question " + postId, e);
            }
        }

        public Dictionary<int, int> GetVoteCount(IEnumerable<int> postIdses)
        {
            try
            {
                var postIdsStr = "";
                bool first = true;
                foreach (var id in postIdses)
                {
                    if (first)
                    {
                        first = false;
                        postIdsStr += id;
                    }
                    else
                        postIdsStr += ", " + id;
                }
                var sql = string.Format("SELECT Key.PostId, Vote, COUNT(*) FROM " + VotesTableName + " WHERE PostId IN ({0}) GROUP BY PostId, vote", postIdsStr);
                using (var session = SessionFactory.OpenSession())
                {
                    var query = session.CreateQuery(sql);
                    var result = query.List().Cast<object[]>().GroupBy(x => x[0]);
                    return result.ToDictionary(x => (int)x.Key, 
                                                   x => x.Sum(y => (int)(((long)y[2]) * GetWeight((VoteType)y[1]))));
                }
            }
            catch (Exception e)
            {
                throw new Exception("Failed to get vote counts for questions " + postIdses, e);
            }
        }

        private static int GetWeight(VoteType voteType)
        {
            switch (voteType)
            {
                case VoteType.ThumbUp:
                    return 1;

                case VoteType.ThumbDown:
                    return -1;

                default:
                    throw new Exception("Unsupported vote type: " + voteType);
            }
        }

        public TVoteOnPost GetVote(User user, TPost question)
        {
            return GetVote(user.Id, question.Id);
        }

        /// <summary>
        /// Returns null if no vote exists
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="postId"></param>
        /// <returns></returns>
        public TVoteOnPost GetVote(int userId, int postId)
        {
            return ActiveRecordBase<TVoteOnPost>.TryFind(new VoteKey(userId, postId));
        }

        private static string VotesTableName
        {
            get { return typeof(TVoteOnPost).Name; }
        }

        private static string PostTableName
        {
            get { return typeof (TPost).Name; }
        }

        private static int GetExistingVoteValue(VoteKey key)
        {
            var existingVote = ActiveRecordMediator<TVoteOnPost>.FindByPrimaryKey(key, false);
            return GradeVote(existingVote);
        }
    }
}