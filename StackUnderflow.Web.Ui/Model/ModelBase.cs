using StackUnderflow.Model.Entities;

namespace StackUnderflow.Web.Ui.Model
{
    public abstract class ModelBase
    {
        protected ModelBase(User loggedInUser)
        {
            LoggedInUser = loggedInUser;
        }


        public User LoggedInUser {get; set;}
    }
}