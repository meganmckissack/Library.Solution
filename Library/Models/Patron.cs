using System.Collections.Generic;

namespace Library.Models
{
    public class Patron
    {
        public Patron()
        {
            this.JoinCheckouts = new HashSet<Checkout>();
        }

        public int PatronId { get; set; }
        public string PatronName { get; set; }
        public virtual ICollection<Checkout> JoinCheckouts { get; set; }
    }
}