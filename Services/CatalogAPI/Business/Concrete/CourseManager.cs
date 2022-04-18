using AutoMapper;
using CatalogAPI.Business.Abstract;
using CatalogAPI.Entities;
using CatalogAPI.Entities.Dtos;
using CatalogAPI.Settings;
using Core.Results;
using MongoDB.Driver;

namespace CatalogAPI.Business.Concrete
{
    internal class CourseManager : ICourseService
    {
        private readonly IMongoCollection<Course> _courseCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        // Will be repository pattern
        public CourseManager(IDataBaseSettings databaseSettings, IMapper mapper)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _courseCollection = database.GetCollection<Course>(databaseSettings.CourseCollectionName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }
        public async Task<IJsonDataResult<List<CourseDto>>> GetAllAsync() 
        {
            var courses = await _courseCollection.Find(course => true).ToListAsync();
            if (courses != null)
            {
                foreach (var course in courses)
                {
                    course.Category = await _categoryCollection.Find<Category>(category => category.Id == course.CategoryId).FirstAsync();
                }
            }
            else
            {
                courses = new List<Course>();
            }
            return new SuccessJsonDataResult<List<CourseDto>>(_mapper.Map<List<CourseDto>>(courses));
        }
        public async Task<IJsonDataResult<CourseDto>> GetByIdAsync(string id) 
        {
            var course = await _courseCollection.Find<Course>(course => course.Id == id).FirstOrDefaultAsync();
            if (course == null) 
            {
                return new ErrorJsonDataResult<CourseDto>("Course Not Found!");
            }
            course.Category = await _categoryCollection.Find<Category>(category => category.Id == course.CategoryId).FirstOrDefaultAsync();
            return new SuccessJsonDataResult<CourseDto>(_mapper.Map<CourseDto>(course));
        }
        public async Task<IJsonDataResult<List<CourseDto>>> GetAllByUserIdAsync(string userId)
        {
            var courses = await _courseCollection.Find<Course>(course => course.UserId == userId).ToListAsync();
            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = await _categoryCollection.Find<Category>(category => category.Id == course.CategoryId).FirstAsync();
                }
            }
            else
            {
                courses = new List<Course>();
            }
            return new SuccessJsonDataResult<List<CourseDto>>(_mapper.Map<List<CourseDto>>(courses));
        }
        public async Task<IJsonDataResult<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto) 
        {
            var newCourse = _mapper.Map<Course>(courseCreateDto);
            await _courseCollection.InsertOneAsync(newCourse);
            return new SuccessJsonDataResult<CourseDto>(_mapper.Map<CourseDto>(newCourse));
        }
        public async Task<IJsonResult> UpdateAsync(CourseUpdateDto courseUpdateDto) 
        {
            var updateCourse = _mapper.Map<Course>(courseUpdateDto);
            var result = await _courseCollection.FindOneAndReplaceAsync(course => course.Id == courseUpdateDto.Id, updateCourse);
            if (result == null)
            {
                return new ErrorJsonResult("Records Not Found!");
            }
            return new SuccessJsonResult("Record Updated!");
        }
        public async Task<IJsonResult> DeleteAsync(string Id) 
        {
            var result = await _courseCollection.DeleteOneAsync(course => course.Id == Id);
            if (result.DeletedCount > 0)
            {
                return new SuccessJsonResult("Deleted");
            }
            else
            {
                return new ErrorJsonResult("Course Not Found!");
            }
        }
    }
}
