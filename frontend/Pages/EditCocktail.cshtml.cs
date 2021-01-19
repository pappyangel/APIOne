using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using frontend.models;
using System.Text.Json;

namespace frontend.Pages
{
    public class EditCocktailModel : PageModel
    {
        
        public Item cocktailToEdit = new();
        public IActionResult OnGet(string cocktailToPass)
        {
            cocktailToEdit = JsonSerializer.Deserialize<Item>(cocktailToPass);
            int dog = 0;
            dog = cocktailToEdit.Id;

            return Page();

        }
    }
}
