using System.Collections.Generic;
using System;
using System.Web;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Library.Models 
{
  public class Book
  {
    public Book()
    {
      this.JoinAuthorBook = new HashSet<AuthorBook>();
    }
    public int BookId { get; set; }
    public string BookTitle { get; set; }
    public virtual ICollection<AuthorBook> JoinAuthorBook { get; }
  }
}