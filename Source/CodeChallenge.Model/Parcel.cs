using System;
using PageUp.CodeChallenge.Helpers;

namespace PageUp.CodeChallenge.Model
{
    /// <summary>
    /// Parcel (package), sent through the mail or package delivery 
    /// </summary>
    public class Parcel
    {
        /// <summary>
        /// Weight
        /// </summary>
        private readonly Weight _weight;

        /// <summary>
        /// Volume
        /// </summary>
        private readonly Volume _volume;

        /// <summary>
        /// Initilization
        /// </summary>
        /// <param name="weight">Weight</param>
        /// <param name="volume">Volume</param>
        public Parcel(Weight weight, Volume volume)
        {
            var weightContractMessage = Contract.Combine(nameof(Parcel), nameof(weight));
            Contract.Requires<ArgumentNullException>(weight != null, HelperResource.NullArgumentIndication, weightContractMessage);

            var volumeContractMessage = Contract.Combine(nameof(Parcel), nameof(volume));
            Contract.Requires<ArgumentNullException>(volume != null, HelperResource.NullArgumentIndication, volumeContractMessage);

            Contract.Requires<ArgumentException>(weight.Value > 0, HelperResource.InvalidArgumentIndication, weightContractMessage);
            Contract.Requires<ArgumentException>(volume.Value > 0, HelperResource.InvalidArgumentIndication, volumeContractMessage);

            _weight = weight;
            _volume = volume;
        }

        /// <summary>
        /// Weight
        /// </summary>
        public Weight Weight
        {
            get
            {
                return _weight;
            }
        }

        /// <summary>
        /// Volume
        /// </summary>
        public Volume Volume
        {
            get
            {
                return _volume;
            }
        }

        /// <summary>
        /// Description
        /// </summary>
        /// <returns>Description</returns>
        public override string ToString()
        {
            return string.Format(ModelResource.ParcelDescriptionFormat, _weight, _volume);
        }
    }

    /// <summary>
    /// Categorization of the parcel
    /// </summary>
    public enum ParcelCategory
    {
        /// <summary>
        /// Weight exceeds 50 KG
        /// </summary>
        RejectedParcel,

        /// <summary>
        /// Weight exceeds 10 KG
        /// </summary>
        HeavyParcel,

        /// <summary>
        /// Volume is less than 1500 CUBIC CM
        /// </summary>
        SmallParcel,

        /// <summary>
        /// Volume is less than 2500 CUBIC CM
        /// </summary>
        MediumParcel,

        /// <summary>
        /// All other type which does not meet other types
        /// </summary>
        LargeParcel
    }

    /// <summary>
    /// Tag is generated when the parcel is validated
    /// </summary>
    public class ParcelTag
    {
        /// <summary>
        /// Cost of delivery for the parcel
        /// </summary>
        private readonly Money _deliveryCost;

        /// <summary>
        /// Category of parcel
        /// </summary>
        private readonly ParcelCategory _category;

        /// <summary>
        /// Initilization
        /// </summary>
        /// <param name="category">Category of parcel</param>
        /// <param name="deliveryCost">Cost of delivery for the parcel</param>
        public ParcelTag(ParcelCategory category, Money deliveryCost)
        {
            var contractMessage = Contract.Combine(nameof(ParcelTag), nameof(deliveryCost));
            Contract.Requires<ArgumentNullException>(deliveryCost != null, HelperResource.NullArgumentIndication, contractMessage);

            _category = category;
            _deliveryCost = deliveryCost;
        }

        /// <summary>
        /// Cost of delivery for the parcel
        /// </summary>
        public Money DeliveryCost
        {
            get
            {
                return _deliveryCost;
            }
        }

        /// <summary>
        /// Category of parcel
        /// </summary>
        public ParcelCategory Category
        {
            get
            {
                return _category;
            }
        }

        /// <summary>
        /// Description
        /// </summary>
        /// <returns>Description formatted as per current culture</returns>
        public override string ToString()
        {
            return string.Format(ModelResource.ParcelTagDescriptionFormat, ModelResource.ResourceManager.GetString(Category.ToString()), DeliveryCost);
        }
    }
}