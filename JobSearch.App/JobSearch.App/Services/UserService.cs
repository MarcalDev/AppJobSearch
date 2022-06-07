using JobSearch.App.Models;
using JobSearch.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JobSearch.App.Services
{
    public  class UserService : Service
    {
        public async  Task<ResponseService<User>> GetUser(string email, string password)
        {
           // Registra a response gerada pelo método get feito no enderço indicado
            HttpResponseMessage response = await _client.GetAsync($"{BaseApiUrl}/api/Users?email={email}&password={password}");

            ResponseService<User> responseService = new ResponseService<User>();
            responseService.IsSucess = response.IsSuccessStatusCode;
            responseService.StatusCode = (int)response.StatusCode;
            

            if (response.IsSuccessStatusCode)
            {                
                //Define 'user' como resultado da request 
                responseService.Data = await response.Content.ReadAsAsync<User>();

            }
            else
            {
                String problemResponse = await response.Content.ReadAsStringAsync();
                var errors = JsonConvert.DeserializeObject<ResponseService<User>>(problemResponse);
                
                responseService.Errors = errors.Errors ;
            }
            //retorna usuário nulo ou resultado da request
            return responseService;
        }
        public async Task<ResponseService<User>> AddUser(User user)
        {
            // Registra a response gerada pelo método add feito através do enderço indicado e converte para Json
            HttpResponseMessage response = await _client.PostAsJsonAsync($"{BaseApiUrl}/api/Users", user);

            ResponseService<User> responseService = new ResponseService<User>();
            responseService.IsSucess = response.IsSuccessStatusCode;
            responseService.StatusCode = (int)response.StatusCode;


            if (response.IsSuccessStatusCode)
            {
                //Define 'user' como resultado da request 
                responseService.Data = await response.Content.ReadAsAsync<User>();

            }
            else
            {
                String problemResponse = await response.Content.ReadAsStringAsync();
                var errors = JsonConvert.DeserializeObject<ResponseService<User>>(problemResponse);

                responseService.Errors = errors.Errors;
            }
            //retorna usuário nulo ou resultado da request
            return responseService;
        }
    }
}
