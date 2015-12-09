using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace IQueryableTask.Provider
{
    internal class LinqToYahooVisitor : ExpressionVisitor
    {
        StringBuilder resultString;

        public string Translate(Expression exp)
        {
            resultString = new StringBuilder();
            Visit(exp);
            return resultString.ToString();
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            //todo: extend method to work not only with 'where'
            if (node.Method.DeclaringType == typeof(Queryable)
                && node.Method.Name == "Where")
            {
                Visit(node.Arguments[1]);

                return node;
            }
            else
            {
                //todo: investigate how to throw exceptions correctly
                throw new NotSupportedException(node.Method.Name);
            }
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.Equal:
                    Visit(node.Left);
                    resultString.Append(" = ");
                    Visit(node.Right);
                    break;
                case ExpressionType.AndAlso:
                    Visit(node.Left);
                    resultString.Append(" and ");
                    Visit(node.Right);
                    break;
                case ExpressionType.OrElse:
                    Visit(node.Left);
                    resultString.Append(" or ");
                    Visit(node.Right);
                    break;
                default:
                    throw new NotSupportedException(string.Format("Operation {0} is not supported", node.NodeType));
            };

            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            resultString.Append(node.Member.Name);

            return base.VisitMember(node);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            resultString.Append("\"").Append(node.Value).Append("\"");

            return node;
        }
    }
}
