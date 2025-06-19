using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using licenses.Models;

namespace licenses.Controllers;

public class LicenseController : Controller
{
    private readonly ILogger<LicenseController> _logger;

    public LicenseController(ILogger<LicenseController> logger)
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
            // Логіка збереження ліцензії
            ViewBag.Message = "License was created successfully.";
            ViewBag.Submitted = true;
        }
        catch
        {
            ViewBag.Message = "There was an error while creating the license.";
            ViewBag.Submitted = false;
        }
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
