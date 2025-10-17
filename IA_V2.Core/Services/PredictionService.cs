using IA_V2.Core.Entities;
using IA_V2.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IA_V2.Core.Services
{
    public class PredictionService : IPredictionService
    {
        private readonly IBaseRepository<Prediction> _predictionRepository;

        public PredictionService(IBaseRepository<Prediction> predictionRepository)
        {
            _predictionRepository = predictionRepository;
        }

        public async Task<IEnumerable<Prediction>> GetAllPredictionAsync()
        {
            return await _predictionRepository.GetAll();
        }

        public async Task<Prediction> GetPredictionByIdAsync(int id)
        {
            return await _predictionRepository.GetById(id);
        }

        public async Task InsertPredictionAsync(Prediction prediction) => await _predictionRepository.Add(prediction);

        public async Task UpdatePredictionAsync(Prediction prediction) => await _predictionRepository.Update(prediction);

        public async Task DeletePredictionAsync(int id) => await _predictionRepository.Delete(id);
    }
}
