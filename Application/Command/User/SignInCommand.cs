using Application.Common;
using Domain.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.User;

public sealed record SignInCommand : IRequest<BaseResult>
{
    public string? username { get; set; }
    public string? name { get; set; }
    public string? mobile { get; set; }
    public string? image { get; set; }
    public SignInCommand(
       
        string? username,
        string? name,
        string? mobile,
        string? image)
    {
      
        this.username = username;
        this.name = name;
        this.mobile = mobile;
        this.image = image;
    }
}


