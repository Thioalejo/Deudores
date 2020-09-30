using Deudores.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Deudores.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddDebtorPage : ContentPage
	{
		public AddDebtorPage ()
		{
			InitializeComponent();
		}

        private async void Guardar_Clicked(object sender, EventArgs e)
        {
            if (nombre.Text == "" || descripcion.Text == "" || valorDeuda.Text== "")
            {
                await DisplayAlert("¡Advertencia!", "No debe dejar campos vacíos", "Aceptar");
            }else
            {
                try
                {
                    var item = new Deudor
                    {
                        Nombre = nombre.Text,
                        Descripcion = descripcion.Text,
                        ValorDeuda = Convert.ToDouble(valorDeuda.Text),
                        FechaEntrega = datePiker.Date
                    };

                    var result = await App.Context.InsertItemAsync(item);
                    if (result == 1)
                    {
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("Error", "No se pudo guardar el deudor", "Aceptar");
                    }
                }
                catch (Exception ex)
                {

                    await DisplayAlert("Error", ex.Message, "Aceptar");
                }
            }   
        }

        private void ValorDeuda_Unfocused(object sender, FocusEventArgs e)
        {
            if (valorDeuda.Text != null)
            {
                valorDeuda.Text = String.Format("{0:N0}", Double.Parse(valorDeuda.Text));
            } 
        }
    }
}