using Appliction.Common.Response;
using Appliction.Interfaces.Common;

namespace Appliction.Services
{
    public class SateService : IstateService
    {
        private readonly IStateRepositry _stateRepositry;
        public SateService(IStateRepositry stateRepositry)
        {
            _stateRepositry = stateRepositry; 
        }
       
        //public async Task<List<State>> GetAllStatesAsync()
        //{
        //   return await _stateRepositry.GetAllStatesAsync();
        //}

        public async Task<List<State>> GetAllStatesAsync()
        {
            return await _stateRepositry.GetAllStatesAsync();
        }
    }
    }

