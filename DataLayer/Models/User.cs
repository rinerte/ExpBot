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
        public DateTime LastActivity { get; set; } = DateTime.Now.ToUniversalTime();
        /// <summary>
        /// Current action
        /// </summary>
        public Enums.Actions Action { get; set; } = Enums.Actions.Idle;
        public string Language { get; set; } = "en";
        public int CurrentData { get; set; }
        public int RowsThisDay { get; set; } 
        public int PagesThisDay { get; set; }
        public int ArticlesThisDay { get; set; }
        public DateTime RowsAdded { get; set; } = DateTime.Now.ToUniversalTime();
        public DateTime PagesAdded { get; set; } = DateTime.Now.ToUniversalTime();
        public DateTime ArticlesAdded { get; set; } = DateTime.Now.ToUniversalTime();

    }
}
