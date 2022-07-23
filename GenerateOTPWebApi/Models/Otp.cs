using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    public class Otp
    {
        [JsonIgnore]
        public long Id { get; set; }
        public Guid UserId { get; set; }
        public int Code { get; set; }
        public DateTime Timestamp { get; set; }
        [NotMapped]
        public int Validity { get; set; }
    }
}