using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IA_V2.Core.Entities
{
    public class Text : BaseEntity
    {
        //public int Id { get; set; }
        public string Content { get; set; }
        public DateTime FechaEnvio { get; set; } = DateTime.UtcNow;

        public int? UserId { get; set; }
        public User? User { get; set; }

        public ICollection<Prediction> Predictions { get; set; } = new List<Prediction>();
    }
}
