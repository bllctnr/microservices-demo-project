using AutoMapper;
using Ecommerce.Services.Catalog.APIBusiness.Abstract;
using Ecommerce.Services.Catalog.APIEntities;
using Ecommerce.Services.Catalog.APIEntities.Dtos;
using Ecommerce.Services.Catalog.APISettings;
using Core.Results;
using MongoDB.Driver;

namespace Ecommerce.Services.Catalog.APIServices
{
    public class CategoryManager : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;


        // Will be repository pattern
        public CategoryManager(IDataBaseSettings databaseSettings, IMapper mapper)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<IJsonDataResult<List<CategoryDto>>> GetAllAsync() 
        {
            var result = await _categoryCollection.Find(category => true).ToListAsync();
            return new SuccessJsonDataResult<List<CategoryDto>>(_mapper.Map<List<CategoryDto>>(result));
        }

        public async Task<IJsonDataResult<CategoryDto>> CreateAsync(CategoryDto categoryDto)
        {
            _categoryCollection.FindOneAndDelete<Category>(category => category.Id == null);

            var newCategory = _mapper.Map<Category>(categoryDto);
            await _categoryCollection.InsertOneAsync(newCategory);
            return new SuccessJsonDataResult<CategoryDto>(_mapper.Map<CategoryDto>(newCategory));
        }

        public async Task<IJsonDataResult<CategoryDto>> GetByIdAsync(string Id) 
        {
            var result = await _categoryCollection.Find<Category>(category => category.Id == Id).FirstOrDefaultAsync();
            if (result == null)
            {
                
            }
            return new SuccessJsonDataResult<CategoryDto>(_mapper.Map<CategoryDto>(result));
        }

        public async Task<Core.Results.IJsonResult> DeleteAsync(string categoryId)
        {
            var result = await _categoryCollection.DeleteOneAsync(category => category.Id == categoryId);
            if (result.DeletedCount > 0)
            {
                return new SuccessJsonResult("Category Deleted");
            }
            else
            {
                return new ErrorJsonResult("Category Not Found!");
            }
        }
    }
}
