using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IA_V2.Core.Entities;

namespace IA_V2.Core.Interfaces
{
    public interface ITextService
    {
        Task<IEnumerable<Text>> GetAllTextAsync();
        Task<Text> GetTextByIdAsync(int id);
        Task InsertTextAsync(Text text);
        Task UpdateTextAsync(Text text);
        Task DeleteTextAsync(int id);
    }
}
