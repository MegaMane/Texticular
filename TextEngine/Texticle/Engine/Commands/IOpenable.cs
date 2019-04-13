﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticle.Environment;

namespace Texticle.Engine
{
    public interface IOpenable
    {
        string Open(GameObject target);
        string Close(GameObject target);
    }
}
