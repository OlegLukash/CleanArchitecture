using OnlineBookShop.Domain;
using OnlineBookShop.Infrastructure.Persistance.Contexts;

namespace OnlineBookShop.Infrastructure.Persistance.DataSeed
{
    public class PublishersSeed
    {
        public static async Task Seed(OnlineBookShopDbContext context)
        {
            if (!context.Publishers.Any())
            {
                var manning = new Publisher()
                {
                    Name = "Manning Publications"
                };

                var microsoftPress = new Publisher()
                {
                    Name = "Microsoft Press"
                };

                var apress = new Publisher()
                {
                    Name = "Apress"
                };

                var oReillyMedia = new Publisher()
                {
                    Name = "O'Reilly Media"
                };

                var packtPublishing = new Publisher()
                {
                    Name = "Packt Publishing"
                };

                context.Publishers.Add(manning);
                context.Publishers.Add(microsoftPress);
                context.Publishers.Add(apress);
                context.Publishers.Add(oReillyMedia);
                context.Publishers.Add(packtPublishing);

                await context.SaveChangesAsync();
            }
        }
    }
}
