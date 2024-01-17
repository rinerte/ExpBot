using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public long TelegramUserId { get; set; }
        public int Level { get; set; } = 1;
        public long Expierence { get; set; } = 0;
        public int Streak { get; set; } = 1;
        public int Multiplier { get; set; } = 1;
        public DateTime LastActivity { get; set; } = DateTime.Now;

    }
}
