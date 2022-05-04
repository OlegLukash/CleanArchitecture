using System.Collections.Generic;

namespace OnlineBookShop.Domain
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<AuthorBook> AuthorBooks { get; set; }
    }
}
