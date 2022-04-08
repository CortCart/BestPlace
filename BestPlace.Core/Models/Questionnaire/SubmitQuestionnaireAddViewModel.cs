using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BestPlace.Core.Models.Questionnaire;

public class SubmitQuestionnaireAddViewModel
{
    [Key]
    public Guid QuestionnaireId { get; set; }


    [DisplayName("Answear")]
    [MaxLength(500)]
    [Required]
    public string Answer { get; set; }

}