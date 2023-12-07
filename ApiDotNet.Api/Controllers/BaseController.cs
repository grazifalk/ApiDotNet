using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiDotNet.Api.Controllers
{
    [Authorize]
    [ApiController]
    public class BaseController : ControllerBase
    {
        [NonAction]
        public bool ValidPermission(List<string> permissionUser, List<string> permissionNeeded)
        {
            return permissionNeeded.Any(x => permissionUser.Contains(x)); //as permissões que ele precisa tem que conter nas permissões dele
        }

        [NonAction]
        // 403 tem acesso mas não tem permissão
        public IActionResult Forbidden()
        {
            var obj = new
            {
                code = "permissao_negada",
                message = "Usuário não tem permissão para acessar esse recurso"
            };

            return new ObjectResult(obj) { StatusCode = 403 };
        }
    }
}
