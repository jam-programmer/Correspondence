using Application.Common;
using Application.Service;
using Application.DataTransferObject;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Application.Command.User;

namespace WebUi.Pages;

public class AccessDeniedModel: PageModel
{
  
    public void OnGet()
    {

    }
}