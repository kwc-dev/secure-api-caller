namespace SecureApiCaller
{
    public class WebhookApiClient
    {
        private readonly HttpClient _http;

        public WebhookApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<string> SendTestRequestAsync()
        {
            var response = await _http.PostAsync(
                "https://webhook.site/77fb741f-3e4a-4987-bf44-8d23c910a033",
                new StringContent("{\"message\":\"Hello from .NET\"}",
                    System.Text.Encoding.UTF8,
                    "application/json"));

            return await response.Content.ReadAsStringAsync();
        }
    }
}
