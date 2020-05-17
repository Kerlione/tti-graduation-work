using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using tti_graduation_work.Application.Common.Interfaces;
using tti_graduation_work.Application.Common.Models;

namespace tti_graduation_work.Infrastructure.Identity
{
    public class ExternalAuthenticationService : IExternalAuthenticationService
    {
        public async Task<IExternalUserModel> AuthenticateAsync(UserModel user)
        {
            var response = SendRequestAsync(user).Result;
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ExternalUserModel>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                return null;
            }
        }

        private async Task<HttpResponseMessage> SendRequestAsync(UserModel userModel)
        {
            return await new HttpClient().PostAsync("https://services-api.tsi.lv:3001/users/authentication", new JsonContent(userModel));
        }

        private class JsonContent: StringContent
        {
            public JsonContent(object obj): base(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json") { }
        }
    }
}
