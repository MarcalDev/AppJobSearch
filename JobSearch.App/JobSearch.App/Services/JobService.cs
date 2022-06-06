using JobSearch.Domain.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JobSearch.App.Services
{
    public class JobService : Service
    {
        public async Task<IEnumerable<Job>> GetJobs(string word, string cityState, int numberOfPage = 1)
        {
            // Registra a response gerada pelo método get feito no enderço indicado
            HttpResponseMessage response = await _client.GetAsync($"{BaseApiUrl}/api/jobs?word={word}" +
                                                $"&cityState={cityState}&numberOfPage={numberOfPage}");

            List<Job> list = null;
            if (response.IsSuccessStatusCode)
            {

                //Define 'list' como resultado da request 
                list = await response.Content.ReadAsAsync<List<Job>>();
            }
            return list;
        }

        public async Task<Job> GetJob(int id)
        {
            // Registra a response gerada pelo método get feito no enderço indicado
            HttpResponseMessage response = await _client.GetAsync($"{BaseApiUrl}/ap/Jobs/1");

            Job job = null;
            if (response.IsSuccessStatusCode)
            {
                //Define 'job' como resultado da request 
                job = await response.Content.ReadAsAsync<Job>();
            }
            //retorna job nulo ou resultado da request
            return job;
        }
        public async Task<Job> AddJob(Job job)
        {
            // Registra a response gerada pelo método add feito através do enderço indicado e converte para Json
            HttpResponseMessage response = await _client.PostAsJsonAsync($"{BaseApiUrl}/api/Jobs", job);

            if (response.IsSuccessStatusCode)
            {
                job = await response.Content.ReadAsAsync<Job>();
            }
            else
            {
                job = null;
            }
            //retorna job nulo ou resultado da request
            return job;
        }
    }
}
