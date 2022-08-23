using System.Collections.Generic;
using System;

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
    public virtual ICollection<AuthorBook> JoinAuthorBook { get; set; }
  }
}