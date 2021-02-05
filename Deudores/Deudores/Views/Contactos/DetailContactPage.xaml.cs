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
    public partial class DetailContactPage : ContentPage
    {
        private Deudor deudor;
        public DetailContactPage(Deudor deudor)
        {
            InitializeComponent();
            this.deudor = deudor;
            this.nombre.Text = deudor.Nombre;
            this.descripcion.Text = deudor.Descripcion;
            this.valorDeuda.Text = deudor.ValorDeuda.ToString();
            this.datePiker.Date = deudor.FechaEntrega;
            this.switchEstadoDeudor.IsToggled = deudor.Activo;
            if (Convert.ToDouble(this.valorDeuda.Text) > 0)
                this.valorDeuda.IsEnabled = false;
            if (Convert.ToDouble(this.valorDeuda.Text) != 0)
                return;
            this.deudor.Abono = 0;
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
                            Activo = false
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
            if (this.valorDeuda.Text == null || !(this.valorDeuda.Text != ""))
                return;
            this.valorDeuda.Text = string.Format("{0:N0}", (object)double.Parse(this.valorDeuda.Text));
        }

        private void Abono_1_Unfocused(object sender, FocusEventArgs e)
        {
            if (this.Abono_1.Text == null || !(this.valorDeuda.Text != ""))
                this.Abono_1.Text = string.Format("{0:N0}", (object)double.Parse(this.Abono_1.Text));
        }

        private async void SwitchEstadoDeudor_Toggled(object sender, ToggledEventArgs e)
        {
            if (switchEstadoDeudor.IsToggled)
                return;
            if (await DisplayAlert("Alerta!!!", "Recuerde que cambiar el estado al deudor: " + nombre.Text + " Cambia la deuda a cero ", "Si", "No"))
            {
                valorDeuda.Text = "0";
                valorDeuda.IsEnabled = true;
            }
            else
                switchEstadoDeudor.IsToggled = true;
        }
    }
   }
