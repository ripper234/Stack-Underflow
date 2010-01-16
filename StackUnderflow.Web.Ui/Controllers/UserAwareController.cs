using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StackUnderflow.Model.Entities;
using StackUnderflow.Persistence.Repositories;
using StackUnderflow.Web.Ui.Model;

namespace StackUnderflow.Web.Ui.Controllers
{
    public abstract class UserAwareController : Controller
    {
        private User _currentUser;
        public IUserRepository Users { get; private set; }

        protected UserAwareController(IUserRepository userRepository)
        {
            Users = userRepository;
        }

        private ModelBase CreateEmptyModel()
        {
            return new EmptyModel(CurrentUser);
        }

        private ModelBase CreateSingleModel<T>(T item)
        {
            return new ItemModel<T>(CurrentUser, item);
        }

        private ModelBase CreateMultipleModel<T>(T[] items)
        {
            return new ItemsModel<T>(CurrentUser, items);
        }

        private ModelBase CreateMultipleModel<T>(IEnumerable<T> items)
        {
            return new ItemsModel<T>(CurrentUser, items);
        }

        protected User CurrentUser
        {
            get
            {
                if (_currentUser != null)
                    return _currentUser;

                var id = User.Identity.Name;
                if (string.IsNullOrEmpty(id))
                    return null;

                _currentUser = Users.GetById(int.Parse(id));
                return _currentUser;
            }
        }

        protected ActionResult MultipleUserView<T>(IEnumerable<T> items)
        {
            return MultipleUserView(items.ToList());
        }

        protected ActionResult MultipleUserView<T>(T[] items)
        {
            return MultipleUserView(items.ToList());
        }

        protected ActionResult MultipleUserView<T>(IList<T> items)
        {
            return View(CreateMultipleModel(items));
        }

        protected ActionResult SingleUserView<T>(T item)
        {
            return View(CreateSingleModel(item));
        }

        protected ActionResult EmptyUserView()
        {
            return View(CreateEmptyModel());
        }
    }
}