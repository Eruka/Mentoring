using IQueryableTask.Client;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace IQueryableTask.Provider
{
    public class LinqToYahooProvider : IQueryProvider
    {
        LinqToYahooClient Client { get; set; }

        public LinqToYahooProvider()
        {
            Client = new LinqToYahooClient();
        }

        public LinqToYahooProvider(LinqToYahooClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException("client");
            }
            Client = client;
        }
        public IQueryable CreateQuery(Expression expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new LinqToYahooDataSet<TElement>(this, expression);
        }

        public object Execute(Expression expression)
        {
            throw new NotImplementedException();
        }

        public TResult Execute<TResult>(Expression expression)
        {
            //todo: generic type
            //var type = expression.Type.GenericTypeArguments[0];
            var translator = new LinqToYahooVisitor();
            var queryString = translator.Translate(expression);
            Console.WriteLine("Where " + queryString);
            return (TResult)(Client.Search<Quote>(queryString));
        }
    }
}
