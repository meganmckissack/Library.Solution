using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using Library.Models;

namespace Library.Controllers
{
  public class BooksController : Controller
  {
    private readonly LibraryContext _db;

    public BooksController(LibraryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
    return View(_db.Books.ToList());
    }

    public ActionResult Create()
    {
      ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "AuthorName");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Book book, int AuthorId)
    {
      _db.Books.Add(book);
      _db.SaveChanges();
      if (AuthorId !=0)
      {
        _db.AuthorBook.Add(new AuthorBook() {
          AuthorId = AuthorId, BookId = book.BookId
        });
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisBook = _db.Books
        .Include(book => book.JoinAuthorBook)
        .ThenInclude(join => join.Author)
        .FirstOrDefault(book => book.
        BookId == id);
      return View(thisBook);
    }

    public ActionResult Edit(int id)
    {
      var thisBook = _db.Books.FirstOrDefault(book => book.BookId == id);
      ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "AuthorName");
      return View(thisBook);
    }

    [HttpPost]
    public ActionResult Edit(Book book, int AuthorId)
    {
      if (AuthorId != 0)
        {
          _db.AuthorBook.Add(new AuthorBook() { AuthorId = AuthorId, BookId = book.BookId });
        }
        _db.Entry(book).State = EntityState.Modified;
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

    public ActionResult AddAuthor(int id)
    { 
      var thisBook = _db.Books.FirstOrDefault(book => book.BookId == id);
      ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "Name");
      return View(thisBook);
    }

    [HttpPost]
    public ActionResult AddAuthor(Book book, int AuthorId)
    {
      if (AuthorId != 0)
      {
        _db.AuthorBook.Add(new AuthorBook() { AuthorId = AuthorId, BookId = book.BookId });
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }


    public ActionResult Delete(int id)
    {
      var thisBook = _db.Books.FirstOrDefault(book => book.BookId == id);
      return View(thisBook);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisBook = _db.Books.FirstOrDefault(book => book.BookId == id);
      _db.Books.Remove(thisBook);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteAuthor(int joinId)
    {
      var joinEntry = _db.AuthorBook.FirstOrDefault(entry => entry.AuthorBookId == joinId);
      _db.AuthorBook.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}
