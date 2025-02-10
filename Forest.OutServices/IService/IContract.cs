using Forest.OutServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forest.OutServices.IService
{
    public interface IContract
    {
        Style[] GetRecordStyles();
    }
}
