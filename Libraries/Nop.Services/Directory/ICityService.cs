using System.Collections.Generic;
using Nop.Core.Domain.Directory;
using System.Web.Mvc;

namespace Nop.Services.Directory
{
    /// <summary>
    /// City service interface
    /// </summary>
    public partial interface ICityService
    {

        /// <summary>
        /// Gets a city
        /// </summary>
        /// <param name="cityId">The city identifier</param>
        /// <returns>City</returns>
        City GetCityById(int cityId);
        
        /// <summary>
        /// Gets a city collection by state/province identifier
        /// </summary>
        /// <param name="stateProvinceId">stateProvince identifier</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>City collection</returns>
        IList<City> GetCitiesByStateProvinceId(int stateProvinceId, bool showHidden = false);

        /// <summary>
        /// Gets all cities
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>City collection</returns>
        IList<City> GetCities(bool showHidden = false);

        IList<SelectListItem> GetCitiesForCombo();
    }
}
