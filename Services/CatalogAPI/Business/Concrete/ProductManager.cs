using AutoMapper;
using Ecommerce.Services.Catalog.APIBusiness.Abstract;
using Ecommerce.Services.Catalog.APIEntities;
using Ecommerce.Services.Catalog.APIEntities.Dtos;
using Ecommerce.Services.Catalog.APISettings;
using Core.Results;
using MongoDB.Driver;
using MassTransit;
using Core.Messages;

namespace Ecommerce.Services.Catalog.APIBusiness.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint; // Mass transit publish endpoint

        public int ProductNameChangedEvent { get; private set; }

        // Will be repository pattern
        public ProductManager(IDataBaseSettings databaseSettings, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _productCollection = database.GetCollection<Product>(databaseSettings.ProductCollectionName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<IJsonDataResult<List<ProductDto>>> GetAllAsync() 
        {
            _productCollection.DeleteOne<Product>(c => c.Id == "625f1e27c82c415e9d5ca3c2");
            var products = await _productCollection.Find(product => true).ToListAsync();
            if (products != null)
            {
                foreach (var product in products)
                {
                    product.Category = await _categoryCollection.Find<Category>(category => category.Id == product.CategoryId).FirstAsync();
                }
            }
            else
            {
                products = new List<Product>();
            }
            return new SuccessJsonDataResult<List<ProductDto>>(_mapper.Map<List<ProductDto>>(products));
        }

        public async Task<IJsonDataResult<ProductDto>> GetByIdAsync(string id) 
        {
            var product = await _productCollection.Find<Product>(product => product.Id == id).FirstOrDefaultAsync();
            if (product == null) 
            {
                return new ErrorJsonDataResult<ProductDto>("Product Not Found!");
            }
            product.Category = await _categoryCollection.Find<Category>(category => category.Id == product.CategoryId).FirstOrDefaultAsync();
            return new SuccessJsonDataResult<ProductDto>(_mapper.Map<ProductDto>(product));
        }

        public async Task<IJsonDataResult<List<ProductDto>>> GetAllByUserIdAsync(string userId)
        {
            var products = await _productCollection.Find<Product>(product => product.UserId == userId).ToListAsync();
            if (products.Any())
            {
                foreach (var product in products)
                {
                    product.Category = await _categoryCollection.Find<Category>(category => category.Id == product.CategoryId).FirstAsync();
                }
            }
            else
            {
                products = new List<Product>();
            }
            return new SuccessJsonDataResult<List<ProductDto>>(_mapper.Map<List<ProductDto>>(products));
        }

        public async Task<IJsonDataResult<ProductDto>> CreateAsync(ProductCreateDto productCreateDto) 
        {
            var newProduct = _mapper.Map<Product>(productCreateDto);
            await _productCollection.InsertOneAsync(newProduct);
            return new SuccessJsonDataResult<ProductDto>(_mapper.Map<ProductDto>(newProduct));
        }

        public async Task<IJsonResult> UpdateAsync(ProductUpdateDto productUpdateDto) 
        {
            var updateProduct = _mapper.Map<Product>(productUpdateDto);
            var result = await _productCollection.FindOneAndReplaceAsync(product => product.Id == productUpdateDto.Id, updateProduct);

            if (result == null)
            {
                return new ErrorJsonResult("Records Not Found!");
            }

            // Send updated event to rabbitmq queue via masstransit
            await _publishEndpoint.Publish<ProductUpdatedEvent>(new ProductUpdatedEvent 
            {
                Id = productUpdateDto.Id,
                ProductName = productUpdateDto.Name,
                Price = productUpdateDto.Price
            });

            return new SuccessJsonResult("Record Updated!");
        }

        public async Task<IJsonResult> DeleteAsync(string Id) 
        {
            var result = await _productCollection.DeleteOneAsync(product => product.Id == Id);
            if (result.DeletedCount > 0)
            {
                return new SuccessJsonResult("Deleted");
            }
            else
            {
                return new ErrorJsonResult("Product Not Found!");
            }
        }
    }
}
