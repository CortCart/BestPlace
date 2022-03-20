using System.ComponentModel.DataAnnotations;

namespace BestPlace.Infrastructure.Data;

public class Delivery
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(250)]
    public string Addres { get; set; }

    [MaxLength(500)]
    public string Description { get; set; }
}