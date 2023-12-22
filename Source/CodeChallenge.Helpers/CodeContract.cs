using System;

namespace PageUp.CodeChallenge.Helpers
{
    /// <summary>
    /// Alternative to .NET code contract
    /// </summary>
    public static class Contract
    {
        /// <summary>
        /// Gets the fully qualified path for a parameter
        /// </summary>
        /// <param name="parameters">Path fragments</param>
        /// <returns>Fully qualified path</returns>
        public static string Combine(params object[] parameters)
        {
            return string.Join("::", parameters);
        }

        /// <summary>
        /// Precondition
        /// </summary>
        /// <typeparam name="TException">Exception to be thrown if pre-condition is not met</typeparam>
        /// <param name="condition">Precondition</param>
        /// <param name="message">Message associated with the exception</param>
        public static void Requires<TException>(bool condition, string message) where TException : Exception
        {
            Requires<TException>(condition, "{0}", message);
        }

        /// <summary>
        /// Precondition
        /// </summary>
        /// <typeparam name="TException">Exception to be thrown if pre-condition is not met</typeparam>
        /// <param name="condition">Precondition</param>
        /// <param name="messageFormat">Message format</param>
        /// <param name="messageParameters">Message Parameters</param>
        public static void Requires<TException>(bool condition, string messageFormat, params object[] messageParameters) where TException : Exception
        {
            if (!condition)
            {
                throw Activator.CreateInstance(typeof(TException), string.Format(messageFormat, messageParameters)) as TException;
            }
        }
    }
}