using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Factory.Controllers
{
  public class EngineersController : Controller
  {
    private readonly FactoryContext _db;

    public EngineersController(FactoryContext db)
    {
      _db = db;
    }


    public ActionResult Index()
    {
      List<Engineer> model = _db.Engineers.OrderBy(engineer => engineer.Name).ToList();
      return View(model);
    }

    public ActionResult Create(int id)
    {
      Engineer thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
      ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "Name");
      return View(thisEngineer);
    }


    [HttpPost]
    public ActionResult Create(Engineer engineer, int MachineId)
    {
      if (!ModelState.IsValid)
      {
        ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "Name");
        return View(engineer);
      }
      _db.Engineers.Add(engineer);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }



    public ActionResult Details(int id)
    {
      var thisEngineer = _db.Engineers
                              .Include(engineer => engineer.JoinEntities)
                              .ThenInclude(join => join.Machine)
                              .FirstOrDefault(engineer => engineer.EngineerId == id);
      return View(thisEngineer);
    }


    public ActionResult Edit(int id)
    {
      var thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
      ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "Name");
      return View(thisEngineer);
    }

    [HttpPost]

    public ActionResult Edit(Engineer engineer)
    {
      // _db.Entry(engineer).State = EntityState.Modified;
      // _db.SaveChanges();
      // return RedirectToAction("Details", new { id = engineer.EngineerId });

      if (!ModelState.IsValid)
      {
        return View(engineer);
      }
      _db.Engineers.Update(engineer);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddMachine(int id)
    {
      var thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
      ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "Name");
      return View(thisEngineer);
    }

    [HttpPost]

    public ActionResult AddMachine(Engineer engineer, int MachineId)
    {
      if (MachineId != 0)
      {
        if (_db.EngineerMachines.Any(join => join.MachineId == MachineId && join.EngineerId == engineer.EngineerId) == false)
          _db.EngineerMachines.Add(new EngineerMachine() { MachineId = MachineId, EngineerId = engineer.EngineerId });
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = engineer.EngineerId });
    }

    public ActionResult Delete(int id)
    {
      var thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
      return View(thisEngineer);
    }
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
      _db.Engineers.Remove(thisEngineer);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]

    public ActionResult DeleteMachine(int joinId)
    {
      var joinEntry = _db.EngineerMachines.FirstOrDefault(entry => entry.EngineerMachineId == joinId);
      _db.EngineerMachines.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = joinEntry.EngineerId });
    }

  }
}

