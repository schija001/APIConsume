using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APIConsume.Entities
{
    [Table("People")]
    public class People
    {

        [Key]
        public int PeopleId { get; set; }

        [Required]
        [MaxLength(155)]
        public string FirstName { get; set; }

        [MaxLength(155)]
        public string MiddleName { get; set; }

        [Required]
        [MaxLength(155)]
        public string LastName { get; set; }


        [MaxLength(155)]
        public string PhoneNumber { get; set; } = String.Empty;

    }
}
