using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyNetCore.Interfaces
{
    interface IMessageHelper
    {
        bool SendIntercom(string message);
    }
}
