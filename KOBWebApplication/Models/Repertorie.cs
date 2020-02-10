using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KOBWebApplication.Models
{
    public class Repertorie
    {
        [Key]
        public Guid Id { get; set; }
        public int Index { get; set; }
        public bool IsCurrent { get; set; }
        public int? SongId { get; set; }
        public string Comments { get; set; }
    }
}
