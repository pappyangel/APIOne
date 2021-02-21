using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using frontend.models;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using System.IO;

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

        [BindProperty]
        public IFormFile ctImage { get; set; }
        
        public MemoryStream inMemImage = new MemoryStream(100);
        


        // public IActionResult OnGet(string cocktailToPass)
        public IActionResult OnGet(int id)
        {
            cocktailToEdit = cocktailRepository.GetItem(id);
            return Page();

        }

        public IActionResult OnPost(Item cocktailToEdit)
        {            
            inMemImage.SetLength(ctImage.Length);
            ctImage.CopyTo(inMemImage);
            cocktailToEdit.cocktailImage = inMemImage.ToArray();

            return Page();

            
            // call the update method
            //await cocktailRepository.UpdateItemAsync(cocktailToEdit);
         
            // redirect to summary page
            //return Redirect("/Cocktails");

        }
    }
}
