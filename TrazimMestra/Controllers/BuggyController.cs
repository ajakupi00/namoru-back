using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrazimMestra.Attributes;

namespace TrazimMestra.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly ApplicationContext _context;

        public BuggyController(ApplicationContext context)
        {
            _context = context;
        }
        [HttpGet("testauth")]
        [MyAuthorize]
        public ActionResult<string> GetSecretText()
        {
            return "secret stuff";
        }


    }
}
