using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Licenta.Models
{
    public class MemberMeeting
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public int MemberId { get; set; }
        public MemberModel Member { get; set; }

        public int MeetingId { get; set; }
        public Meeting Meeting { get; set; }
    }
}
