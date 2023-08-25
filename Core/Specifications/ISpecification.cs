using System.Linq.Expressions;

namespace Core.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; } //where criteria ex: id of 1 .where
        List<Expression<Func<T, object>>> Includes { get; } //includes, used for accessors .includes
    }
}