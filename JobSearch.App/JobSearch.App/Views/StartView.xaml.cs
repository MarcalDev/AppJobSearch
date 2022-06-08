using JobSearch.App.Models;
using JobSearch.App.Services;
using JobSearch.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JobSearch.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartView : ContentPage
    {
        private JobService _service;
        private ObservableCollection<Job> _listOfJobs;
        private SearchParams _searchParams;
        private int _listOfJobsFirstRequest;
        public StartView()
        {
            InitializeComponent();

            _service = new JobService();
        }

        private void GoVisualizer(object sender, EventArgs e)
        {
            Navigation.PushAsync(new VisualizerView());
        }

        private void GoRegisterJobView(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterJobView());
        }

        private void FocusWord(object sender, EventArgs e)
        {
            TxtWord.Focus();
        }

        private void FocusCityState(object sender, EventArgs e)
        {
            TxtCityState.Focus();
        }

        private void Logout(object sender, EventArgs e)
        {
            App.Current.Properties.Remove("User");
            App.Current.SavePropertiesAsync();
            App.Current.MainPage = new LoginView();
        }

        private async void Search(object sender, EventArgs e)
        {
            Loading.IsVisible = true;
            Loading.IsRunning = true;
            // Coleta os dados informados pelo o usuário
            string word = TxtWord.Text;
            string cityState = TxtCityState.Text;

            // Intancia um objeto e define os parametros como os dados salvos anteriormente
            _searchParams = new SearchParams() { Word = word, CityState = cityState, NumberOfPage = 1 };

            // Preenche a lista de vagas com o response do método GetJobs, usando os parametros do objeto
            ResponseService<List<Job>> responseService = await _service.GetJobs(_searchParams.Word, 
                                                         _searchParams.CityState, _searchParams.NumberOfPage);

            if (responseService.IsSucess)
            {
                // Instancia uma ObservableCollection utilizando os dados da responseService
                _listOfJobs = new ObservableCollection<Job>( responseService.Data );
                _listOfJobsFirstRequest = _listOfJobs.Count();
                // preenche a CollectionView com as instancias
                ListOfJobs.ItemsSource = _listOfJobs;
                ListOfJobs.RemainingItemsThreshold = 1;
            }
            else
            {
                await DisplayAlert("Erro!", "Oops! Ocorreu um erro inesperado! Tente novamente mais tarde.", "OK");

            }
            Loading.IsVisible = false;
            Loading.IsRunning = false;
        }

        private async void InfinitySearch(object sender, EventArgs e)
        {
            ListOfJobs.RemainingItemsThreshold = -1;
            _searchParams.NumberOfPage++;

            ResponseService<List<Job>> responseService = await _service.GetJobs(_searchParams.Word, _searchParams.CityState, _searchParams.NumberOfPage);

            if (responseService.IsSucess)
            {
                // Colocar dentro da CollectionView
                var listOfJobsFromService = responseService.Data;
                foreach (var item in listOfJobsFromService)
                {
                    _listOfJobs.Add(item);
                }
                if(_listOfJobsFirstRequest == listOfJobsFromService.Count)
                {
                    ListOfJobs.RemainingItemsThreshold = 1;
                }
                else
                {
                    ListOfJobs.RemainingItemsThreshold = -1;
                }
                
            }
            else
            {
                await DisplayAlert("Erro!", "Oops! Ocorreu um erro inesperado! Tente novamente mais tarde.", "OK");

            }
            if(_listOfJobs.Count == 0)
            {
                NoResult.IsVisible = true;
            }
            else
            {
                NoResult.IsVisible = false;
            }

            ListOfJobs.RemainingItemsThreshold = 0;
        }
    }
}