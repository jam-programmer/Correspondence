using Application.Service;
using Application.ViewModel;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUi.Pages.Letter;

public class CreateModel (IJanService janService): PageModel
{
    private readonly IJanService _janService=janService;
    public List<JanUserViewModel>? JanUsers { get; set; }
    public async Task OnGet()
    {
        JanUsers=await _janService.GetUsersAsync(); 
    }
}
