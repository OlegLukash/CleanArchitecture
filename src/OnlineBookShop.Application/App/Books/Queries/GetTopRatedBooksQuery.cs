using Dapper;
using MediatR;
using OnlineBookShop.Application.App.Books.Responses;
using OnlineBookShop.Application.Common.Interfaces;

namespace OnlineBookShop.Application.App.Books.Queries
{
    public class GetTopRatedBooksQuery: IRequest<IEnumerable<TopRatedBookDto>>
    {

    }

    public class GetTopRatedBooksQueryHandler : IRequestHandler<GetTopRatedBooksQuery, IEnumerable<TopRatedBookDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetTopRatedBooksQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<IEnumerable<TopRatedBookDto>> Handle(GetTopRatedBooksQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT " +
                                "[vBooks].[Id]," +
                                "[vBooks].[Title]," +
                                "[vBooks].[Description]," +
                                "[vBooks].[PublishedOn]," +
                                "[vBooks].[Price]," +
                                "[vBooks].[PublisherId]" +
                                "FROM [dbo].[vBooks]" +
                                "WHERE [vBooks].[NumStars] > 3";
            
            //Here a view / materialized view (indexed view) might be created

            var topRatedBooksDto = await connection.QueryAsync<TopRatedBookDto>(sql);

            return topRatedBooksDto;
        }
    }
}
