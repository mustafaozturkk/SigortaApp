using SigortaApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace SigortaApp.Web.ViewComponents
{
    public class CommentList : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var commentvalues = new List<UserComment>
            {
                new UserComment()
                {
                    Id=1,
                    UserName="Mustafa"
                },
                new UserComment()
                {
                    Id=2,
                    UserName="Ali"
                },
                new UserComment()
                {
                    Id =3,
                    UserName="Merve"
                }
            };
            return View(commentvalues);
        }
    }
}
