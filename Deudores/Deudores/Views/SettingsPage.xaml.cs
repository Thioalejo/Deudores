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
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            if(switchActivarClave.IsEnabled)
            {
                stackLayoutClaveDeAcceso.IsVisible = true;
            }else if(!switchActivarClave.IsEnabled)
                {
                stackLayoutClaveDeAcceso.IsVisible = false;
            }
            
        }
    }
}