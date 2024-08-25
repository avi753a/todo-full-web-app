using Microsoft.AspNetCore.Identity;
namespace TodoApp.Data.Entites
{
    public class User : IdentityUser<Guid>
    {
        [PersonalData]
        //For Future Reference 
        public string? Name { get; set; }

    }
}
