using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DisasterAlleviationFoundation.Pages
{
    public class LoginModel : PageModel
    {

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public void OnGet()
        {
            
        }

        public IActionResult OnPost()
        {
            string adminUsername = "admin";
            string adminPassword = "Goodfellas-145";

            if (Username == adminUsername && Password == adminPassword)
            {
                return RedirectToPage("admin_main");
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password");
                return Page();
            }
        }


    }
}
