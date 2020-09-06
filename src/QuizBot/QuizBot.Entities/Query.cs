namespace QuizBot.Entities
{
    public class Query : IEntity
    {
        public int Id { get; set; }
        
        public string Question { get; set; }
        
        public string Answer { get; set; }
    }
}