using System.ComponentModel.DataAnnotations;
using BestPlace.Core.Models.Category;

namespace BestPlace.Core.Models;

public class QuestionnaireDetailsViewModel
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [MaxLength(500)]
    public string Description { get; set; }

    public ICollection<QuestionnaireSubmitViewModel> Submits { get; set; } = new List<QuestionnaireSubmitViewModel>();
}