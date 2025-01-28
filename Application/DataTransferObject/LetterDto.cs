using Domain.Enum;

namespace Application.DataTransferObject;

public sealed record LetterDto
{
    public Guid Id { get; set; }
    public string? Number { get; set; }
    public string? Title { get; set; }
    public string? Text { get; set; }
    public string? Referrer { get; set; }
    public PriorityType Priority { get; set; }

}
