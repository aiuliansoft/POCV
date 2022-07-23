using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Otp
    {
        public Guid UserId { get; set; }
        public int Code { get; set; }
        public DateTime Timestamp { get; set; }
        [NotMapped]
        public int Validity { get; set; }
    }
}