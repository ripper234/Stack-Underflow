using System;
using System.Collections.Generic;
using System.Linq;
using Castle.ActiveRecord;
using NHibernate;
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
            var vote = new TVoteOnPost
                           {
                               Key = new VoteKey { UserId = userId, PostId = postId },
                               Vote = voteType
                           };

            try
            {
                ActiveRecordMediator<TVoteOnPost>.Create(vote);
            }
            catch (Exception)
            {
                ActiveRecordMediator<TVoteOnPost>.Update(vote);
            }
            //ActiveRecordMediator<VoteOnQuestion>.Save(vote);
        }

        public void RemoveVote(int voterId, int postId)
        {
            var voteOnQuestion = new TVoteOnPost {Key = new VoteKey(voterId, postId)};
            try
            {
                ActiveRecordMediator<TVoteOnPost>.Delete(voteOnQuestion);
            }
            catch (Exception)
            {
                // swallow it, the deleted object is really not there
                // todo - should catch a specific exception, not just any
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
                            "SELECT Vote, COUNT(*) FROM " + TableName + " WHERE PostId = :postId GROUP BY vote");
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
                var sql = string.Format("SELECT Key.PostId, Vote, COUNT(*) FROM " + TableName + " WHERE PostId IN ({0}) GROUP BY PostId, vote", postIdsStr);
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

        private static string TableName
        {
            get { return typeof(TVoteOnPost).Name; }
        }
    }
}