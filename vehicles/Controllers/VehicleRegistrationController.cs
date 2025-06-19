using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using vehicles.Models;

namespace vehicles.Controllers;

public class VehicleRegistrationController : Controller
{
    private readonly ILogger<VehicleRegistrationController> _logger;

    public VehicleRegistrationController(ILogger<VehicleRegistrationController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Submitted = false;
        return View();
    }

    [HttpPost]
    public IActionResult Create(IFormCollection collection)
    {
        try
        {
            var id = collection["OwnerID"].ToString();
            var email = collection["EmailAddress"].ToString();
            var vehicle = collection["Vehicle"].ToString();
            var dateRegistered = collection["DateOfRegistration"].ToString();

            VehicleRegistration registration = new()
            {
                OwnerID = id,
                EmailAddress = email,
                Vehicle = vehicle,
                DateOfRegistration = DateTime.Parse(dateRegistered)
            };

            // TODO: Save the vehicle registration in a database

            ViewBag.Message = "Vehicle was registered successfully.";
            ViewBag.Submitted = true;

        }
        catch
        {
            ViewBag.Message = "There was an error while registering the vehicle.";
            ViewBag.Submitted = false;
        }
        return View();
    }

    public IActionResult About()
    {
        ViewData["Message"] = "Motor Vehicle Department";
        return View();
    }

    public IActionResult Contact()
    {
        ViewData["Message"] = "Your contact page.";
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
