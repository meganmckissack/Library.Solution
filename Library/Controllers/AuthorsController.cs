using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using Library.Models;

namespace Library.Controllers
{
  public class AuthorsController : Controller
  {
    private readonly LibraryContext _db;

    public AuthorsController(LibraryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Author> model = _db.Authors.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      ViewBag.BookId = new SelectList(_db.Books, "BookId", "BookTitle");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Author author, int BookId)
    {
      _db.Authors.Add(author);
      _db.SaveChanges();
      if (BookId !=0)
      {
        _db.AuthorBook.Add(new AuthorBook() {
          BookId = BookId, AuthorId = author.AuthorId
        });
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Author thisAuthor = _db.Authors.FirstOrDefault(author => author.AuthorId == id);
      return View(thisAuthor);
    }

    public ActionResult Edit(int id)
    {
      var thisAuthor = _db.Authors.FirstOrDefault(author => author.AuthorId == id);
      return View(thisAuthor);
    }

    [HttpPost]
    public ActionResult Edit(Author author)
    {
      _db.Entry(author).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisAuthor = _db.Authors.FirstOrDefault(author => author.AuthorId == id);
      return View(thisAuthor);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisAuthor = _db.Authors.FirstOrDefault(author => author.AuthorId == id);
      _db.Authors.Remove(thisAuthor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}