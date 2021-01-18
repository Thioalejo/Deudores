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
	public partial class DeudaTotalPage : ContentPage
	{
		public DeudaTotalPage ()
		{
			InitializeComponent ();
            LoadItems();
        }

        double valortotalDuedores;
        private async void LoadItems()
        {
            var items = await App.Context.GetItemAsync();
         
            foreach (var item in items)
            {
                valortotalDuedores = valortotalDuedores + item.ValorDeuda;
            }

            lblTotalDeudores.Text = Convert.ToString(valortotalDuedores);
        }
    }
}