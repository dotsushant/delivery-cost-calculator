using System.Collections.Generic;

namespace PageUp.CodeChallenge.Contracts
{
    /// <summary>
    /// Contract for implementing validators
    /// </summary>
    /// <typeparam name="TInput">Entity to be validated</typeparam>
    /// <typeparam name="TResult">Validation result</typeparam>
    public interface IValidator<TInput, TResult>
    {
        /// <summary>
        /// Set of rules to be used during validation
        /// </summary>
        IEnumerable<ISpecification<TInput>> Rules
        {
            get;
        }

        /// <summary>
        /// Validated the entity using given set of rules
        /// </summary>
        /// <param name="entity">Entity under consideration</param>
        /// <returns>Validation result</returns>
        TResult Validate(TInput entity);
    }
}