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
        Chidan = 3,
        Phu = 4,
        Nguyhien = 5,
        Canhbao = 6,
    }
}
