
using CommonLibrary;

namespace ECommerce.Api.Common
{
    public class AppSettings
    {
        #region Application
        public static string ApplicationSwagger
        {
            get
            {
                return MyConvert.ToString(Startup.Configuration["AppSettings:Application:Swagger"]);
            }
        }

        #region URL
        public static string API_URL
        {
            get
            {
                return MyConvert.ToString(Startup.Configuration["AppSettings:Application:URL:API_URL"]);
            }
        }
        public static string WebsiteUrl
        {
            get
            {
                return MyConvert.ToString(Startup.Configuration["AppSettings:Application:URL:WebsiteUrl"]);
            }
        }
        public static string AdminWebsiteUrl
        {
            get
            {
                return MyConvert.ToString(Startup.Configuration["AppSettings:Application:URL:AdminWebsiteUrl"]);
            }
        }
        #endregion

        #region Path
        public static string PathDocumentUpload
        {
            get
            {
                return MyConvert.ToString(Startup.Configuration["AppSettings:Application:Path:DocumentUpload"]);
            }
        }
        #endregion
        public static int LinkExpireDuration
        {
            get
            {
                return MyConvert.ToInt(Startup.Configuration["AppSettings:Application:LinkExpireDuration"]);
            }
        }
        public static int UserImageSize
        {
            get
            {
                return MyConvert.ToInt(Startup.Configuration["AppSettings:Application:UserImageSize"]);
            }
        }
        public static int UserThumbImageSize
        {
            get
            {
                return MyConvert.ToInt(Startup.Configuration["AppSettings:Application:UserThumbImageSize"]);
            }
        }
        public static int CategoryImageSize
        {
            get
            {
                return MyConvert.ToInt(Startup.Configuration["AppSettings:Application:CategoryImageSize"]);
            }
        }
        public static int ProductImageSize
        {
            get
            {
                return MyConvert.ToInt(Startup.Configuration["AppSettings:Application:ProductImageSize"]);
            }
        }
        public static int ProductThumbImageSize
        {
            get
            {
                return MyConvert.ToInt(Startup.Configuration["AppSettings:Application:ProductThumbImageSize"]);
            }
        }
        public static string VersionNo
        {
            get
            {
                return MyConvert.ToString(Startup.Configuration["AppSettings:VersionNo"]);
            }
        }
        public static DateTime UpdateDateTime
        {
            get
            {
                return MyConvert.ToDateTime(Startup.Configuration["AppSettings:UpdateDateTime"]);
            }
        }
        public static Double AppLastVersionNo
        {
            get
            {
                return MyConvert.ToDouble(Startup.Configuration["AppSettings:AppLastVersionNo"]);
            }
        }
        public static bool WebUnderMaintenance
        {
            get
            {
                return MyConvert.ToBoolean(Startup.Configuration["AppSettings:Application:WebUnderMaintenance"]);
            }
        }

        #endregion
        public static string EmailFrom
        {
            get
            {
                return MyConvert.ToString(Startup.Configuration["AppSettings:Email:From"]);
            }
        }

        public static string UploadedFilePath
        {
            get
            {
                return MyConvert.ToString(Startup.Configuration["UploadedFilePath"]);
            }
        }

        #region Security Token
        public static bool SecurityTokenEnabled
        {
            get
            {
                return MyConvert.ToBoolean(Startup.Configuration["AppSettings:SecurityToken:Enabled"]);
            }
        }

        /// <summary>
        /// Get SecurityToken key from configuration.
        /// </summary>
        public static string SecurityTokenKey
        {
            get
            {
                return MyConvert.ToString(Startup.Configuration["AppSettings:SecurityToken:Key"]);
            }
        }

        /// <summary>
        /// Get SecurityToken Issuer from configuration.
        /// </summary>
        public static string SecurityTokenIssuer
        {
            get
            {
                return MyConvert.ToString(Startup.Configuration["AppSettings:SecurityToken:Issuer"]);
            }
        }
        #endregion

        #region Redis
        public static bool RedisHome
        {
            get
            {
                return MyConvert.ToBoolean(Startup.Configuration["AppSettings:Redis:Home"]);
            }
        }
        public static bool RedisProduct
        {
            get
            {
                return MyConvert.ToBoolean(Startup.Configuration["AppSettings:Redis:Product"]);
            }
        }
        #endregion Redis

        #region Mongo
        public static bool MongoSearch
        {
            get
            {
                return MyConvert.ToBoolean(Startup.Configuration["AppSettings:Mongo:Search"]);
            }
        }
        public static bool MongoProduct
        {
            get
            {
                return MyConvert.ToBoolean(Startup.Configuration["AppSettings:Mongo:Product"]);
            }
        }
        public static bool MongoReview
        {
            get
            {
                return MyConvert.ToBoolean(Startup.Configuration["AppSettings:Mongo:Review"]);
            }
        }
        #endregion Mongo

    }
}