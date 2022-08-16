using System.Collections.Generic;

namespace Library.Models
{
    public class Copy
    {
        public Copy()
        {
            this.Books = new HashSet<Book>();
            this.JoinBookCopy = new HashSet<BookCopy>();
        }

        public int CopyId { get; set; }
        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<BookCopy> JoinBookCopy { get; set; }
    }
}