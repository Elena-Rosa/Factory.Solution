using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Factory.Controllers
{
  public class HomeController : Controller
  {
    private readonly FactoryContext _db;

    public HomeController(FactoryContext db)
    {
      _db = db;
    }

    [HttpGet("/")]

    public ActionResult Index()
    {
      ViewBag.EmployeeDirectory = new List<Engineer>(_db.Engineers.OrderBy(engineer => engineer.Name));
      ViewBag.MachineInventory = new List<Machine>(_db.Machines.OrderBy(machine => machine.MachineDescription));
      return View();
    }


  }
}