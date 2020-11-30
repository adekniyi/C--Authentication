using AspNetIdentityDemo.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AspNetIdentityDemo.Client
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        public Login()
        {
            this.InitializeComponent();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            var model = new LoginViewModel
            {
                Email = txtEmail.Text,
                Password = txtPassword.Password,
            };

            var JsonDate = JsonConvert.SerializeObject(model);

            var Content = new StringContent(JsonDate, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://localhost:7313/api/auth/login", Content);

            var responseBody = await response.Content.ReadAsStringAsync();

            var responseObject = JsonConvert.DeserializeObject<UserManagerResponse>(responseBody);

            if (responseObject.IsSuccess)
            {
                Frame.Navigate(typeof(MainPage), responseObject.Message);
            }

            else
            {
                var dialog = new MessageDialog(responseObject.Message);
                await dialog.ShowAsync();
            }
        }
    }
}
