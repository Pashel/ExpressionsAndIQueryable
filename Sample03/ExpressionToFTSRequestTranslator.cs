using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sample03
{
	public class ExpressionToFTSRequestTranslator : ExpressionVisitor
	{
        List<string> queryList = new List<string>();
        StringBuilder resultString = new StringBuilder();

		public List<string> Translate(Expression exp)
		{
			Visit(exp);
			return queryList;
		}

	    private void VisitSubNodes(Expression first, Expression secod, string before = "", string after = "")
	    {
            Visit(first);

            resultString.Append("(");
            resultString.Append(before);

            Visit(secod);

            resultString.Append(after);
            resultString.Append(")");

            // add formed query to collection
            queryList.Add(resultString.ToString());
	        resultString.Clear();
	    }

		protected override Expression VisitMethodCall(MethodCallExpression node)
		{
		    Expression predicate;
            switch (node.Method.Name)
            {
                case "Where":
                    if (node.Method.DeclaringType != typeof(Queryable))
                        break;

                    predicate = node.Arguments[0];
                    if (predicate.NodeType == ExpressionType.Call)
                    {
                        // if left expression is not constant visit it (for expressions combining)
                        Visit(predicate);
                    }
                    predicate = node.Arguments[1];
                    Visit(predicate);
                    return node;

                case "Select":
                    if (node.Method.DeclaringType != typeof(Queryable))
                        break;
                    // example of not supported operation
                    throw new NotSupportedException(string.Format("Operation Select is not supported", node.NodeType));

                case "StartsWith":
                    VisitSubNodes(node.Object, node.Arguments[0], after: "*");
                    return node;

                case "EndsWith":
                    VisitSubNodes(node.Object, node.Arguments[0], "*");
                    return node;

                case "Contains":
                    VisitSubNodes(node.Object, node.Arguments[0], "*", "*");
                    return node;
            }

            return base.VisitMethodCall(node);
		}

		protected override Expression VisitBinary(BinaryExpression node)
		{
			switch (node.NodeType)
			{
				case ExpressionType.Equal:

			        Expression visitFirst;
			        Expression visitSecond;

			        if (node.Left.NodeType == ExpressionType.MemberAccess && node.Right.NodeType == ExpressionType.Constant)
			        {
			            visitFirst = node.Left;
			            visitSecond = node.Right;
			        }
			        else if(node.Left.NodeType == ExpressionType.Constant && node.Right.NodeType == ExpressionType.MemberAccess)
			        {
                        visitFirst = node.Right;
                        visitSecond = node.Left;
                    }
			        else
                        throw new NotSupportedException(string.Format("One operand shold be property or field amother constant", node.NodeType));

			        VisitSubNodes(visitFirst, visitSecond);
                    break;

				default:
					throw new NotSupportedException(string.Format("Operation {0} is not supported", node.NodeType));
			}

			return node;
		}

		protected override Expression VisitMember(MemberExpression node)
		{
			resultString.Append(node.Member.Name).Append(":");

			return base.VisitMember(node);
		}

		protected override Expression VisitConstant(ConstantExpression node)
		{
			resultString.Append(node.Value);

			return node;
		}
	}
}
