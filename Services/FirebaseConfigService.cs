using System.Net.Http.Json;

namespace GospelReachCapstone.Services
{
    public class FirebaseConfigService
    {
        public FirebaseConfig Config { get; private set; }

        public async Task LoadConfig(HttpClient http)
        {
            Config = await http.GetFromJsonAsync<FirebaseConfig>("firebaseConfig.json");
        }
    }


    public class FirebaseConfig
    {
        public string apiKey { get; set; }
        public string authDomain { get; set; }
        public string projectId { get; set; }
        public string storageBucket { get; set; }
        public string messagingSenderId { get; set; }
        public string appId { get; set; }
        public string measurementId { get; set; }
    }
}
