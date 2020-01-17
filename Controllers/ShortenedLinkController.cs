using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using LinkShortener.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace LinkShortener.Controllers
{
    [ApiController]
    [Session]
    public class ShortenedLinkController : ControllerBase
    {
        private readonly IShortenedLinkService _shortenedLinkService;
        private readonly IConfiguration _configuration;
        private readonly string _domainName;
        
        public ShortenedLinkController(IShortenedLinkService shortenedListService, IConfiguration configuration)
        {
            _shortenedLinkService = shortenedListService;
            _configuration = configuration;
            _domainName = configuration.GetValue<string>("DomainName");
        }

        [HttpGet("/create")]
        public async Task<IActionResult> CreateShortenedLink(string fullLink)
        {
            var userId = HttpContext.Session.GetString("UserId");
            var shortenedLink = await _shortenedLinkService.CreateShortenedLinkAsync(fullLink, userId);

            return Ok($"{_domainName}/{shortenedLink.Link}");
        }

        [HttpGet("/{shortenedLink}")]
        public async Task<IActionResult> GetFullLink(string shortenedLink)
        {
            var link = await _shortenedLinkService.GetLinkByShortenedLinkAsync(shortenedLink);

            if (link == null)
                return NotFound();

            return Ok(link);
        }
        
        [HttpGet("/my_links")]
        public async Task<IActionResult> GetLinksByUser()
        {
            var userId = HttpContext.Session.GetString("UserId");
            var links = await _shortenedLinkService.GetShortenedLinksByUserIdAsync(userId);

            return Ok(links);
        }

        [HttpGet("/test")]
        public IActionResult Test()
        {
            return Ok(HttpContext.Session.GetString("UserId"));
        }

    }
}