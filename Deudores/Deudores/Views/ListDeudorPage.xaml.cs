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
	public partial class ListDeudorPage : ContentPage
	{
        private Deudor deudor;
        public ListDeudorPage(Deudor deudor)
		{
			InitializeComponent ();
            this.deudor = deudor;
            nombre.Text = deudor.Nombre;
            descripcion.Text = deudor.Descripcion;
            valorDeuda.Text = deudor.ValorDeuda.ToString();
            datePiker.Date = deudor.FechaEntrega;

            if(Convert.ToDouble(valorDeuda.Text) > 0)
            {
                valorDeuda.IsEnabled = false;
            }

            if (Convert.ToDouble(valorDeuda.Text) == 0)
            {
                this.deudor.Abono = 0;
                //Abono_1.IsVisible = false;
                //datePiker.IsVisible = false;
            }
        }

        private async void Actualizar_Clicked(object sender, EventArgs e)
        {
            if (nombre.Text == "" || descripcion.Text == "" || valorDeuda.Text == "" || Abono_1.Text == "")
            {
                await DisplayAlert("¡Advertencia!", "No debe dejar campos vacíos", "Aceptar");
            }
            else
            {
                Double valorTotalMenosAbono = Convert.ToDouble(valorDeuda.Text) - Convert.ToDouble(Abono_1.Text);
                if (valorTotalMenosAbono < 0)
                {
                    await DisplayAlert("Alerta", "¡Estas ingresando mas dinero que la deuda total!", "Aceptar");
                }
                else
                {

                    try
                    {
                        var item = new Deudor
                        {
                            Id = this.deudor.Id,
                            Nombre = nombre.Text,
                            Descripcion = descripcion.Text,

                            FechaEntrega = datePiker.Date,
                            Abono = this.deudor.Abono + Convert.ToDouble(Abono_1.Text),
                            ValorDeuda = valorTotalMenosAbono,
                        };

                        var result = await App.Context.UpdateItemAsync(item);
                        if (result == 1)
                        {
                            //datePiker.IsVisible = true;
                            //Abono_1.IsVisible = true;
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
            
                   
            
        }

        private void ValorDeuda_Unfocused(object sender, FocusEventArgs e)
        {
            if (valorDeuda.Text != null && valorDeuda.Text !="")
            {
                valorDeuda.Text = String.Format("{0:N0}", Double.Parse(valorDeuda.Text));
            }
        }

        private void Abono_1_Unfocused(object sender, FocusEventArgs e)
        {
            if (Abono_1.Text != null && valorDeuda.Text != "")
            {
                Abono_1.Text = String.Format("{0:N0}", Double.Parse(Abono_1.Text));
            }
        }
    }
}