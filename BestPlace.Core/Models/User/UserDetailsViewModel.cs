using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BestPlace.Core.Models.User;

public class UserDetailsViewModel
{
    public string Id { get; set; }

    [Required]
    [Display(Name = "First name")]
    public string FirstName { get; set; }

    [Required]
    [Display(Name = "Last name")]
    public string LastName { get; set; }

    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required]
    [MaxLength(250)]
    [Display(Name = "Address")]
    public string Address { get; set; }

    [Required]
    [RegularExpression("[+]{1}359 [0-9]{3} [0-9]{4}")]
    [Display(Name = "Phone")]
    public string Phone { get; set; }

    [Url]
    [Display(Name = "Instagram")]
    public string InstagramUrl { get; set; }


    [Url]
    [Display(Name = "Facebook")]
    public string Facebook { get; set; }


    [Required]
    [DisplayName("ImageId")]
    public Guid ImageId { get; set; }

    public ICollection<UserItemDetailsWithImageViewModel> Items { get; set; }

}