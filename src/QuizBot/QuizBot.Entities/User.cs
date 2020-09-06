using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizBot.Entities
{
    public class User : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public int TelegramId { get; set; }
        
        public int ChatId { get; set; }
        
        public string Name { get; set; }
        
        public int Score { get; set; }
    }
}