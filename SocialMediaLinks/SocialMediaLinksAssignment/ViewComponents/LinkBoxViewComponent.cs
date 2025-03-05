using System;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace SocialMediaLinksAssignment.ViewComponents;

public class LinkBoxViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(SocialLink link)
    {
        return View(link);
    }
}
