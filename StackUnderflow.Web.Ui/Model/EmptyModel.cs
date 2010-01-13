using StackUnderflow.Model.Entities;

namespace StackUnderflow.Web.Ui.Model
{
    public class EmptyModel : ModelBase
    {
        public EmptyModel(User loggedInUser) : base(loggedInUser)
        {
        }
    }
}