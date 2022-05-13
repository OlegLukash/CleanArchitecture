using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineBookShop.API.Controllers
{
    [Authorize]
    [ApiController]
    public abstract class AppBaseController : ControllerBase
    {
    }
}
