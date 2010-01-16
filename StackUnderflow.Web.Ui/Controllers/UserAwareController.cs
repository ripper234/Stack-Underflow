using System.Collections.Generic;
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

        protected ModelBase CreateModel()
        {
            return new EmptyModel(CurrentUser);
        }

        protected ModelBase CreateModel<T>(T item)
        {
            return new ItemModel<T>(CurrentUser, item);
        }

        protected ModelBase CreateModel<T>(T[] items)
        {
            return new ItemsModel<T>(CurrentUser, items);
        }

        protected ModelBase CreateModel<T>(IEnumerable<T> items)
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

        protected ActionResult UserView<T>(IEnumerable<T> items)
        {
            return View(CreateModel(items));
        }

        protected ActionResult UserView<T>(T[] items)
        {
            return View(CreateModel(items));
        }

        protected ActionResult UserView<T>(T item)
        {
            return View(CreateModel(item));
        }

        protected ActionResult UserView()
        {
            return View(CreateModel());
        }
    }
}