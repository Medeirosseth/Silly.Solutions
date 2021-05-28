using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Factory.Models;

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
    public IActionResult Index()
    {
      IQueryable<Engineer> engineers = _db.Engineers;
      IQueryable<Machine> machines = _db.Machines;
      Dictionary<string, int> model = new Dictionary<string, int>() {};
      model.Add("engineers", engineers.Count());
      model.Add("machines", machines.Count());
      return View(model);
    }
  }
}