
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
    public class DeleteCocktailModel : PageModel
    {

          public ICocktailRepository cocktailRepository;
        
        [BindProperty]        
        public Item cocktailToDelete {get;set;}

        public DeleteCocktailModel(ICocktailRepository cocktailRepository)
        {
            this.cocktailRepository = cocktailRepository;
        }
        
        public IActionResult OnGet(int id)
        {            
            cocktailToDelete = cocktailRepository.GetItem(id);
            return Page();
        }

        public async Task<IActionResult> OnPost(Item cocktailToDelete)
        {
            // call the add method
            //await cocktailRepository.DeleteItemAsync(cocktailToDelete.Id);
            await cocktailRepository.DeleteItemAsync(cocktailToDelete);

            // redirect to summary page
            return Redirect("/Cocktails");

        }

    }

}