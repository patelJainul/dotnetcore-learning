using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models;
using Models.Configurations;

namespace SocialMediaLinksAssignment.Controllers
{
    public class HomeController(
        IOptions<SocialMediaLinksOptions> options,
        IWebHostEnvironment webHostEnvironment
    ) : Controller
    {
        // GET: HomeController
        public ActionResult Index()
        {
            SocialMediaLinksOptions socialMediaLinks = options.Value;

            SocialLink[] socialLinks;
            if (webHostEnvironment.IsDevelopment())
            {
                socialLinks =
                [
                    new SocialLink { Name = "Facebook", Link = socialMediaLinks.Facebook },
                    new SocialLink { Name = "Twitter", Link = socialMediaLinks.Twitter },
                    new SocialLink { Name = "YouTube", Link = socialMediaLinks.Youtube },
                ];
                return View(socialLinks);
            }

            socialLinks =
            [
                new SocialLink { Name = "Facebook", Link = socialMediaLinks.Facebook },
                new SocialLink { Name = "Twitter", Link = socialMediaLinks.Twitter },
                new SocialLink { Name = "Instagram", Link = socialMediaLinks.Instagram },
                new SocialLink { Name = "YouTube", Link = socialMediaLinks.Youtube },
            ];

            return View(socialLinks);
        }
    }
}
