using System;
using PageUp.CodeChallenge.Helpers;

namespace PageUp.CodeChallenge.Model
{
    /// <summary>
    /// Unit of measurement
    /// <remarks>Currently only basic units are supported</remarks>
    /// </summary>
    public enum MeasurementUnit
    {
        KG          /* Weight */,
        CM          /* Dimension */,
        CUBIC_CM    /* Volume */
    }

    /// <summary>
    /// Basic measurable
    /// </summary>
    public class Measurable : IComparable<Measurable>, IEquatable<Measurable>
    {
        /// <summary>
        /// Value of measurable
        /// </summary>
        private readonly double _value;

        /// <summary>
        /// Unit of measurement
        /// </summary>
        private readonly MeasurementUnit _unit;

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="unit">Unit of measurement</param>
        /// <param name="value">Initial value</param>
        public Measurable(MeasurementUnit unit, double value)
        {
            var contractMessage = Contract.Combine(nameof(Measurable), nameof(value));
            Contract.Requires<ArgumentException>(value >= 0, HelperResource.NegativeArgumentIndication, contractMessage);

            _unit = unit;
            _value = value;
        }

        /// <summary>
        /// Unit of measurement
        /// </summary>
        public MeasurementUnit Unit
        {
            get
            {
                return _unit;
            }
        }

        /// <summary>
        /// Value of measurable
        /// </summary>
        public double Value
        {
            get
            {
                return _value;
            }
        }

        /// <summary>
        /// Comparer
        /// </summary>
        /// <param name="measurable">The measurable with which to compare to</param>
        /// <returns>-1 = Less, 0 = Equal, 1 = Greater</returns>
        public int CompareTo(Measurable measurable)
        {
            var contractMessage = Contract.Combine(nameof(CompareTo), nameof(measurable));
            Contract.Requires<ArgumentNullException>(measurable != null, HelperResource.NullArgumentIndication, contractMessage);

            return this.Value.CompareTo(measurable.Value);
        }

        /// <summary>
        /// Equality check
        /// </summary>
        /// <param name="other">The measurable with which to compare to</param>
        /// <returns>True if the value matches</returns>
        public bool Equals(Measurable other)
        {
            return 0 == this.CompareTo(other);
        }

        /// <summary>
        /// Description of measurable
        /// </summary>
        /// <returns>Description of measurable</returns>
        public override string ToString()
        {
            return string.Format("{0} {1}", Value, Unit);
        }
    }

    /// <summary>
    /// A measurable which denotes weight
    /// <remarks>This is a specialized class to provide better semantic</remarks>
    /// </summary>
    public class Weight : Measurable
    {
        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="value">Initial value</param>
        public Weight(double value)
            : base(MeasurementUnit.KG, value)
        {
        }
    }

    /// <summary>
    /// A measurable extent of a particular kind, such as length, breadth, depth, or height.
    /// <remarks>This is a specialized class to provide better semantic</remarks>
    /// </summary>
    public class Dimension : Measurable
    {
        public Dimension(double value)
            : base(MeasurementUnit.CM, value)
        {
        }
    }

    /// <summary>
    /// A measurable extent of a particular kind, such as volume
    /// <remarks>This is a specialized class to provide better semantic</remarks>
    /// </summary>
    public class Volume : Measurable
    {
        /// <summary>
        /// Height
        /// </summary>
        private readonly Dimension _height;

        /// <summary>
        /// Width
        /// </summary>
        private readonly Dimension _width;

        /// <summary>
        /// Depth
        /// </summary>
        private readonly Dimension _depth;

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="height">Height</param>
        /// <param name="width">Width</param>
        /// <param name="depth">Depth</param>
        public Volume(double height, double width, double depth)
            : base(MeasurementUnit.CUBIC_CM, height * width * depth)
        {
            _height = new Dimension(height);
            _width = new Dimension(width);
            _depth = new Dimension(depth);
        }

        /// <summary>
        /// Height
        /// </summary>
        public Dimension Height
        {
            get
            {
                return _height;
            }
        }

        /// <summary>
        /// Width
        /// </summary>
        public Dimension Width
        {
            get
            {
                return _width;
            }
        }

        /// <summary>
        /// Depth
        /// </summary>
        public Dimension Depth
        {
            get
            {
                return _depth;
            }
        }

        /// <summary>
        /// Description
        /// </summary>
        /// <returns>Description</returns>
        public override string ToString()
        {
            return string.Format("{0} x {1} x {2}", Height, Width, Depth);
        }
    }
}