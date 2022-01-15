using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#nullable disable

namespace backend
{
    public partial class TblTransactionTimeSlot
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string InstrutorCode { get; set; }
        public DateTime? CreateDate { get; set; }
        public TimeSpan? TimeStart { get; set; }
        public TimeSpan? TimeEnd { get; set; }
    }

    public class GetFreetimeDto 
    {
        public string InstrutorCode { get; set; }
        public string Date { get; set; }
    }

    public class bookedTimeDto
    {
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
    }

    public partial class TblTransactionTimeSlotDto
    {
        public string InstrutorCode { get; set; }
        public string CreateDate { get; set; }
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }
    }

}
