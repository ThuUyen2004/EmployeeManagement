using Microsoft.AspNetCore.Mvc;

namespace EmployeeMangement.Controllers
{
    public class JobPositionController : Controller
    {
        public JobPositionController() { }
        public async Task<IActionResult> JobPositionList()
        {
            return PartialView("~/Views/Shared/Page/_JobPositionList.cshtml");
        }


    }
}
