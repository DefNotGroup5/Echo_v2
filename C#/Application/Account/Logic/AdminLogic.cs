using Domain.Account.Models;
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
        private readonly UsersService _usersService;

        public AdminLogic(AdminService adminService, UsersService usersService)
        {
            _adminService = adminService;
            _usersService = usersService;
        }

        public async Task<IEnumerable<User>> ListSellersAsync()
        {
            var response = await _adminService.ListSellersAsync(new Google.Protobuf.WellKnownTypes.Empty());
            var users = new List<User>();
            foreach (var grpcUser in response.Users)
            {
                users.Add(ConvertGrpcUserToUser(grpcUser));
            }
            return users;
        }

        public async Task AuthorizeSellerAsync(int userId, bool isAuthorized)
        {
            var request = new AuthorizeSellerRequest
            {
                Id = userId,
                IsAuthorized = isAuthorized
            };
            await _adminService.AuthorizeSellerAsync(request);
        }

        private User ConvertGrpcUserToUser(GrpcUser grpcUser)
        {
            var user = new User(grpcUser.Email, grpcUser.Password)
            {
                Id = grpcUser.Id,
                FirstName = grpcUser.FirstName,
                LastName = grpcUser.LastName,
                Address = grpcUser.Address,
                City = grpcUser.City,
                PostalCode = grpcUser.PostalCode,
                Country = grpcUser.Country,
                IsAdmin = grpcUser.IsAdmin,
            };

            return user;
        }
    }
}