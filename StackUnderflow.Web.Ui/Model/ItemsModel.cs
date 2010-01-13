using System.Collections.Generic;
using StackUnderflow.Model.Entities;

namespace StackUnderflow.Web.Ui.Model
{
    public class ItemsModel<T> : ModelBase
    {
        public ItemsModel(User loggedInUser, IEnumerable<T> items) : base(loggedInUser)
        {
            Items = items;
        }

        public IEnumerable<T> Items { get; set;}
    }
}