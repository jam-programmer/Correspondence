using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common;
public sealed record ApiOption
{
    public string? BaseUrl { set; get; }
    public Dictionary<string, string>? HeaderParameters { set; get; }
    public Dictionary<string, string>? QueryParameters { set; get; }
    public Dictionary<string, string>? Data { set; get; }
    public object? DataBody { set; get; }
    public TimeSpan TimeOut { set; get; }
    public string? BearerToken { set; get; }
    public string? AuthorizationTitle { set; get; }

    public string? userName { set; get; } = string.Empty;
    public string? Password { set; get; } = string.Empty;
}