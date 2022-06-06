﻿using JobSearch.App.Services;
using JobSearch.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JobSearch.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {
        private UserService _service;
        public LoginView()
        {
            InitializeComponent();

            _service = new UserService();
        }

        private void GoRegister(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterView());
        }

        private async void GoStart(object sender, EventArgs e)
        {
            string email = TxtEmail.Text;
            string password = TxtPassword.Text;

            // Faz uma require (Método GetUser) com os parametros informados
            User user = await _service.GetUser(email, password);

            if(user == null)
            {
                await DisplayAlert("Erro!", "Nenhum usuários encontrado", "OK");
            }
            else
            {
                // Serialização de objeto com informações de usuário e retorna através de Json
                App.Current.Properties.Add("User", JsonConvert.SerializeObject(user));
                App.Current.MainPage = new NavigationPage(new StartView());

            }
        }
    }
}