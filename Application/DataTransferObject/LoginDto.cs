using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DataTransferObject;

public sealed record LoginDto
{
    public string token {  get; set; }
    [JsonIgnore]
    public string path {  get; set; }   
}
