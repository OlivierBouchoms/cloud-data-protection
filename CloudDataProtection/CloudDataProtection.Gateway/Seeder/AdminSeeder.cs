using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CloudDataProtection.Business;
using CloudDataProtection.Core.Messaging;
using CloudDataProtection.Core.Result;
using CloudDataProtection.Dto.Result;
using CloudDataProtection.Entities;
using Microsoft.Extensions.Options;

namespace CloudDataProtection.Seeder
{
    public class AdminSeeder
    {
        private readonly AuthenticationBusinessLogic _logic;
        private readonly IMessagePublisher<AdminSeededModel> _messagePublisher;
        private readonly AdminSeederOptions _options;

        public AdminSeeder(AuthenticationBusinessLogic logic, IMessagePublisher<AdminSeededModel> publisher, IOptions<AdminSeederOptions> options)
        {
            _logic = logic;
            _messagePublisher = publisher;
            _options = options.Value;
        }

        public async Task Seed()
        {
            BusinessResult<ICollection<User>> getResult = await _logic.GetAll(UserRole.Admin);

            if (!getResult.Success)
            {
                throw new Exception("An error occurred while attempting to seed the admin user");
            }

            if (getResult.Data.Count > 0)
            {
                return;
            }

            User adminUser = new User
            {
                Email = _options.Email,
                Role = UserRole.Admin
            };

            BusinessResult<User> businessResult = await _logic.Create(adminUser);

            if (businessResult.Success)
            {
                AdminSeededModel seededModel = new AdminSeededModel
                {
                    Email = adminUser.Email,
                    Id = adminUser.Id,
                    Url = "https://example.com",
                    Expiration = DateTime.Today.AddMonths(1)
                };

                await _messagePublisher.Send(seededModel);
            }
        }
    }
}