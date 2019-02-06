using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEngine
{
    public class Sequence
    {
        private int _value;
        private int _startValue;
        private int _endValue;

        public Sequence()
        {
            _value = -1;
            _startValue = -1;
            _endValue = Int32.MaxValue;
        }

        public Sequence(int start)
        {
            _value = start;
            _startValue = start;
            _endValue = Int32.MaxValue;
        }

        public Sequence(int start, int end)
        {

            _startValue = start;
            _endValue = end;
            _value = start;
        }

        public int Next
        {
            get
            {
                if (_value == Int32.MaxValue || _value == _endValue)
                    _value = _startValue;
                return ++_value;
            }


        }
    }
}
