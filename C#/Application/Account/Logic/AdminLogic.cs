using Domain.Shopping.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Account.LogicInterfaces;
using Domain.Account.Models;
using GrpcClientServices;
using AdminService = GrpcClientServices.Services.AdminService;
using UsersService = GrpcClientServices.Services.UsersService;

namespace Application.Account.Logic
{
    public class AdminLogic : IAdminLogic
    {
        private readonly AdminService _adminService;
        private readonly UsersService _usersService;
        public AdminLogic(AdminService adminService, UsersService usersService)
        {
            _adminService = adminService;
            _usersService = usersService;
        }

        public async Task<ICollection<User?>> ListSellersAsync()
        {
            try
            {
                return await _adminService.ListSellersAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task AuthorizeSellerAsync(int id, bool authorizationState)
        {
            try
            {
                User? user = await _usersService.GetByIdAsync(id);
                if (user == null)
                {
                    throw new Exception("User doesn't exist!");
                }

                if (user is not Seller)
                {
                    throw new Exception("User is not a seller!");
                }
                await _adminService.AuthorizeSellerAsync(id, authorizationState);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}