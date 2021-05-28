using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using Factory.Models;

namespace Factory.Controllers 
{
  public class MachinesController : Controller
  {
    private readonly FactoryContext _db;

    public MachinesController(FactoryContext db)
    {
      _db = db;
    }

    [HttpGet("/machines")]
    public ActionResult Index()
    {
      return View(_db.Machines.ToList());
    }

    [HttpGet("/machines/create")]
    public ActionResult Create()
    {
      ViewBag.EngineerId = new SelectList(_db.Engineers, "engineerId", "Name");
      return View();
    }

    [HttpPost("/machines/create")]
    public ActionResult Create(Machine machine, int engineerId)
    {
      _db.Machines.Add(machine);
      _db.SaveChanges();
      if (engineerId != 0)
      {
        _db.EngineerMachines.Add(new EngineerMachine() { EngineerId = engineerId, MachineId = machine.MachineId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpGet("/machines/details/{id}")]
    public ActionResult Details(int id)
    {
      var thisMachine = _db.Machines
      .Include(machine => machine.JoinEntities)
      .ThenInclude(join => join.Engineer)
      .FirstOrDefault(machine => machine.MachineId == id);
      return View(thisMachine);
    }

    [HttpGet("machine/edit/{id}")]
    public ActionResult Edit(int id)
    {
      var thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
      ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId", "EngineerName");
      return View(thisMachine);
    }

    [HttpPost("/machine/edit/{id}"), ActionName("Edit")]
    public ActionResult Edit(Machine machine, int engineerId)
    {
      if (engineerId != 0)
      {
        _db.EngineerMachines.Add(new EngineerMachine() {EngineerId = engineerId, MachineId = machine.MachineId});
      }
      _db.Entry(machine).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("index");
    }

    [HttpGet("/machine/delete/{id}")]
    public ActionResult Delete(int id)
    {
      var thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
      return View(thisMachine);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisMachine = _db.Machines.First(machine => machine.MachineId == id);
      _db.Machines.Remove(thisMachine);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}