using System.Web.Mvc;
using StackUnderflow.Persistence.Repositories;

namespace StackUnderflow.Web.Ui.Controllers
{
    public class UserController : UserAwareController
    {
        public UserController(IUserRepository userRepository) : base(userRepository)
        {
        }

        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /User/Details/5

        public ActionResult Details(int id)
        {
            return SingleUserView(Users.GetById(id));
        }

        //
        // GET: /User/Edit/5
 
        public ActionResult Edit(int id)
        {
            return EmptyUserView();
        }

        //
        // POST: /User/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return EmptyUserView();
            }
        }
    }
}
