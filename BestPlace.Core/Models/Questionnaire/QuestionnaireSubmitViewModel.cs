using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BestPlace.Infrastructure.Data.Identity;

namespace BestPlace.Core.Models.Category;

public class QuestionnaireSubmitViewModel
{
    [Required]
    [MaxLength(500)]
    public string Answer { get; set; }

    [Required]
    public string UserId { get; set; }

    [Required]
    public string UserName { get; set; }
}