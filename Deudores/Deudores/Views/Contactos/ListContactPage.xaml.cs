using Deudores.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Deudores.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeudoresListPage : ContentPage
    {

        public DeudoresListPage() => this.InitializeComponent();

        private async void ToolbarItem_Clicked(object sender, EventArgs e) => await this.Navigation.PushAsync((Page)new AddContactPage());

        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
   
            if (!await DisplayAlert("Confirmación", "¿Esta seguro de eliminar el contacto?", "SI", "NO"))
                return;
            if (await App.Context.DeleteItemAsync((Deudor)(sender as MenuItem).CommandParameter) != 1)
                return;
            LoadItems();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.LoadItems();
        }

        private async void LoadItems() => lista_de_deudores.ItemsSource = (IEnumerable)await App.Context.GetItemAsync();

        private void Lista_de_deudores_ItemSelected(object sender, SelectedItemChangedEventArgs e) => this.Navigation.PushAsync((Page)new DetailContactPage((Deudor)e.SelectedItem));
    }
}
