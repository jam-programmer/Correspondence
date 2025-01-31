using Application.Command.Letter;
using Application.Common;
using Application.DataTransferObject;
using Application.Service;
using Application.ViewModel;
using EndPoint_WebUi.Authorization;
using Humanizer;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUi.Pages.Letter;
[JanAuthorization("Correspondence")]

public class CreateModel (IJanService janService,IMediator mediator): PageModel
{
    private readonly IMediator _mediator=mediator;
    private readonly IJanService _janService=janService;
    public List<JanUserViewModel>? JanUsers { get; set; }
    public LetterDto? Letter { get; set; }
    public async Task OnGet()
    {
        JanUsers=await _janService.GetUsersAsync(); 
    }


    public async Task<JsonResult> 
        OnPostRegisterLetter([FromForm] LetterDto? Letter)
    {
        Letter.UserName = "4311169140";
        BaseResult resultRegisterLetter =
             await _mediator.Send(new InsertLetterCommand(Letter!));
        return new JsonResult(resultRegisterLetter);
    }
}
