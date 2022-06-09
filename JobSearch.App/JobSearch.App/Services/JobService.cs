using JobSearch.App.Models;
using JobSearch.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JobSearch.App.Services
{
    public class JobService : Service
    {
        public async Task<ResponseService<List<Job>>> GetJobs(string word, string cityState, int numberOfPage = 1)
        {
            HttpResponseMessage response = await _client.GetAsync($"{BaseApiUrl}/api/jobs?word={word}" +
                                                $"&cityState={cityState}&numberOfPage={numberOfPage}");

            ResponseService<List<Job>> responseService = new ResponseService<List<Job>>();
            responseService.IsSucess = response.IsSuccessStatusCode;
            responseService.StatusCode = (int)response.StatusCode;


            if (response.IsSuccessStatusCode)
            {
                //Define 'job' como resultado da request 
                responseService.Data = await response.Content.ReadAsAsync<List<Job>>();

                var pagination = new Pagination()
                {
                    IsPagination = true,
                    TotalItems = int.Parse(response.Headers.GetValues("X-Total-Items").FirstOrDefault())
                };
                responseService.Pagination = pagination;

            }
            else
            {
                String problemResponse = await response.Content.ReadAsStringAsync();
                var errors = JsonConvert.DeserializeObject<ResponseService<List<Job>>>(problemResponse);




                responseService.Errors = errors.Errors;
            }

            //retorna usuário nulo ou resultado da request
            return responseService;

        }

        public async Task<ResponseService<Job>> GetJob(int id)
        {
            // Registra a response gerada pelo método get feito no enderço indicado
            HttpResponseMessage response = await _client.GetAsync($"{BaseApiUrl}/api/Job");

            ResponseService<Job> responseService = new ResponseService<Job>();
            responseService.IsSucess = response.IsSuccessStatusCode;
            responseService.StatusCode = (int)response.StatusCode;


            if (response.IsSuccessStatusCode)
            {
                //Define 'job' como resultado da request 
                responseService.Data = await response.Content.ReadAsAsync<Job>();

            }
            else
            {
                String problemResponse = await response.Content.ReadAsStringAsync();
                var errors = JsonConvert.DeserializeObject<ResponseService<Job>>(problemResponse);

                responseService.Errors = errors.Errors;
            }
            //retorna usuário nulo ou resultado da request
            return responseService;

        }
        public async Task<ResponseService<Job>> AddJob(Job job)
        {
            // Registra a response gerada pelo método add feito através do enderço indicado e converte para Json
            HttpResponseMessage response = await _client.PostAsJsonAsync($"{BaseApiUrl}/api/jobs", job);

            ResponseService<Job> responseService = new ResponseService<Job>();
            responseService.IsSucess = response.IsSuccessStatusCode;
            responseService.StatusCode = (int)response.StatusCode;


            if (response.IsSuccessStatusCode)
            {
                //Define 'job' como resultado da request 
                responseService.Data = await response.Content.ReadAsAsync<Job>();

            }
            else
            {
                String problemResponse = await response.Content.ReadAsStringAsync();
                var errors = JsonConvert.DeserializeObject<ResponseService<Job>>(problemResponse);

                responseService.Errors = errors.Errors;
            }
            //retorna usuário nulo ou resultado da request
            return responseService;

        }
    }
}



