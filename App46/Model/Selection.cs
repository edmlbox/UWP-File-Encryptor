using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App46.Model
{
    public class Selection
    {
        public bool _RemoveAll { get; set; } = false;
        public bool _RemoveSelected { get; set; } = false;
        public bool _SelectMultiply { get; set; } = false;
        public bool _SelectAll { get; set; } = false;
        public bool _DeselectAll { get; set; } = false;
    }
}
