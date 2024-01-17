using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Log
    {
        public int Id { get; set; }
        public string ActivityName { get; set; }
        public string Note { get; set; }
        public DateTime Time { get; set; }
        public long ExpGained { get; set; }
    }
}
