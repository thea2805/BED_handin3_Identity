using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BED_handin3_Identity.Pages.Kitchen
{
    public class KitchenModel : PageModel
    {
        public string? Date { get; set; }
        public string? DateReverse { get; set; }

        public void OnGet()
        {
            Date = DateTime.Now.ToShortDateString();
            DateReverse = DateTime.Now.ToString("yyyy-MM-dd");


            //Call get method(date)
            /*Display the following:
                Expected number of guests
                Adults checked in 
                Children checked in 
                Guests yet to check in   
            */


        }
    }
}
