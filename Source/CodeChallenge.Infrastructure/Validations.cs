using System;
using System.Linq;
using System.Collections.Generic;
using PageUp.CodeChallenge.Model;
using PageUp.CodeChallenge.Helpers;
using PageUp.CodeChallenge.Contracts;

namespace PageUp.CodeChallenge.Infrastructure
{
    /// <summary>
    /// Validator for parcel
    /// </summary>
    public class ParcelValidator : IValidator<Parcel, ParcelTag>
    {
        /// <summary>
        /// List of specifications/rules to be met by the parcel
        /// </summary>
        private List<ISpecification<Parcel>> _rules = new List<ISpecification<Parcel>>();

        /// <summary>
        /// Initialization
        /// <remarks>TODO: This should be instrumented in XML file for better management</remarks>
        /// </summary>
        public ParcelValidator()
        {
            // Rejected
            // > 50 KG && <= MAX
            var rejectedParcelWeightConstraint = new MeasurableBound(new Weight(50), new Weight(double.MaxValue), MeasurableBoundType.Exclusive, MeasurableBoundType.Inclusive);
            var rejectedParcelWeightSpecification = new ParcelWeightSpecification(rejectedParcelWeightConstraint, ParcelCategory.RejectedParcel, NullMoney.Instance);
            _rules.Add(rejectedParcelWeightSpecification);

            // Heavy Parcel
            // > 10 KG && <= 50 KG
            var heavyParcelWeightConstraint = new MeasurableBound(new Weight(10), new Weight(50), MeasurableBoundType.Exclusive, MeasurableBoundType.Inclusive);
            var heavyParcelWeightSpecification = new ParcelWeightSpecification(heavyParcelWeightConstraint, ParcelCategory.HeavyParcel, new Money(15m));
            _rules.Add(heavyParcelWeightSpecification);

            // Small Parcel
            // > 0 && < 1500 CUBIC_CM
            var smallParcelVolumeConstraint = new MeasurableBound(new Volume(0, 0, 0), new Volume(1500, 1, 1), MeasurableBoundType.Exclusive, MeasurableBoundType.Exclusive);
            var smallParcelVolumeSpecification = new ParcelVolumeSpecification(smallParcelVolumeConstraint, ParcelCategory.SmallParcel, new Money(0.05m));
            _rules.Add(smallParcelVolumeSpecification);

            // Medium Parcel
            // >= 1500 && < 2500 CUBIC_CM
            var mediumParcelVolumeConstraint = new MeasurableBound(new Volume(1500, 1, 1), new Volume(2500, 1, 1), MeasurableBoundType.Inclusive, MeasurableBoundType.Exclusive);
            var mediumParcelVolumeSpecification = new ParcelVolumeSpecification(mediumParcelVolumeConstraint, ParcelCategory.MediumParcel, new Money(0.04m));
            _rules.Add(mediumParcelVolumeSpecification);

            var largeParcelVolumeSpecification = new ParcelVolumeSpecification(null, ParcelCategory.LargeParcel, new Money(0.03m));
            _rules.Add(largeParcelVolumeSpecification);
        }

        /// <summary>
        /// Set of rules to be used during validation
        /// </summary>
        public IEnumerable<ISpecification<Parcel>> Rules
        {
            get
            {
                return _rules;
            }
        }

        /// <summary>
        /// Validated the parcel using given set of rules
        /// </summary>
        /// <param name="entity">Parcel under consideration</param>
        /// <returns>Validation result</returns>
        public ParcelTag Validate(Parcel parcel)
        {
            var contractMessage = Contract.Combine(nameof(Validate), nameof(parcel));
            Contract.Requires<ArgumentNullException>(parcel != null, HelperResource.NullArgumentIndication, contractMessage);
            Contract.Requires<InvalidOperationException>(Rules.Count() > 0, InfrastructureResource.InsufficientRulesIndication);

            var matchingRule = _rules.FirstOrDefault(rule => rule.IsSatisfiedBy(parcel)) as ParcelSpecificationBase;
            return matchingRule.GenerateTag(parcel);
        }
    }
}