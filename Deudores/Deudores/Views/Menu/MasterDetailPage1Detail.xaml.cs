using Deudores.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Deudores.Views.Menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailPage1Detail : ContentPage
    {
        public MasterDetailPage1Detail()
        {
            InitializeComponent();
            recuperarTotal();
        }

        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Confirmación", "¿Esta seguro de eliminar el deudor?", "SI", "NO"))
            {
                var item = (Deudor)(sender as MenuItem).CommandParameter;
                var result = await App.Context.DeleteItemAsync(item);
                if (result == 1)
                {
                    LoadItems();
                }
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadItems();
        }
        public async void recuperarTotal()
        {
            var items = await App.Context.GetItemAsync();
        }
        //double valorTotal;
        private async void LoadItems()
        {
            var items = await App.Context.GetItemAsync();
            lista_de_deudores.ItemsSource = items;
            //foreach (var item in items)
            //{
            //    item.TotalDeudores = item.TotalDeudores + item.ValorDeuda;
            //}
        }


        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DeudoresListPage());
        }

        private void Lista_de_deudores_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Navigation.PushAsync(new UpdateDeudorPage((Deudor)e.SelectedItem));
        }

        //private async void BtnArchivar_Clicked(object sender, SelectedItemChangedEventArgs e)
        //{
        //    //if (await DisplayAlert("Confirmación", "¿Esta seguro de archivar el deudor?", "SI", "NO"))
        //    //{
        //    //    var item3 = (Deudor)(sender as MenuItem).CommandParameter;

        //    //    Deudor deudor = new Deudor();
        //    //    try
        //    //    {
        //    //        deudor.Activo
        //    //        var item = new Deudor
        //    //        {
        //    //            Id = this.deudor.Id,
        //    //            Nombre = nombre.Text,
        //    //            Descripcion = descripcion.Text,

        //    //            FechaEntrega = datePiker.Date,
        //    //            Abono = this.deudor.Abono + Convert.ToDouble(Abono_1.Text),
        //    //            ValorDeuda = valorTotalMenosAbono,
        //    //            Activo = switchEstadoDeudor.IsToggled
        //    //        };

        //    //        var result = await App.Context.UpdateItemAsync(item);
        //    //        if (result == 1)
        //    //        {
        //    //            //datePiker.IsVisible = true;
        //    //            //Abono_1.IsVisible = true;
        //    //            await Navigation.PopAsync();
        //    //        }
        //    //        else
        //    //        {
        //    //            await DisplayAlert("Error", "No se pudo guardar el deudor", "Aceptar");
        //    //        }
        //    //    }
        //    //    catch (Exception ex)
        //    //    {

        //    //        await DisplayAlert("Error", ex.Message, "Aceptar");
        //    //    }
        //    //}
        //    //var item2 = (Deudor)(sender as MenuItem).CommandParameter;
        //    //var item = new Deudor
        //    //{
        //    //    Activo = false
        //    //};
        //    //LoadItems();
        //}

    }
}