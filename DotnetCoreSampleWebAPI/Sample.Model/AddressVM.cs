using Newtonsoft.Json;
using Sample.Util;
using System;
using System.Collections.Generic;

namespace Sample.Model
{
    public class AddressVM
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("street")]
        public string street { get; set; }
        [JsonProperty("city")]
        public string city { get; set; }
        [JsonProperty("postal_code")]
        public int postal_code { get; set; }

        public void NullOrEmptyValidation(IList<AppError> errors)
        {
            if (String.IsNullOrEmpty(street))
            {
                AppError.STREET_NOT_PROVIDED.message = "Field street is required. Please provide a value for field street";
                errors.Add(AppError.STREET_NOT_PROVIDED);
            }

            if (String.IsNullOrEmpty(city))
            {
                AppError.CITY_NOT_PROVIDED.message = "Field city is required. Please provide a value for field city";
                errors.Add(AppError.CITY_NOT_PROVIDED);
            }

            if (postal_code == 0)
            {
                AppError.POSTAL_CODE_NOT_PROVIDED.message = "Field postal_code is required. Please provide a value for field postal_code";
                errors.Add(AppError.POSTAL_CODE_NOT_PROVIDED);
            }
        }
    }
}
