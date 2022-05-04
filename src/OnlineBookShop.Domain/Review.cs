namespace OnlineBookShop.Domain
{
    public class Review : BaseEntity
    {
        public string VoterName { get; set; }

        public short NumStars { get; set; }

        public string Comment { get; set; }

        public int BookId { get; set; }

        public Book Book { get; set; }
    }
}
