using System.Net;
using System.Runtime.Intrinsics.Arm;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace kantor
{
    public partial class MainPage : ContentPage
    {

        public class Currency
        {

            public string? table { get; set; }
            public string? currency { get; set; }
            public string? code { get; set; }
            public IList<Rate> rates { get; set; }


        }


        public class Rate
        {
            public string? no { get; set; }
            public string? effectiveDate { get; set; }
            public double? bid { get; set; }
            public double? ask { get; set; }
        }


        public MainPage()
        {
            InitializeComponent();
        }
        /*
         nazwa funkcji: OnCounterClicked
         parametry wejściowe: sender, EventArgs
         wartośc zwracana: brak
         informacje: Pobiera wartosci walut z api, datę, pobiera tą wartość i zapisuje w stringu, parsuje tekst przez wartości JSONa do specyficznego dla niego parametru.
         autor: Jakub
        */
        private void OnCounterClicked(object sender, EventArgs e)
        {
            string date = dpData.Date.ToString("yyyy-MM-dd");

            string usd = "https://api.nbp.pl/api/exchangerates/rates/c/usd/" + date + "/?format=json";
            string gbp = "https://api.nbp.pl/api/exchangerates/rates/c/gbp/" + date + "/?format=json";
            string eur = "https://api.nbp.pl/api/exchangerates/rates/c/eur/" + date + "/?format=json";
            string usdj;
            string gbpj;
            string eurj;



            using (var webClient = new WebClient())
            {
                usdj = webClient.DownloadString(usd);
                gbpj = webClient.DownloadString(gbp);
                eurj = webClient.DownloadString(eur);
            }

            Currency usdc = JsonSerializer.Deserialize<Currency>(usdj);
            Currency gbpc = JsonSerializer.Deserialize<Currency>(gbpj);
            Currency eurc = JsonSerializer.Deserialize<Currency>(eurj);

            




        }
    }

}