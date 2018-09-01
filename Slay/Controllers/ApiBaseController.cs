namespace Slay.Host.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public abstract class ApiBaseController : ControllerBase
    {
        protected string GetBaseUrl()
        {
            return Request.Scheme + "://" + Request.Host + Request.PathBase.Value.TrimEnd('/') + "/";
        }
    }
}