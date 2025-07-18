using AdvancedADO;
using DocumentDBClient;
using Microsoft.Extensions.Configuration;

namespace ECommerce.Business
{
    public class CommonBusiness
    {
        public static IConfiguration configuration;
        #region Constructor
        /// <summary>
        /// This construction is set properties default value based on its data type in table.
        /// </summary>
        public CommonBusiness(IConfiguration config)
        {
            configuration = config;
            this.ConnectionStringKey = "Default";
            this.DocumentConnectionStringKey = "DefaultMongoDB";
        }
        public CommonBusiness(IConfiguration config, string connectionStringKey)
        {
            configuration = config;

            this.ConnectionStringKey = connectionStringKey;
        }
        #endregion

        #region public override Properties
        public string ConnectionStringKey { get; set; }
        public string DocumentConnectionStringKey { get; set; }
        #endregion

        #region Private Methods
        public ISql CreateSqlInstance()
        {
            SqlFactory sqlFactory = new SqlFactory(configuration);
            return sqlFactory.CreateInstance(ConnectionStringKey);
        }
        public ISql CreateSqlInstance(string connectionStringKey)
        {
            SqlFactory sqlFactory = new SqlFactory(configuration);
            return sqlFactory.CreateInstance(connectionStringKey);
        }

        public IDocument<TEntity> CreateDocumentInstance<TEntity>(string collectionName) where TEntity : IBaseEntity
        {
            DocumentFactory<TEntity> documentFactory = new DocumentFactory<TEntity>(configuration, collectionName);
            return documentFactory.CreateInstance("DefaultMongoDB");
        }
        public IDocument<TEntity> CreateDocumentInstance<TEntity>(string connectionStringKey, string collectionName) where TEntity : IBaseEntity
        {
            DocumentFactory<TEntity> documentFactory = new DocumentFactory<TEntity>(configuration, collectionName);
            return documentFactory.CreateInstance(connectionStringKey);
        }
        #endregion
    }
}
