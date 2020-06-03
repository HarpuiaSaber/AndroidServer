using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Entities
{
    [Table("Law")]
    public class Law
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }
        public string Content { get; set; }
        public LawType Type { get; set; }
        public string Punishment { get; set; }
    }

    public enum LawType
    {
        Tocdo = 1,
        Nongdo = 2,
        Dangky = 3,
        Dungcho = 4,
        Vuotxe = 5,
        Chuyenhuong = 6,
        Tinhieu = 7,
        Chonguoivahang= 8
    }
}
 