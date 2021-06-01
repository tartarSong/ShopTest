using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreLib.action
{
    public interface ICartActionInterface
    {
        double GetTotal(string connectionString);
    }
}
