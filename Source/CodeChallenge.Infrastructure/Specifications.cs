using System;
using PageUp.CodeChallenge.Model;
using PageUp.CodeChallenge.Helpers;
using PageUp.CodeChallenge.Contracts;

namespace PageUp.CodeChallenge.Infrastructure
{
    /// <summary>
    /// Base specification for handling parcel validation
    /// </summary>
    public abstract class ParcelSpecificationBase : ISpecification<Parcel>
    {
        /// <summary>
        /// Constraint on parcel w.r.t Weight or Volume
        /// </summary>
        private readonly MeasurableBound _constraint;

        /// <summary>
        /// Category of parcel associated with the current specification
        /// </summary>
        private readonly ParcelCategory _category;

        /// <summary>
        /// Cost multipler for calculating the cost of delivery of the parcel
        /// </summary>
        private readonly Money _deliveryCostFactor;

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="constraint">Constraint on parcel w.r.t Weight or Volume</param>
        /// <param name="category">Category of parcel associated with the current specification</param>
        /// <param name="deliveryCostFactor">Cost multipler for calculating the cost of delivery of the parcel</param>
        public ParcelSpecificationBase(MeasurableBound constraint, ParcelCategory category, Money deliveryCostFactor)
        {
            var contractMessage = Contract.Combine(nameof(ParcelSpecificationBase), nameof(deliveryCostFactor));
            Contract.Requires<ArgumentNullException>(deliveryCostFactor != null, HelperResource.NullArgumentIndication, contractMessage);

            _constraint = constraint;
            _category = category;
            _deliveryCostFactor = deliveryCostFactor;
        }

        /// <summary>
        /// Constraint on parcel w.r.t Weight or Volume
        /// </summary>
        public MeasurableBound Constraint
        {
            get
            {
                return _constraint;
            }
        }

        /// <summary>
        /// Category of parcel associated with the current specification
        /// </summary>
        public ParcelCategory Category
        {
            get
            {
                return _category;
            }
        }

        /// <summary>
        /// Cost multipler for calculating the cost of delivery of the parcel
        /// </summary>
        public Money DeliveryCostFactor
        {
            get
            {
                return _deliveryCostFactor;
            }
        }

        /// <summary>
        /// Indicates whether the parcel meets the specification
        /// </summary>
        /// <param name="parcel">Parcel under consideration</param>
        /// <returns>True if the parcel meets the specification</returns>
        public bool IsSatisfiedBy(Parcel parcel)
        {
            var contractMessage = Contract.Combine(nameof(IsSatisfiedBy), nameof(parcel));
            Contract.Requires<ArgumentNullException>(parcel != null, HelperResource.NullArgumentIndication, contractMessage);

            return InternalIsSatisfiedBy(parcel);
        }

        /// <summary>
        /// Generates the tag for a given parcel
        /// </summary>
        /// <param name="parcel">Parcel under consideration</param>
        /// <returns>Tag generated for the parcel</returns>
        public ParcelTag GenerateTag(Parcel parcel)
        {
            var contractMessage = Contract.Combine(nameof(GenerateTag), nameof(parcel));
            Contract.Requires<ArgumentNullException>(parcel != null, HelperResource.NullArgumentIndication, contractMessage);

            return InternalGenerateTag(parcel);
        }

        /// <summary>
        /// Indicates whether the parcel meets the specification
        /// </summary>
        /// <param name="parcel">Parcel under consideration</param>
        /// <returns>True if the parcel meets the specification</returns>
        protected abstract bool InternalIsSatisfiedBy(Parcel parcel);

        /// <summary>
        /// Generates the tag for a given parcel
        /// </summary>
        /// <param name="parcel">Parcel under consideration</param>
        /// <returns>Tag generated for the parcel</returns>
        protected abstract ParcelTag InternalGenerateTag(Parcel parcel);
    }

    /// <summary>
    /// Specification for parcel weight
    /// </summary>
    public class ParcelWeightSpecification : ParcelSpecificationBase
    {
        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="constraint">Constraint on measurable</param>
        /// <param name="category">Parcel category if the specification is met</param>
        /// <param name="deliveryCostFactor">Cost multipler for calculating the cost of delivery of the parcel</param>
        public ParcelWeightSpecification(MeasurableBound constraint, ParcelCategory category, Money deliveryCostFactor)
            : base(constraint, category, deliveryCostFactor)
        {
        }

        /// <summary>
        /// Indicates whether the parcel meets the specification
        /// </summary>
        /// <param name="parcel">Parcel under consideration</param>
        /// <returns>True if the parcel meets the specification</returns>
        protected override bool InternalIsSatisfiedBy(Parcel parcel)
        {
            return Constraint.Contains(parcel.Weight);
        }

        /// <summary>
        /// Generates the tag for a given parcel
        /// </summary>
        /// <param name="parcel">Parcel under consideration</param>
        /// <returns>Tag generated for the parcel</returns>
        protected override ParcelTag InternalGenerateTag(Parcel parcel)
        {
            var deliveryCost = Category == ParcelCategory.RejectedParcel ? NullMoney.Instance : new Money((decimal)parcel.Weight.Value * DeliveryCostFactor.Value);
            return new ParcelTag(Category, deliveryCost);
        }
    }

    /// <summary>
    /// Specification for parcel volume
    /// </summary>
    public class ParcelVolumeSpecification : ParcelSpecificationBase
    {
        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="constraint">Constraint on measurable</param>
        /// <param name="category">Parcel category if the specification is met</param>
        /// <param name="deliveryCostFactor">Cost multipler for calculating the cost of delivery of the parcel</param>
        public ParcelVolumeSpecification(MeasurableBound constraint, ParcelCategory category, Money deliveryCostFactor)
            : base(constraint, category, deliveryCostFactor)
        {
        }

        /// <summary>
        /// Indicates whether the parcel meets the specification
        /// </summary>
        /// <param name="parcel">Parcel under consideration</param>
        /// <returns>True if the parcel meets the specification</returns>
        protected override bool InternalIsSatisfiedBy(Parcel parcel)
        {
            return (null == Constraint) ? true : Constraint.Contains(parcel.Volume);
        }

        /// <summary>
        /// Generates the tag for a given parcel
        /// </summary>
        /// <param name="parcel">Parcel under consideration</param>
        /// <returns>Tag generated for the parcel</returns>
        protected override ParcelTag InternalGenerateTag(Parcel parcel)
        {
            var deliveryCost = new Money((decimal)parcel.Volume.Value * DeliveryCostFactor.Value);
            return new ParcelTag(Category, deliveryCost);
        }
    }
}