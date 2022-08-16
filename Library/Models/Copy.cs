using System.Collections.Generic;

namespace Library.Models
{
    public class Copy
    {
        public Copy()
        {
            this.Books = new HashSet<Book>();
            this.JoinBookCopy = new HashSet<BookCopy>();
            this.JoinCheckouts = new HashSet<Checkout>();
        }

        public int CopyId { get; set; }
        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<BookCopy> JoinBookCopy { get; set; }
        public virtual ICollection<Checkout> JoinCheckouts { get; set; }
    }
}