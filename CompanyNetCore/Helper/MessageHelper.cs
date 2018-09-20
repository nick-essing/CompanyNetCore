using Chayns.Backend.Api.Credentials;
using Chayns.Backend.Api.Models.Data;
using Chayns.Backend.Api.Repositories;
using CompanyNetCore.Interfaces;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace CompanyNetCore.Model
{
    class MessageHelper : IMessageHelper
    {
        public ChaynsApiInfo _backendApiSettings;
        public MessageHelper(IOptions<ChaynsApiInfo> backendApiSettings)
        {
            _backendApiSettings = backendApiSettings.Value;
        }
        public bool SendIntercom(string message)
        {
            var secret = new SecretCredentials(_backendApiSettings.Secret, 430009);
            var intercomRepository = new IntercomRepository(secret);
            var intercomData = new IntercomData(158750)
            {
                Message = message,
                UserIds = new List<int>
                {
                    1876100
                }
            };
            var result = intercomRepository.SendIntercomMessage(intercomData);
            return result.Status.Success;
        }
    }
}
