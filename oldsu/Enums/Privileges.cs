using System;
using System.Collections.Generic;
using System.Text;

namespace Oldsu.Enums
{
    [Flags]
    public enum Privileges
    {
        Normal = 1,
        GMT = 2,
        Supporter = 4,
        Developer = 8
    }

}
