using Microsoft.AspNetCore.Mvc;

namespace Jago.System.UI.Controllers
{
    public abstract class BaseController<TViewModel> : Controller where TViewModel : class, new()
    {
        protected BaseController()
        {
        }

        public abstract IEnumerable<TViewModel> GetRows();
        public abstract TViewModel GetRow(Guid id);

        public abstract void LoadViewBags();
        #region Not necessary for this application
        //protected IActionResult ViewForm(Guid id)
        //{
        //    LoadViewBags();

        //    if (id == Guid.Empty)
        //        return View(new TViewModel());
        //    else
        //        return View(GetRow(id));
        //}

        //protected IActionResult ViewDefault(string action, TViewModel vm, ValidationResult validationResult)
        //{
        //    LoadViewBags();
        //    if (!ModelState.IsValid)
        //        return View(vm);

        //    if (validationResult.Errors.Count > 0)
        //    {
        //        foreach (var error in validationResult.Errors)
        //        {
        //            AddError(error.PropertyName, error.ErrorMessage);
        //        }
        //        return View(vm);
        //    }
        //    else
        //    {
        //        return RedirectToAction(action);
        //    }
        //}

        // private readonly ICollection<string> _errors = new List<string>();
        // private void AddError(string field, string error) => _errors.Add($"{field}|{error}");
        #endregion
    }

}
