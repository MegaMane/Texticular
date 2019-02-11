using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texticular
{ 
    public class ActionResponse
    {
        public string Verb { get; set; }
        public GameObject requiredObject;
        public Action<string[]> Response ;
    }
}
