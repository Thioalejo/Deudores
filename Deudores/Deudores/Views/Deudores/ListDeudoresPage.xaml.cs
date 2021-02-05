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
    public partial class ListDeudoresPage : ContentPage
    {
        private double valorTotal;
        public ListDeudoresPage()
        {
            this.recuperarTotal();
        }

        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
 
            if (!await DisplayAlert("Confirmación", "¿Esta seguro de eliminar el deudor?", "SI", "NO"))
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

        public async void recuperarTotal()
        {
            List<Deudor> itemAsync = await App.Context.GetItemAsync();
            double valorTotal = this.valorTotal;
        }

        private async void LoadItems()
        {
            List<Deudor> itemAsync = await App.Context.GetItemActiveAsync();
            lista_de_deudores.ItemsSource = (IEnumerable)itemAsync;
            foreach (Deudor deudor in itemAsync)
                this.valorTotal += deudor.ValorDeuda;
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e) => await this.Navigation.PushAsync((Page)new DeudoresListPage());

        private void Lista_de_deudores_ItemSelected(object sender, SelectedItemChangedEventArgs e) => this.Navigation.PushAsync((Page)new DetailContactPage((Deudor)e.SelectedItem));
    }
}
