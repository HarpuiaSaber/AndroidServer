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
        Hieulenh = 1,
        Chuyenhuong = 2,
        Chonguoi = 3,
        Tocdo = 4,
        Doxe = 5,
        Giaytoxe = 6,
        Duongcam = 7,
        Nongdo= 8
    }
}
 