using StackUnderflow.Model.Entities;

namespace StackUnderflow.Web.Ui.Model
{
    public class ItemModel<T> : ModelBase
    {
        public ItemModel(User loggedInUser, T item) : base(loggedInUser)
        {
            Item = item;
        }

        public T Item { get; set; }
    }
}