using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BestPlace.Infrastructure.Data.Identity;

namespace BestPlace.Infrastructure.Data;

public class SubmitQuestionnaire
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(500)]
    public string Answer { get; set; }

    [Required]
    public Guid QuestionnaireId { get; set; }

    [ForeignKey(nameof(QuestionnaireId))]
    public Questionnaire Questionnaire { get; set; }

    [Required]
    public string UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public ApplicationUser User { get; set; }
}
