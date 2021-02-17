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
        
        // [BindProperty]
        public Item cocktailToEdit {get;set;}

        public EditCocktailModel(ICocktailRepository cocktailRepository)
        {
            this.cocktailRepository = cocktailRepository;
        }



        // public IActionResult OnGet(string cocktailToPass)
        public IActionResult OnGet(int id)
        {
            cocktailToEdit = cocktailRepository.GetItem(id);
            return Page();

        }

        public async Task<IActionResult> OnPost(Item cocktailToEdit)
        {
            
            int dog = 0;
            // dog = item.Id;
            dog = cocktailToEdit.Id;

            // List<Item> bobo = await cocktailRepository.UpdateItemAsync(cocktailToEdit);
            await cocktailRepository.UpdateItemAsync(cocktailToEdit);
            // dog = cocktailfromPost.Id;


            return Redirect("/Cocktails");

        }
    }
}
