using System;
using PageUp.CodeChallenge.Helpers;

namespace PageUp.CodeChallenge.Model
{
    /// <summary>
    /// Describes the lower and upper bound for the given measurable
    /// </summary>
    public enum MeasurableBoundType
    {
        Inclusive,  /* Includes */
        Exclusive   /* Excludes */
    }

    /// <summary>
    /// Range of measurable
    /// </summary>
    /// <typeparam name="T">Measurable type</typeparam>
    public class MeasurableBound
    {
        /// <summary>
        /// Lower bound
        /// </summary>
        private readonly Measurable _lowerBound;

        /// <summary>
        /// Upper bound
        /// </summary>
        private readonly Measurable _upperBound;

        /// <summary>
        /// Lower bound type
        /// </summary>
        private readonly MeasurableBoundType _lowerBoundType;

        /// <summary>
        /// Upper bound type
        /// </summary>
        private readonly MeasurableBoundType _upperBoundType;

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="lowerBound">Lower bound</param>
        /// <param name="upperBound"> Upper bound</param>
        /// <param name="lowerBoundType">Lower bound type</param>
        /// <param name="upperBoundType">Upper bound type</param>
        public MeasurableBound(Measurable lowerBound, Measurable upperBound, MeasurableBoundType lowerBoundType, MeasurableBoundType upperBoundType)
        {
            var lowerBoundContractMessage = Contract.Combine(nameof(MeasurableBound), nameof(lowerBound));
            var upperBoundContractMessage = Contract.Combine(nameof(MeasurableBound), nameof(upperBound));

            Contract.Requires<ArgumentNullException>(lowerBound != null, HelperResource.NullArgumentIndication, lowerBoundContractMessage);
            Contract.Requires<ArgumentNullException>(upperBound != null, HelperResource.NullArgumentIndication, upperBoundContractMessage);
            Contract.Requires<ArgumentException>(lowerBound.Unit == upperBound.Unit, HelperResource.IncompatibleArgumentIndication, lowerBoundContractMessage, upperBoundContractMessage);
            Contract.Requires<ArgumentException>(lowerBound.CompareTo(upperBound) <= 0, HelperResource.InvalidArgumentIndication, string.Format("{0} or {1}", lowerBoundContractMessage, upperBoundContractMessage));

            _lowerBound = lowerBound;
            _upperBound = upperBound;

            _lowerBoundType = lowerBoundType;
            _upperBoundType = upperBoundType;
        }

        /// <summary>
        /// Lower bound
        /// </summary>
        public Measurable LowerBound
        {
            get
            {
                return _lowerBound;
            }
        }

        /// <summary>
        /// Upper bound
        /// </summary>
        public Measurable UpperBound
        {
            get
            {
                return _upperBound;
            }
        }

        /// <summary>
        /// Lower bound type
        /// </summary>
        public MeasurableBoundType LowerBoundType
        {
            get
            {
                return _lowerBoundType;
            }
        }

        /// <summary>
        /// Upper bound type
        /// </summary>
        public MeasurableBoundType UpperBoundType
        {
            get
            {
                return _upperBoundType;
            }
        }

        /// <summary>
        /// Checks if the value falls within the bounds
        /// </summary>
        /// <param name="value">Value to be checked</param>
        /// <returns>True, if the value falls within the bounds</returns>
        public bool Contains(Measurable value)
        {
            var lowerBoundContractMessage = Contract.Combine(nameof(LowerBound));
            var valueContractMessage = Contract.Combine(nameof(Contains), nameof(value));

            Contract.Requires<ArgumentNullException>(value != null, HelperResource.NullArgumentIndication, valueContractMessage);
            Contract.Requires<ArgumentException>(LowerBound.Unit == value.Unit, HelperResource.IncompatibleArgumentIndication, lowerBoundContractMessage, valueContractMessage);

            var lowerBoundDistance = _lowerBound.CompareTo(value);
            var upperBoundDistance = _upperBound.CompareTo(value);

            var lower = _lowerBoundType == MeasurableBoundType.Exclusive ? _lowerBound.CompareTo(value) < 0 : _lowerBound.CompareTo(value) <= 0;
            var upper = _upperBoundType == MeasurableBoundType.Exclusive ? _upperBound.CompareTo(value) > 0 : _upperBound.CompareTo(value) >= 0;

            return lower && upper;
        }
    }
}