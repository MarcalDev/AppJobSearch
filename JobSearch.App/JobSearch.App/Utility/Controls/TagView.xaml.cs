using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JobSearch.App.Utility.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TagView : ContentView
    {
        // Cria uma propriedade para o componente, por onde serão passados dados
        public static readonly BindableProperty TechonologiesProperty = BindableProperty.Create("Techonologies", typeof(string), typeof(TagView));
        public string Techonologies
        {
            get => (string)GetValue(TagView.TechonologiesProperty);
            set => SetValue(TagView.TechonologiesProperty, value);
        }
        public static readonly BindableProperty NumberOfWordsProperty = BindableProperty.Create("NumberOfWords", typeof(int), typeof(TagView));

        public int NumberOfWords
        {
            get => (int)GetValue(TagView.NumberOfWordsProperty);
            set => SetValue(TagView.NumberOfWordsProperty, value);
        }

        public TagView()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if(propertyName == "Techonologies")
            {
                if(Techonologies != null)
                {
                    // Limpar ao decorrer da lista
                    Container.Children.Clear();

                    // Separar os caracteres entre vírgula
                    string[] words = Techonologies.Split(',');

                    // Define o limite de tags para cada item, compara com o número de linhas informado no xaml
                    int limit = (words.Count() >= NumberOfWords) ? NumberOfWords : words.Count();

                    for (int i =0; i< limit; i++)
                    {
                        // Definindo as propriedades dos componentes pelo cs
                        var frame = new Frame() { BackgroundColor = Color.FromHex("#F7F8FA"), Padding = new Thickness(3), HasShadow = false};
                        var label = new Label() { Text = words[i], Padding = new Thickness(3), FontFamily = "MontserratLight", FontSize = 10, 
                                                    TextColor = Color.FromHex("8D9EAA") };

                        // Content só aceita 1 componente
                        frame.Content = label;

                        // Children aceita mais de 1 componente
                        Container.Children.Add(frame);
                    }
                }

            }
            base.OnPropertyChanged(propertyName);
        }
    }
}