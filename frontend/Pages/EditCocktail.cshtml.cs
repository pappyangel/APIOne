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

        public ICocktailRepository cocktailRepository;
        public Item cocktailToEdit;

        public EditCocktailModel(ICocktailRepository cocktailRepository)
        {
            this.cocktailRepository = cocktailRepository;
        }



        // public IActionResult OnGet(string cocktailToPass)
        public IActionResult OnGet(int id)
        {
            cocktailToEdit = cocktailRepository.GetItem(id);
            
            // cocktailToEdit = JsonSerializer.Deserialize<Item>(cocktailToPass);            
            int dog = 0;
            dog = cocktailToEdit.Id;

            return Page();

        }
    }
}
