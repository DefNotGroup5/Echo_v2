using Domain.Shopping.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Account.LogicInterfaces;
using GrpcClientServices;
using AdminService = GrpcClientServices.Services.AdminService;
using UsersService = GrpcClientServices.Services.UsersService;

namespace Application.Account.Logic
{
    public class AdminLogic : IAdminLogic
    {
        private readonly AdminService _adminService;

        public AdminLogic(AdminService adminService)
        {
            _adminService = adminService;
            
        }

        public async Task<ICollection<User?>> ListSellersAsync()
        {
            return await _adminService.ListSellersAsync();
        }

        public async Task AuthorizeSellerAsync(int id, bool authorizationState)
        {
            await _adminService.AuthorizeSellerAsync(id, authorizationState);
        }
    }
}