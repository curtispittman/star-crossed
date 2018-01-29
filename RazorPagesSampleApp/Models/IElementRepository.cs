using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesSampleApp.Models
{
    public interface IElementRepository
    {
        Task<IEnumerable<Element>> GetAll();
        Task Add(Element todo);
        Task Remove(Guid id);
        Task<Element> Find(Guid id);
        Task Update(Element todo);
        Task Star(Guid id);
        Task Cross(Guid id);
        Task RemoveAll();
    }
}
