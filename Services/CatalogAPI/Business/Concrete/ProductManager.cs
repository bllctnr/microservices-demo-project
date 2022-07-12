using AutoMapper;
using Ecommerce.Services.Catalog.APIBusiness.Abstract;
using Ecommerce.Services.Catalog.APIEntities;
using Ecommerce.Services.Catalog.APIEntities.Dtos;
using Ecommerce.Services.Catalog.APISettings;
using Core.Results;
using MongoDB.Driver;
using MassTransit;
using Core.Messages;
using Core.Constants;

namespace Ecommerce.Services.Catalog.APIBusiness.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint; // Mass transit publish endpoint

        public int ProductNameChangedEvent { get; private set; }

        // Will be repository pattern
        // Returns success or error result for every request , it will be fit on Restful response types eg: if record not found response should be 204 No Content  
        public ProductManager(IDataBaseSettings databaseSettings, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _productCollection = database.GetCollection<Product>(databaseSettings.ProductCollectionName);
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<IJsonDataResult<List<ProductDto>>> GetAllAsync() 
        {
            var products = await _productCollection.Find(product => true).ToListAsync();
            if (products != null)
            {
                return new SuccessJsonDataResult<List<ProductDto>>(_mapper.Map<List<ProductDto>>(products));
            }
            else
            {
                return new ErrorJsonDataResult<List<ProductDto>>(null, Messages.RecordNotFount);
            }
        }

        public async Task<IJsonDataResult<ProductDto>> GetByIdAsync(string id) 
        {
            var product = await _productCollection.Find<Product>(product => product.Id == id).FirstOrDefaultAsync();
            if (product == null) 
            {
                return new ErrorJsonDataResult<ProductDto>(Messages.RecordNotFount);
            }
            return new SuccessJsonDataResult<ProductDto>(_mapper.Map<ProductDto>(product));
        }

        public async Task<IJsonDataResult<List<ProductDto>>> GetAllByUserIdAsync(string userId)
        {
            var products = await _productCollection.Find<Product>(product => product.UserId == userId).ToListAsync();
            if (products.Any())
            {
                return new SuccessJsonDataResult<List<ProductDto>>(_mapper.Map<List<ProductDto>>(products));
            }
            else
            {
                return new ErrorJsonDataResult<List<ProductDto>>(Messages.RecordNotFount);
            }
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
                return new ErrorJsonResult(Messages.RecordNotFount);
            }

            // Send updated event to rabbitmq queue via masstransit
            await _publishEndpoint.Publish<ProductUpdatedEvent>(new ProductUpdatedEvent 
            {
                Id = productUpdateDto.Id,
                ProductName = productUpdateDto.Name,
                Price = productUpdateDto.Price
            });

            return new SuccessJsonResult(Messages.RecordsUpdated);
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
                return new ErrorJsonResult(Messages.RecordNotFount);
            }
        }
    }
}
