using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesSampleApp.Models;

namespace RazorPagesSampleApp.Pages
{
    public class EditModel : PageModel
    {
        private readonly IElementRepository _elementRepository;

        public EditModel(IElementRepository elementRepository)
        {
            _elementRepository = elementRepository;
        }

        public Element EditElement { get; private set; }

        public async Task OnGetAsync(Guid id)
        {
            await GetEditElement(id);
        }

        public async Task<IActionResult> OnPostAsync(Element element)
        {
            if (ModelState.IsValid)
            {
                await _elementRepository.Update(element);
                return RedirectToPage("Index");
            }
            return RedirectToPage();
        }

        private async Task GetEditElement(Guid id)
        {
            EditElement = await _elementRepository.Find(id);
        }
    }
}