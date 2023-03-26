using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace xptoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculateController : ControllerBase
    {
        private readonly CalculateService _calculateService;

        public CalculateController(CalculateService calculateService) 
        {
            _calculateService = calculateService;
        }


        [HttpPost]
        public ActionResult<Result> Post(Call request)
        {
            var result = _calculateService.CalculateValue(request);
            return Ok(result);
        }
    }

}
