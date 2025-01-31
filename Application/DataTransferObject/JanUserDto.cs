using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransferObject;

public sealed record  JanUserDto
{
    public bool success { get; set; }
    public string? username { get; set; }
    public string? name { get; set; }
    public string? mobile { get; set; }
    public string? image { get; set; }
}
