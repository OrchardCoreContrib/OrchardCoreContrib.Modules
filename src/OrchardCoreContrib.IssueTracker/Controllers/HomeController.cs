using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrchardCoreContrib.IssueTracker.Controllers;
public class HomeController : Controller
{
    public ActionResult Index()
    {
        return View();
    }
}
