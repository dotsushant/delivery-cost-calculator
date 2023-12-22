using System;
using PageUp.CodeChallenge.Helpers;

namespace PageUp.CodeChallenge.Model
{
    /// <summary>
    /// Represents amount
    /// </summary>
    public class Money
    {
        /// <summary>
        /// Value
        /// </summary>
        private readonly decimal _value;

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="value">Value</param>
        public Money(decimal value)
        {
            var contractMessage = Contract.Combine(nameof(Money), nameof(value));
            Contract.Requires<ArgumentException>(value >= 0, HelperResource.NegativeArgumentIndication, contractMessage);

            _value = value;
        }

        /// <summary>
        /// Value
        /// </summary>
        public decimal Value
        {
            get
            {
                return _value;
            }
        }

        /// <summary>
        /// Description
        /// <remarks>Formatted to suit current culture e.g. $123.98</remarks>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0:C}", Value);
        }
    }

    /// <summary>
    /// Represents invalid amount
    /// </summary>
    public class NullMoney : Money
    {
        /// <summary>
        /// Single instance with lazy initialization
        /// </summary>
        private static readonly Lazy<NullMoney> _instance = new Lazy<NullMoney>(() => new NullMoney());

        /// <summary>
        /// Single instance
        /// </summary>
        public static NullMoney Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        /// <summary>
        /// Private initialization
        /// </summary>
        private NullMoney()
            : base(0m)
        {

        }

        /// <summary>
        /// Description
        /// </summary>
        /// <returns>Description</returns>
        public override string ToString()
        {
            return HelperResource.NotApplicable;
        }
    }
}