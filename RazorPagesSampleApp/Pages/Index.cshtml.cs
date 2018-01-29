using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesSampleApp.Models;

namespace RazorPagesSampleApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IElementRepository _elementRepository;
        public IndexModel(IElementRepository elementRepository)
        {
            _elementRepository = elementRepository;
        }
        [TempData]
        public string Message { get; set; }

        [BindProperty]
        public IEnumerable<Element> Elements { get; private set; }

        public async Task OnGetAsync()
        {
            await GetElements();
        }

        public async Task<IActionResult> OnPostAsync(Element element)
        {
            await OnGetAsync();
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            await _elementRepository.Add(element);
            Message = $"Added: {element.Description}";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostClearListAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _elementRepository.RemoveAll();

            return Page();
        }

        public async Task<IActionResult> OnPostStarAsync(Guid id)
        {
            if (ModelState.IsValid)
            {
                await _elementRepository.Star(id);
                return RedirectToPage();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostCrossAsync(Guid id)
        {
            if (ModelState.IsValid)
            {
                await _elementRepository.Cross(id);
                return RedirectToPage();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostRemoveAsync(Guid id)
        {
            if (ModelState.IsValid)
            {
                await _elementRepository.Remove(id);
                return RedirectToPage();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostEditAsync(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if ((await _elementRepository.Find(id) != null))
                return RedirectToPage("Edit", new { id = id });

            return Page();
        }
        private async Task GetElements()
        {
            Elements = await _elementRepository.GetAll();
        }
    }
}
