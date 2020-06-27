using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Entities
{
    [Table("TraficSign")]
    public class TraficSign
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
        public TraficSignType Type { get; set; }
    }
    public enum TraficSignType
    {
        Cam = 1,
        Hieulenh = 2, 
        Nguyhiem = 3,
        Phu = 4,
        Chidan = 5, 
        Vachkeduong = 6,
        Duongcaotoc = 7,
        Duongdoingoai = 8
    }
}
