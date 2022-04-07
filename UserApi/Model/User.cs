using System.ComponentModel.DataAnnotations;

namespace UserApi.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }
        public string? Phone { get; set; }
        public int? Age { get; set; }

    }
}
