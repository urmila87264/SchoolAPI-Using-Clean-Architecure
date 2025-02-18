using Appliction.Common.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appliction.Interfaces.Common
{
    public interface IStateRepositry
    {
        Task<List<State>> GetAllStatesAsync();
    }
}
