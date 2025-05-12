using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GabyWorld.Data
{
    /// <summary>
    /// Settings table in DB
    /// </summary>
    public class SettingsDataModel
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Value { get; set; }
    }
}
