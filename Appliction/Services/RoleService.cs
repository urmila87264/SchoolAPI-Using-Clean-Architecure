using Appliction.Interfaces.Student;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appliction.Services
{
    public class RoleService:IRolesService
    {
        private readonly IRolesRepository _rolesRepository;
        public RoleService(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
            
        }

        public async Task<List<Roles>> GetAllRolesAsync()
        {
            return await _rolesRepository.GetAllRolesAsync();
        }
    }
}
