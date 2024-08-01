using Microsoft.EntityFrameworkCore;
using OnlineBookShop.Infrastructure.Persistance.Contexts;
using System.Data;

namespace OnlineBookShop.API.Middlewares
{
    public class DbTransactionMiddleware
    {
        private readonly RequestDelegate _next;

        public DbTransactionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, OnlineBookShopDbContext dbContext)
        {
            //different isolation levels for reads and writes
            // to improve performance and limit table locking we assume that all GET methods can run on uncommited level, 
            // which means that dirty reads are possible
            var isolationLevel = IsolationLevel.Unspecified;
            if (httpContext.Request.Method == HttpMethod.Get.Method)
                isolationLevel = IsolationLevel.ReadUncommitted;
            else
                isolationLevel = IsolationLevel.ReadCommitted;


            using (var transaction = await dbContext.Database.BeginTransactionAsync(isolationLevel))
            {
                await _next(httpContext);

                //Commit transaction if all commands succeed, transaction will auto-rollback when disposed if either commands fails
                await dbContext.Database.CommitTransactionAsync();
            }

            //using (var transaction = CreateTransactionScope())
            //{
            //    await _next(httpContext);
            //    transaction.Complete();
            //}
        }

        //private static TransactionScope CreateTransactionScope()
        //{
        //    const int _transactionTimeout = 240;

        //    var options = new TransactionOptions
        //    {
        //        IsolationLevel = IsolationLevel.ReadCommitted,
        //        Timeout = TimeSpan.FromSeconds(_transactionTimeout)
        //    };

        //    return new TransactionScope(TransactionScopeOption.Required, options, TransactionScopeAsyncFlowOption.Enabled);
        //}


    }
}
