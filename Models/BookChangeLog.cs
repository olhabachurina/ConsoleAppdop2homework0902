using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppdop2homework0902.Models
{
    public class BookChangeLog
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string PropertyName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime ChangeDateTime { get; set; }
    }
}
