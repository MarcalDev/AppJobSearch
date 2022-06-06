using JobSearch.Domain.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JobSearch.App.Services
{
    public  class UserService : Service
    {
        public async  Task<User> GetUser(string email, string password)
        {
           // Registra a response gerada pelo método get feito no enderço indicado
            HttpResponseMessage response = await _client.GetAsync($"{BaseApiUrl}/api/Users?email={email}&password={password}");

            User user = null;
            if (response.IsSuccessStatusCode)
            {                
                //Define 'user' como resultado da request 
                user = await response.Content.ReadAsAsync<User>();
                
            }
            //retorna usuário nulo ou resultado da request
            return user;
        }
        public async Task<User> AddUser(User user)
        {
            // Registra a response gerada pelo método add feito através do enderço indicado e converte para Json
            HttpResponseMessage response = await _client.PostAsJsonAsync($"{BaseApiUrl}/api/Users", user);

            if (response.IsSuccessStatusCode)
            {
                user = await response.Content.ReadAsAsync<User>();
            }
            else
            {
                user = null;
            }
            //retorna usuário nulo ou resultado da request
            return user;
        }
    }
}
