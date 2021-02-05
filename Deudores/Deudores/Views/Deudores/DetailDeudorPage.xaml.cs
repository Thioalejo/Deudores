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
	public partial class DetailDeudorPage : ContentPage
	{
        private Deudor deudor;
        public DetailDeudorPage(Deudor deudor)
		{
			InitializeComponent ();
            this.deudor = deudor;
            nombre.Text = deudor.Nombre;
            descripcion.Text = deudor.Descripcion;
            valorDeuda.Text = deudor.ValorDeuda.ToString();
            datePiker.Date = deudor.FechaEntrega;
            switchEstadoDeudor.IsToggled = deudor.Activo;

            if (Convert.ToDouble(valorDeuda.Text) > 0)
            {
                valorDeuda.IsEnabled = false;
            }

            if (Convert.ToDouble(valorDeuda.Text) == 0)
            {
                this.deudor.Abono = 0;
            }
        }

        private async void Actualizar_Clicked(object sender, EventArgs e)
        {
            if (nombre.Text == "" || descripcion.Text == "" || (valorDeuda.Text == "" || Abono_1.Text == ""))
            {
                await DisplayAlert("¡Advertencia!", "No debe dejar campos vacíos", "Aceptar");
            }
            else
            {
                double num = Convert.ToDouble(valorDeuda.Text) - Convert.ToDouble(Abono_1.Text);
                if (num < 0)
                {
                    await DisplayAlert("Alerta", "¡Estas ingresando mas dinero que la deuda total!", "Aceptar");
                }
                else
                {
                    try
                    {
                        if (num > 0)
                        {
                            if (await App.Context.UpdateItemAsync(new Deudor()
                            {
                                Id = deudor.Id,
                                Nombre = nombre.Text,
                                Descripcion = descripcion.Text,
                                FechaEntrega = datePiker.Date,
                                Abono = deudor.Abono + Convert.ToDouble(Abono_1.Text),
                                ValorDeuda = num,
                                Activo = true
                            }) == 1)
                            {
                                Page page1 = await Navigation.PopAsync();
                            }
                            else
                                await DisplayAlert("Error", "No se pudo guardar el deudor", "Aceptar");
                        }
                        else if (await App.Context.UpdateItemAsync(new Deudor()
                        {
                            Id = deudor.Id,
                            Nombre = nombre.Text,
                            Descripcion = descripcion.Text,
                            FechaEntrega = datePiker.Date,
                            Abono = deudor.Abono + Convert.ToDouble(Abono_1.Text),
                            ValorDeuda = num,
                            Activo = switchEstadoDeudor.IsToggled
                        }) == 1)
                        {
                            Page page2 = await Navigation.PopAsync();
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