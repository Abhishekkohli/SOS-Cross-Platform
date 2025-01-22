using Xamarin.Forms;
using Plugin.Messaging;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace EmergencyApp
{
    public partial class MainPage : ContentPage
    {
        private readonly string ApiUrl = "http://localhost:5000/api/contacts";

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnSendSOS(object sender, EventArgs e)
        {
            string message = MessageEntry.Text;
            string location = GetLocation();

            var contacts = await GetContactsFromApi("user123"); // Replace with actual user ID
            foreach (var contact in contacts)
            {
                SendSms(contact.Phone, $"{message}. Location: {location}");
            }

            await DisplayAlert("Success", "SOS sent to all contacts!", "OK");
        }

        private string GetLocation()
        {
            // Mock location for simplicity
            return "Latitude: 37.7749, Longitude: -122.4194";
        }

        private void SendSms(string phoneNumber, string message)
        {
            var smsMessenger = CrossMessaging.Current.SmsMessenger;
            if (smsMessenger.CanSendSms)
            {
                smsMessenger.SendSms(phoneNumber, message);
            }
        }

        private async Task<List<Contact>> GetContactsFromApi(string userId)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync($"{ApiUrl}/{userId}");
                return JsonConvert.DeserializeObject<List<Contact>>(response);
            }
        }
    }

    public class Contact
    {
        public string Name { get; set; }
        public string Phone { get; set; }
    }
}
