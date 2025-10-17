using IA_V2.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IA_V2.Core.Entities;

namespace IA_V2.Core.Services
{
    public class TextService : ITextService
    {
        private readonly IBaseRepository<Text> _textRepository;

        public TextService(IBaseRepository<Text> textRepository)
        {
            _textRepository = textRepository;
        }

        public async Task<IEnumerable<Text>> GetAllTextAsync()
        {
            return await _textRepository.GetAll();
        }

        public async Task<Text> GetTextByIdAsync(int id)
        {
            return await _textRepository.GetById(id);
        }

        public async Task InsertTextAsync(Text text)
        {
            await _textRepository.Add(text);
        }

        public async Task UpdateTextAsync(Text text)
        {
            await _textRepository.Update(text);
        }

        public async Task DeleteTextAsync(int id)
        {
            await _textRepository.Delete(id);
        }
    }
}
