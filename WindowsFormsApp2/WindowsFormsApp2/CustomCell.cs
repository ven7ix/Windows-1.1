using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    internal class CustomCell
    {
        public string _data { get; set; }
        public bool _canBeOpened { get; set; }
        public bool _isOpen { get; set; }
        public bool _gotCalled { get; set; }
        public List<CustomCell> _dependentCells { get; set; }
        public CustomCell _parent { get; set; }
        public bool _isShown { get; set; }

        public CustomCell(string data, bool canBeOpened, CustomCell parent)
        {
            _data = data;
            _canBeOpened = canBeOpened;
            _isOpen = false;
            _gotCalled = false;
            _dependentCells = null;
            _parent = parent;
            _isShown = false;
        }
    }
}
