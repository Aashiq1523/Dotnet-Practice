using Newtonsoft.Json;
using Sample.Util;
using System;
using System.Collections.Generic;

namespace Sample.Model
{
    public class PersonVM
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("address")]
        public AddressVM address { get; set; }

        public void NullOrEmptyValidation()
        {
            IList<AppError> errors = new List<AppError>();
            if (String.IsNullOrEmpty(name))
            {
                AppError.NAME_NOT_PROVIDED.message = "Field name is required. Please provide a value for field name";
                errors.Add(AppError.NAME_NOT_PROVIDED);
            }
            address.NullOrEmptyValidation(errors);

            if (errors.Count != 0)
                throw new AppException(errors);
        }

    }
}
