using AutoMapper;
using BC.Bicycles.Models.Exceptions;
using BC.Problems.Boundary.Features;
using BC.Problems.Boundary.Request;
using BC.Problems.Boundary.Response;
using BC.Problems.Models;
using BC.Problems.Repositories.Interfaces;
using BC.Problems.Services.Interfaces;
using Newtonsoft.Json;

namespace BC.Problems.Services
{
    /// <summary>
    /// Service to manage bicycles.
    /// </summary>
    public class BicycleService : IBicycleService
    {
        private readonly IMapper _mapper;
        private readonly IBicycleRepository _bicycleRepository;
        private readonly ILogger<BicycleService> _logger; // ToDo D: Change to serilog.

        public BicycleService(IMapper mapper, IBicycleRepository bicycleRepository, ILogger<BicycleService> logger)
        {
            _mapper = mapper;
            _bicycleRepository = bicycleRepository;
            _logger = logger;
        }

        public async Task<List<BicycleForReadModel>> GetBicycleListAsync(BicycleParameters bicycleParameters, HttpResponse response = null)
        {
            var bicycles = await _bicycleRepository.GetBicyclesAsync(bicycleParameters);

            if (response != null)
            {
                response.Headers.Add("Pagination", JsonConvert.SerializeObject(bicycles.MetaData));
            }

            return _mapper.Map<List<BicycleForReadModel>>(bicycles);
        }

        public async Task<BicycleForReadModel> GetBicycleAsync(Guid id)
        {
            var bicycleEntity = await _bicycleRepository.GetBicycleAsync(id);
            CheckIfFound(id, bicycleEntity);
            return _mapper.Map<BicycleForReadModel>(bicycleEntity);
        }

        public async Task<Guid> CreateBicycleAsync(BicycleForCreateOrUpdateModel model)
        {
            //await CheckIfAlreadyExists(model); // ToDo D: Do we need handler for existing bicycles?

            var bicycleEntity = _mapper.Map<Bicycle>(model);

            await _bicycleRepository.CreateBicycleAsync(bicycleEntity);

            return bicycleEntity.Id;
        }

        public async Task DeleteBicycleAsync(Guid id)
        {
            var bicycleEntity = await _bicycleRepository.GetBicycleAsync(id);
            CheckIfFound(id, bicycleEntity);

            await _bicycleRepository.DeleteBicycleAsync(bicycleEntity);
        }

        public async Task UpdateBicycleAsync(Guid id, BicycleForCreateOrUpdateModel model)
        {
            //await CheckIfAlreadyExists(model);

            var bicycleEntity = await _bicycleRepository.GetBicycleAsync(id);
            CheckIfFound(id, bicycleEntity);

            _mapper.Map(model, bicycleEntity);
            await _bicycleRepository.UpdateBicycleAsync(bicycleEntity);
        }

        public async Task<BicycleForCreateOrUpdateModel> GetBicycleForUpdateModelAsync(Guid id)
        {
            var bicycleEntity = await GetBicycleAsync(id);

            return _mapper.Map<BicycleForCreateOrUpdateModel>(bicycleEntity);
        }

        private void CheckIfFound(Guid id, Bicycle bicycleEntity)
        {
            if (bicycleEntity is null)
            {
                _logger.LogInformation($"Bicycle with id: {id} doesn't exist in the database.");
                throw new EntityNotFoundException(nameof(Bicycle), id);
            }
        }
    }
}
