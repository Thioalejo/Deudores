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
	public partial class AddContactPage : ContentPage
	{
		public AddContactPage()
		{
			InitializeComponent();
		}

        private async void Guardar_Clicked(object sender, EventArgs e)
        {
            if (nombre.Text == "" || descripcion.Text == "" || valorDeuda.Text == "")
            {
                await DisplayAlert("¡Advertencia!", "No debe dejar campos vacíos", "Aceptar");
            }
            else
            {
                try
                {
                    if (await App.Context.InsertItemAsync(new Deudor()
                    {
                        Nombre = nombre.Text,
                        Descripcion = descripcion.Text,
                        ValorDeuda = Convert.ToDouble(valorDeuda.Text),
                        FechaEntrega = datePiker.Date,
                        Activo = true
                    }) == 1)
                    {
                        Page page = await Navigation.PopAsync();
                    }
                    else
                        await DisplayAlert("Error", "No se pudo guardar el deudor", "Aceptar");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message, "Aceptar");
                }
            }
        }

        private void ValorDeuda_Unfocused(object sender, FocusEventArgs e)
        {
            if (this.valorDeuda.Text == null)
                return;
            this.valorDeuda.Text = string.Format("{0:N0}", (object)double.Parse(this.valorDeuda.Text));
        }

        private async void SwitchEstadoDeudor_Toggled(object sender, ToggledEventArgs e)
        {

            //if (switchEstadoDeudor.IsToggled)
            //    return;
            //if (await DisplayAlert("Alerta!!!", "Recuerde que cambiar el estado al deudor: " + nombre.Text + " Cambia la deuda a cero ", "Si", "No"))
            //{
            //    valorDeuda.Text = "0";
            //    valorDeuda.IsEnabled = true;
            //}
            //else
            //    switchEstadoDeudor.IsToggled = true;
        }
    }
}