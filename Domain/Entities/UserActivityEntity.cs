namespace Domain.Entities;

public class UserActivityEntity : BaseEntity
{
    public DateTime DateTime { get; set; }
    public string? Message { get; set; }
    public Guid UserId { get; set; }
    public UserEntity? User { get; set; }
}
