using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BED_handin3_Identity.Pages.Restaurant
{
    [Authorize("CanEnterRestaurant")]
    public class RestaurantModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {
            Message = "Hello";
        }

        public void OnPost()
        {
            Message = "Post used";
        }
    }
}
