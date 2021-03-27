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
    public class AddCocktailModel : PageModel
    {

        public ICocktailRepository cocktailRepository;
        
        // [BindProperty]
        public Item cocktailToAdd {get;set;}

        public AddCocktailModel(ICocktailRepository cocktailRepository)
        {
            this.cocktailRepository = cocktailRepository;
        }


        // public IActionResult OnGet(string cocktailToPass)
        public void OnGet()
        {            
              
        }

        public async Task<IActionResult> OnPost(Item cocktailToAdd)
        {
            // call the add method
            await cocktailRepository.AddItemAsync(cocktailToAdd);
         
            // redirect to summary page
            return Redirect("/Cocktails");

        }
    }
}
