

using Domain.Enum;

namespace Domain.Entities;

public class UserEntity : BaseEntity
{
    public string? UserName { get; set; }
    public string? ImageUrl { get; set; }
    public string? FullName { get; set; }
    public string? PhoneNumber { set; get; }
    public ICollection<UserActivityEntity>? UserActivities {  get; set; }    

}
