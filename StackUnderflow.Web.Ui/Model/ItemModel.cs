using StackUnderflow.Model.Entities;

namespace StackUnderflow.Web.Ui.Model
{
    public class Model<T> : ModelBase
    {
        public Model(User loggedInUser, T item) : base(loggedInUser)
        {
            Item = item;
        }

        public T Item { get; set; }
    }
}