using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
using Library.Models;

namespace Library.Controllers
{
  public class CopiesController : Controller
  {
    private readonly LibraryContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public CopiesController(UserManager<ApplicationUser> userManager, LibraryContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public ActionResult Index()
    {
    return View(_db.Copies.ToList());
    }

    [Authorize]
    public ActionResult Create()
    {
      ViewBag.PatronId = new SelectList(_db.Patrons, "PatronId", "PatronName");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Copy copy, int PatronId)
    {
      _db.Copies.Add(copy);
      _db.SaveChanges();
      if (PatronId !=0)
      {
        _db.Checkouts.Add(new Checkout() {
          PatronId = PatronId, CopyId = copy.CopyId
        });
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisCopy = _db.Copies
        .Include(copy => copy.JoinCheckouts)
        .ThenInclude(join => join.Patron)
        .FirstOrDefault(copy => copy.
        CopyId == id);
      return View(thisCopy);
    }

    [Authorize]
    public ActionResult Edit(int id)
    {
      var thisCopy = _db.Copies.FirstOrDefault(copy => copy.CopyId == id);
      ViewBag.PatronId = new SelectList(_db.Patrons, "PatronId", "PatronName");
      return View(thisCopy);
    }

    [HttpPost]
    public ActionResult Edit(Copy copy, int PatronId)
    {
      if (PatronId != 0)
        {
          _db.Checkouts.Add(new Checkout() { PatronId = PatronId, CopyId = copy.CopyId });
        }
        _db.Entry(copy).State = EntityState.Modified;
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

    [Authorize]
    public ActionResult AddAuthor(int id)
    { 
      var thisCopy = _db.Copies.FirstOrDefault(copy => copy.CopyId == id);
      ViewBag.PatronId = new SelectList(_db.Patrons, "PatronId", "PatronName");
      return View(thisCopy);
    }

    [HttpPost]
    public ActionResult AddAuthor(Copy copy, int PatronId)
    {
      if (PatronId != 0)
      {
        _db.Checkouts.Add(new Checkout() { PatronId = PatronId, CopyId = copy.CopyId });
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    [Authorize]
    public ActionResult Delete(int id)
    {
      var thisCopy = _db.Copies.FirstOrDefault(copy => copy.CopyId == id);
      return View(thisCopy);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisCopy = _db.Copies.FirstOrDefault(copy => copy.CopyId == id);
      _db.Copies.Remove(thisCopy);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [Authorize]
    [HttpPost]
    public ActionResult DeletePatron(int joinId)
    {
      var joinEntry = _db.Checkouts.FirstOrDefault(entry => entry.CheckoutId == joinId);
      _db.Checkouts.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}