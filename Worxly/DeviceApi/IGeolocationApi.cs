using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worxly.DeviceApi
{
    public interface IGeolocationApi
    {
        Task<(double?, double?)> GetLocation();
    }
}
