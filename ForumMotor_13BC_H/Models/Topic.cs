namespace ForumMotor_13BC_H.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreateDate { get; set; }


    }
}
