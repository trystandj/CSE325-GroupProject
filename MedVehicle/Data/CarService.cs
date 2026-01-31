namespace MedVehicle.MongoDB;

using MedVehicle.Models;
using MedVehicle.Infrastructure; 
using global::MongoDB.Driver;

public class CarService
{
    // Reference to the Cars collection in MongoDB
    private readonly IMongoCollection<Car> _carsCollection;

    // Constructor that accepts the MongoDB context
    public CarService(NamedCollection dbContext)
    {
        _carsCollection = dbContext.GetCollection<Car>("Cars");
    }

    //  CRUD Operations
    public async Task<List<Car>> GetCarsAsync() => 
        await _carsCollection.Find(_ => true).ToListAsync();

    //  Create a new car
    public async Task CreateAsync(Car newCar) => 
        await _carsCollection.InsertOneAsync(newCar);
        
    // READ: Get by PublicId
    public async Task<Car?> GetByPublicIdAsync(string publicId) =>
        await _carsCollection.Find(x => x.PublicId == publicId).FirstOrDefaultAsync();

    // UPDATE: Find by PublicId, then replace
    public async Task UpdateByPublicIdAsync(string publicId, Car updatedCar) =>
        await _carsCollection.ReplaceOneAsync(x => x.PublicId == publicId, updatedCar);

    // DELETE: Find by PublicId
    public async Task RemoveByPublicIdAsync(string publicId) =>
        await _carsCollection.DeleteOneAsync(x => x.PublicId == publicId);
}