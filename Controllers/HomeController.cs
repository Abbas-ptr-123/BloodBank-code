using BloodBank.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Diagnostics;
using System.Security.Claims;


namespace BloodBank.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }



        public IActionResult GetHelp()
        {
            return View();
        }



        public IActionResult AboutUs()
        {
            return View();
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
     
     
        // Updated Seeker Action


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public override bool Equals(object? obj)
        {
            return obj is HomeController controller &&
                   EqualityComparer<HttpContext>.Default.Equals(HttpContext, controller.HttpContext) &&
                   EqualityComparer<HttpRequest>.Default.Equals(Request, controller.Request) &&
                   EqualityComparer<HttpResponse>.Default.Equals(Response, controller.Response) &&
                   EqualityComparer<RouteData>.Default.Equals(RouteData, controller.RouteData) &&
                   EqualityComparer<ModelStateDictionary>.Default.Equals(ModelState, controller.ModelState) &&
                   EqualityComparer<ControllerContext>.Default.Equals(ControllerContext, controller.ControllerContext) &&
                   EqualityComparer<IModelMetadataProvider>.Default.Equals(MetadataProvider, controller.MetadataProvider) &&
                   EqualityComparer<IModelBinderFactory>.Default.Equals(ModelBinderFactory, controller.ModelBinderFactory) &&
                   EqualityComparer<IUrlHelper>.Default.Equals(Url, controller.Url) &&
                   EqualityComparer<IObjectModelValidator>.Default.Equals(ObjectValidator, controller.ObjectValidator) &&
                   EqualityComparer<ProblemDetailsFactory>.Default.Equals(ProblemDetailsFactory, controller.ProblemDetailsFactory) &&
                   EqualityComparer<ClaimsPrincipal>.Default.Equals(User, controller.User) &&
                   EqualityComparer<ViewDataDictionary>.Default.Equals(ViewData, controller.ViewData) &&
                   EqualityComparer<ITempDataDictionary>.Default.Equals(TempData, controller.TempData) &&
                   EqualityComparer<dynamic>.Default.Equals(ViewBag, controller.ViewBag);
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(HttpContext);
            hash.Add(Request);
            hash.Add(Response);
            hash.Add(RouteData);
            hash.Add(ModelState);
            hash.Add(ControllerContext);
            hash.Add(MetadataProvider);
            hash.Add(ModelBinderFactory);
            hash.Add(Url);
            hash.Add(ObjectValidator);
            hash.Add(ProblemDetailsFactory);
            hash.Add(User);
            hash.Add(ViewData);
            hash.Add(TempData);
            hash.Add(ViewBag);
            return hash.ToHashCode();
        }
    }
}
