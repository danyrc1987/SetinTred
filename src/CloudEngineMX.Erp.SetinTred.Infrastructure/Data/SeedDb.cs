namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Data
{
    using System;
    using System.Threading.Tasks;
    using Core.Entities;
    using Identity;
    using Interfaces;
    using Microsoft.EntityFrameworkCore.Internal;

    public class SeedDb
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;

        public SeedDb(
            DataContext dataContext,
            IUserService userService)
        {
            _dataContext = dataContext;
            _userService = userService;
        }

        public async Task SeedAsync()
        {
            await _dataContext.Database.EnsureCreatedAsync();

            await CheckRoleAsync();

            if (!_dataContext.Users.Any())
            {
                await CheckUser("Daniel", "Ramos", "danyrc1987@gmail.com", "Vero_2015");
            }
        }

        private async Task CheckUser(string nombre, string apellidos, string email, string contraseña)
        {

            var userCore = new UserCore
            {
                CreationDate = DateTime.Now,
                LastName = apellidos,
                FirstName = nombre,
                Key = Guid.NewGuid().ToString(),
                ReportId = 1006,
                IsActive = true
            };

            var user = new User
            {
                UserCore = userCore,
                Email = email,
                UserName = email,
            };


            var result = await _userService.AddUserAsync(user, contraseña);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException("No se pudo crear el super usuario");
            }

            await _userService.AddUserToRoleAsync(user, "Administrator");
            var token = await _userService.GenerateEmailConfirmationTokenAsync(user);
            await _userService.ConfirmEmailAsync(user, token);

        }

        private async Task CheckRoleAsync()
        {
            await _userService.CheckRoleAsync("Administrator");
            await _userService.CheckRoleAsync("User");
        }
    }
}
