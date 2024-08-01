namespace OnlineBookShop.Application.App.Books.Responses
{
    public class TopRatedBookDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime PublishedOn { get; set; }

        public int PublisherId { get; set; }

        public decimal Price { get; set; }
    }
}
