using System.ComponentModel.DataAnnotations;

namespace BestPlace.Infrastructure.Data;

public class Questionnaire
{
    [Key] 
    public Guid Id { get; set; } = Guid.NewGuid();


    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [MaxLength(500)]
    public string Description { get; set; }

    [Required]
    public DateTime DueDate { get; set; }

    public ICollection<SubmitQuestionnaire> SubmitQuestionnaires { get; set; } = new List<SubmitQuestionnaire>();
}