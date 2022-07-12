namespace Ecommerce.Services.Catalog.APISettings
{
    public class DataBaseSettings : IDataBaseSettings
    {
        public string ProductCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}