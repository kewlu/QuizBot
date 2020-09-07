using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizBot.Entities
{
    public class User : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public long UserId { get; set; }
        
        public long ChatId { get; set; }
        
        public string Name { get; set; }

        public long Score { get; set; }
    }
}