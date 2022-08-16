using System.Collections.Generic;

namespace Library.Models
{
    public class Author
    {
        public Author()
        {
            this.JoinAuthorBook = new HashSet<AuthorBook>();
        }

        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public virtual ICollection<AuthorBook> JoinAuthorBook { get; set; }
    }
}