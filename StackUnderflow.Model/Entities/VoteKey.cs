#region

using System;
using Castle.ActiveRecord;

#endregion

namespace StackUnderflow.Model.Entities
{
    [Serializable]
    public class VoteKey : IEquatable<VoteKey>
    {
        public VoteKey()
        {
        }

        public VoteKey(int userId, int questionId)
        {
            UserId = userId;
            QuestionId = questionId;
        }

        [KeyProperty]
        public int UserId { get; set; }

        [KeyProperty]
        public int QuestionId { get; set; }

        #region IEquatable<VoteKey> Members

        public bool Equals(VoteKey other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.UserId == UserId && other.QuestionId == QuestionId;
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (VoteKey)) return false;
            return Equals((VoteKey) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (UserId*397) ^ QuestionId;
            }
        }

        public static bool operator ==(VoteKey left, VoteKey right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(VoteKey left, VoteKey right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return string.Format("UserId: {0}, QuestionId: {1}", UserId, QuestionId);
        }
    }
}