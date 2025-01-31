using Application.Common.Resource;
using Domain.Enum;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.DataTransferObject;

public sealed record LetterDto
{
    public Guid Id { get; set; }
    public string? Number { get; set; }
    public string? Title { get; set; }
    public string? Text { get; set; }
    public string? ReferrerOriginal { get; set; }
    public string? ReferrerCopy { get; set; }
    public string? UserName {  get; set; }
    public PriorityType Priority { get; set; }
    public LetterType Type { get; set; }
    public List<IFormFile>? Files { get; set; }
}
public class ReferrerItemModel
{
    public string? UserName { get; set; }
    public string? FullName { get; set; }
}