using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BestPlace.Infrastructure.Data.Identity;

namespace BestPlace.Infrastructure.Data;

public class Call
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    
    public string UserId { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public ApplicationUser? User { get; set; }


    [Required]
    [MaxLength(500)]
    public string Problem { get; set; }
}