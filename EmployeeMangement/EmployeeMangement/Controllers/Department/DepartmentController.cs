using Microsoft.AspNetCore.Mvc;

namespace EmployeeMangement.Controllers
{
    public class DepartmentController : Controller
    {
        public DepartmentController() { }
        public async Task<IActionResult> DepartmentList()
        {
            return PartialView("~/Views/Shared/Page/_DepartmentList.cshtml");
        }
    }
}
