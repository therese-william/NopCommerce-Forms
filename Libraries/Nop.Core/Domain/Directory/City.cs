
using Nop.Core.Domain.Localization;

namespace Nop.Core.Domain.Directory
{
    /// <summary>
    /// Represents a city
    /// </summary>
    public partial class City : BaseEntity, ILocalizedEntity
    {
        /// <summary>
        /// Gets or sets the state identifier
        /// </summary>
        public int StateId { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// Gets or sets the post code
        /// </summary>
        public string PostCode { get; set; }

        /// <summary>
        /// Gets or sets the area
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// Gets or sets the latitude
        /// </summary>
        public double? Latitude { get; set; }

        /// <summary>
        /// Gets or sets the Longitude
        /// </summary>
        public double? Longitude { get; set; }

        /// <summary>
        /// Gets or sets the Zone
        /// </summary>
        public int? Zone { get; set; }

        /// <summary>
        /// Gets or sets the country
        /// </summary>
        public virtual StateProvince State { get; set; }
    }

}
