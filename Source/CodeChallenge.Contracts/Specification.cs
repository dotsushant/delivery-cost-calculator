using System;

namespace PageUp.CodeChallenge.Contracts
{
    /// <summary>
    /// Contract to lay out the specification for a given subject
    /// </summary>
    /// <typeparam name="T">subject for which the specification to be satisfied</typeparam>
    public interface ISpecification<T>
    {
        /// <summary>
        /// Indicates whether the given type meets the specification
        /// </summary>
        /// <param name="subject">Subject under consideration</param>
        /// <returns>True if the subject meets the specification</returns>
        bool IsSatisfiedBy(T subject);
    }
}