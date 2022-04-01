using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BestPlace.Core.Models.Category;

public class CategoryEditViewModel
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    [Required]
    public IFormFile Image { get; set; }
}