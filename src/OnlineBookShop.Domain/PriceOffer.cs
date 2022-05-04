namespace OnlineBookShop.Domain
{
    public class PriceOffer : BaseEntity
    {
        public decimal NewPrice { get; set; }

        public string PromotionalText { get; set; }

        public int BookId { get; set; }

        public Book Book { get; set; }
    }
}
