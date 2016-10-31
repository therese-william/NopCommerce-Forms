using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Core.Domain.Directory;
using Nop.Services.Events;
using System.Web.Mvc;
using Nop.Core.Domain.Vendors;

namespace Nop.Services.Directory
{
    /// <summary>
    /// City service
    /// </summary>
    public partial class CityService : ICityService
    {
        #region Constants

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : stateprovince ID
        /// </remarks>
        private const string CITIES_ALL_KEY = "Nop.`city.all-{0}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string CITIES_PATTERN_KEY = "Nop.city.";

        #endregion

        #region Fields

        private readonly IRepository<City> _cityRepository;
        private readonly IRepository<StateProvince> _stateProvinceRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="cityRepository">City repository</param>
        /// <param name="eventPublisher">Event published</param>
        public CityService(ICacheManager cacheManager,
            IRepository<City> cityRepository,
            IRepository<StateProvince> stateProvinceRepository,
            IEventPublisher eventPublisher)
        {
            _cacheManager = cacheManager;
            _cityRepository = cityRepository;
            _stateProvinceRepository = stateProvinceRepository;
            _eventPublisher = eventPublisher;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a city
        /// </summary>
        /// <param name="cityId">The city identifier</param>
        /// <returns>City</returns>
        public virtual City GetCityById(int cityId)
        {
            if (cityId == 0)
                return null;

            return _cityRepository.GetById(cityId);
        }
        
        /// <summary>
        /// Gets a city collection by state/province identifier
        /// </summary>
        /// <param name="stateProvinceId">state/province identifier</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>City collection</returns>
        public virtual IList<City> GetCitiesByStateProvinceId(int stateProvinceId, bool showHidden = false)
        {
            string key = string.Format(CITIES_ALL_KEY, stateProvinceId);
            return _cacheManager.Get(key, () =>
            {
                var query = from c in _cityRepository.Table
                            orderby c.CityName
                            where c.StateId == stateProvinceId 
                            select c;
                var cities = query.ToList();
                return cities;
            });
        }

        /// <summary>
        /// Gets all cities
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>City collection</returns>
        public virtual IList<City> GetCities(bool showHidden = false)
        {
            var query = from c in _cityRepository.Table
                        orderby c.StateId, c.CityName
                select c;
            var cities = query.ToList();
            return cities;
        }

        public virtual IList<SelectListItem> GetCitiesForCombo()
        {
            var query = from c in _cityRepository.Table
                        join sp in _stateProvinceRepository.Table on c.StateId equals sp.Id
                        orderby c.StateId, c.CityName
                        select new SelectListItem { 
                            Value= c.Id.ToString(),
                            Text= c.CityName + ", " + sp.Abbreviation + " " + c.PostCode
                        };
            var cities = query.ToList();
            return cities;
        }
        #endregion
    }
}
