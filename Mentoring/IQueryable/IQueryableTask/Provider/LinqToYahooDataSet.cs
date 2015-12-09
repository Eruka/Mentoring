using IQueryableTask.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace IQueryableTask.Provider
{
    public class LinqToYahooDataSet<T> : IQueryable<T> 
    {
        LinqToYahooClient Client { get; set; }
        public LinqToYahooDataSet()
        {
            LinqToYahooClient client = new LinqToYahooClient();
            Provider = new LinqToYahooProvider(client);
            Expression = Expression.Constant(this);
        }

        public LinqToYahooDataSet(LinqToYahooProvider provider, Expression expression)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }

            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            Provider = provider;
            Expression = expression;
        }

        public Type ElementType
        {
            get
            {
                return typeof(T);
            }
        }

        public Expression Expression { get; set; }

        public IQueryProvider Provider { get; set; }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return Provider.Execute<IEnumerable<T>>(Expression).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Provider.Execute<IEnumerable>(Expression).GetEnumerator();
        }
    }
}
