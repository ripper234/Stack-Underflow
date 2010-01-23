using System;
using StackUnderflow.Model.Entities.DB;

namespace StackUnderflow.Web.Ui.Utils
{
    public static class ImageHelper
    {
        public static string ImageForVote(VoteType voteType, VoteType? userVote)
        {
            bool selected = voteType == userVote;
            return GetPrefix(voteType) + (selected ? "_on.png" : "_off.png");
        }

        private static string GetPrefix(VoteType voteType)
        {
            const string path = "/Content/images/";
            switch (voteType)
            {
                case VoteType.ThumbUp:
                    return path + "plus";

                case VoteType.ThumbDown:
                    return path + "minus";

                default:
                    throw new Exception("Illegal vote type " + voteType);
            }
        }
    }
}