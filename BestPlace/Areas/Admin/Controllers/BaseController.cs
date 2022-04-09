using BestPlace.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestPlace.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = UserConstants.Roles.Administrator)]
    public class BaseController : Controller
    {

    }
}
