using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace kantor
{
    public partial class MainPage : ContentPage
    {
        private const string ApiUrl = "https://api.coindesk.com/v1/bpi/currentprice.json";

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            using (var webClient = new WebClient())
            {
                string json = webClient.DownloadString(ApiUrl);
                var jsonDoc = JsonNode.Parse(json);

                double usdRate = jsonDoc["bpi"]["USD"]["rate_float"].GetValue<double>();
                double gbpRate = jsonDoc["bpi"]["GBP"]["rate_float"].GetValue<double>();
                double eurRate = jsonDoc["bpi"]["EUR"]["rate_float"].GetValue<double>();

                double usdToPlnRate = 4.00;
                double plnRate = usdRate * usdToPlnRate;

                usdLabel.Text = $"USD: {usdRate:F2}";
                gbpLabel.Text = $"GBP: {gbpRate:F2}";
                eurLabel.Text = $"EUR: {eurRate:F2}";
                plnLabel.Text = $"PLN: {plnRate:F2}";
            }
        }

        private void OnCalculateClicked(object sender, EventArgs e)
        {
            if (double.TryParse(btcEntry.Text, out double btcAmount) && btcAmount > 0)
            {
                double usdRate = double.Parse(usdLabel.Text.Split(':')[1]);
                double gbpRate = double.Parse(gbpLabel.Text.Split(':')[1]);
                double eurRate = double.Parse(eurLabel.Text.Split(':')[1]);
                double plnRate = double.Parse(plnLabel.Text.Split(':')[1]);

                usdResultLabel.Text = $"USD: {btcAmount * usdRate:F2}";
                gbpResultLabel.Text = $"GBP: {btcAmount * gbpRate:F2}";
                eurResultLabel.Text = $"EUR: {btcAmount * eurRate:F2}";
                plnResultLabel.Text = $"PLN: {btcAmount * plnRate:F2}";
            }
            else
            {
                DisplayAlert("Błąd", "Wprowadź prawidłową ilość bitcoinów.", "OK");
            }
        }
    }
}
