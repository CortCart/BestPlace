using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BestPlace.Core.Models.Roles;

public class RoleAddViewModel
{
    [Required]
    [MaxLength(50)]
    [DisplayName("Name")]
    public string Name { get; set; }
}