using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BestPlace.Core.Models;

public class QuestionnaireAddViewModel
{
    [Required]
    [MaxLength(100)]
    [DisplayName("Name")]
    public string Name { get; set; }

    [Required]
    [MaxLength(500)]
    [DisplayName("Description")]
    public string Description { get; set; }

    [Required]
    public string DueDate { get; set; }

}