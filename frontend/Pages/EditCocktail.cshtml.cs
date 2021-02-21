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
using Microsoft.AspNetCore.Hosting;

namespace frontend.Pages
{
    public class EditCocktailModel : PageModel
    {

        public ICocktailRepository cocktailRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        // [BindProperty]
        public Item cocktailToEdit { get; set; }

        public EditCocktailModel(ICocktailRepository cocktailRepository, IWebHostEnvironment webHostEnvironment)
        {
            this.cocktailRepository = cocktailRepository;
            // IWebHostEnvironment service allows us to get the
            // absolute path of WWWRoot folder
            this.webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public IFormFile ctImage { get; set; }

        // public IActionResult OnGet(string cocktailToPass)
        public IActionResult OnGet(int id)
        {
            cocktailToEdit = cocktailRepository.GetItem(id);
            return Page();

        }

        public async Task<IActionResult> OnPost(Item cocktailToEdit)
        {
            
             if (ctImage != null)
            {
                // If a new photo is uploaded, the existing photo must be
                // deleted. So check if there is an existing photo and delete
                if (cocktailToEdit.ImagePath != null)
                {
                    string filePath = Path.Combine(webHostEnvironment.WebRootPath,
                        "images", cocktailToEdit.ImagePath);
                    System.IO.File.Delete(filePath);
                }
                // Save the new photo in wwwroot/images folder and update
                // PhotoPath property of the employee object
                cocktailToEdit.ImagePath = ProcessUploadedFile();
            }
            
            // call the update method            
            await cocktailRepository.UpdateItemAsync(cocktailToEdit);

            // redirect to summary page
            return Redirect("/Cocktails");

        }


        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;

            if (ctImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + ctImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    ctImage.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}
