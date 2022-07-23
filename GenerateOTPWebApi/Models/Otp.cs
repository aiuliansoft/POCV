using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Otp
    {
        public Guid UserId { get; init; }
        public int Code { get; init; }
        public DateTime Timestamp { get; init; }
        [NotMapped]
        public int Validity { get; init; }
    }
}