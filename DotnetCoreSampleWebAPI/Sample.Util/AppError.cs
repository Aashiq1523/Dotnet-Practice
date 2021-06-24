using System.Text.Json.Serialization;

namespace Sample.Util
{
    public class AppError
    {
        public static readonly AppError UNKNOWN_ERROR = new AppError("UNKNOWN_ERROR", 500);
        public static readonly AppError BAD_REQUEST = new AppError("BAD_REQUEST", 400);
        public static readonly AppError INTERNAL_SERVER_ERROR = new AppError("INTERNAL_SERVER_ERROR", 500);
        public static readonly AppError FILE_UPLOAD_NOT_SUCCESS = new AppError("FILE_UPLOAD_NOT_SUCCESS", 400);
        public static readonly AppError FILE_FORMAT_NOT_SUPPORTED = new AppError("FILE_FORMAT_NOT_SUPPORTED", 400);
        public static readonly AppError FILE_NOT_PROVIDED = new AppError("FILE_NOT_PROVIDED", 400);
        public static readonly AppError ID_CANNOT_BE_EMPTY = new AppError("ID_CANNOT_BE_EMPTY", 400);
        public static readonly AppError NAME_NOT_PROVIDED = new AppError("NAME_NOT_PROVIDED", 400);
        public static readonly AppError STREET_NOT_PROVIDED = new AppError("STREET_NOT_PROVIDED", 400);
        public static readonly AppError CITY_NOT_PROVIDED = new AppError("CITY_NOT_PROVIDED", 400);
        public static readonly AppError POSTAL_CODE_NOT_PROVIDED = new AppError("POSTAL_CODE_NOT_PROVIDED", 400);
        public static readonly AppError MULTIPLE_PERSON_CREATE_FAILED = new AppError("MULTIPLE_PERSON_CREATE_FAILED", 400);
        public static readonly AppError PERSONS_NOT_FOUND = new AppError("PERSONS_NOT_FOUND", 400);
        public static readonly AppError PERSON_CREATE_FAILED = new AppError("PERSON_CREATE_FAILED", 400);
        public static readonly AppError PERSON_READ_FAILED = new AppError("PERSON_READ_FAILED", 400);
        public static readonly AppError PERSON_READ_ALL_FAILED = new AppError("PERSON_READ_ALL_FAILED", 400);
        public static readonly AppError PERSON_UPDATE_FAILED = new AppError("PERSON_UPDATE_FAILED", 400);
        public static readonly AppError PERSON_DELETE_FAILED = new AppError("PERSON_DELETE_FAILED", 400);

        protected AppError() { }

        public string key { get; set; }
        [JsonIgnore]
        public int code { get; set; }
        public string message { get; set; }

        private AppError(string key, int code)
        {
            this.key = key;
            this.code = code;
        }
    }
}
