using System.ComponentModel.DataAnnotations;

namespace BestPlace.Infrastructure.Data;

public class Image
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public byte[] Source { get; set; }
}