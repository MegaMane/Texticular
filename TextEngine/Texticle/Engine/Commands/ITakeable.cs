﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texticle.Engine
{
    public interface ITakeable
    {
        string Take();
        string Put(string target);
    }
}
