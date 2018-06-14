using System;
using System.Collections.Generic;
using AspNetCoreSample.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AspNetCoreSample.Controllers
{
    [Route("/CodeElementsHook")]
    [ApiController]
    public class WebHookController : Controller
    {
        private readonly CodeElementsWebHookOptions _options;

        public WebHookController(IOptions<CodeElementsWebHookOptions> options)
        {
            _options = options.Value;
        }

        [HttpPost]
        public IActionResult IssueLicenseCallback(LicenseWebhookInfo licenseWebhookInfo, [FromQuery] string secret)
        {
            if (secret != _options.Secret)
                return BadRequest();

            //process data
            
            return Ok("*ok*");
        }
    }

    public class LicenseWebhookInfo
    {
        public string LicenseKey { get; set; }
        public int LicenseId { get; set; }
        public DateTimeOffset? ExpirationDate { get; set; } //null
        public string LicenseType { get; set; }
        public int LicenseTypeId { get; set; }
        public string PaymentMethod { get; set; } //null
        public int? PaymentMethodId { get; set; } //null
        public string CustomerName { get; set; } //null
        public string CustomerEmail { get; set; }
        public List<LicenseAttributeDto> Attributes { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
    }

    public class LicenseAttributeDto
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public LicenseAttributeCategory Category { get; set; }
    }

    public enum LicenseAttributeCategory
    {
        Payment = 0,
        Customer = 1
    }
}