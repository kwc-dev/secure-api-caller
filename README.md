# secure-api-caller
**secure-api-caller** is a lightweight .NET minimal API designed to demonstrate how to perform secure outbound HTTP calls using modern .NET patterns. 
It integrates seamlessly with Azure Key Vault for secret management, uses HttpClientFactory for resilient HTTP communication, 
and includes a custom authentication handler and typed client for clean, testable API access. 
The project also uses webhook.site to make it easy to inspect outbound requests during development.

This project serves as a reference implementation for building cloud‑ready, secure API callers in .NET.

## 🚀 Features
- Secure outbound HTTP calls
- Automatic secret loading from Key Vault
- Authentication via custom handler
- Typed HttpClient for clean API calls
- Testable architecture with mocked HTTP pipeline

## 🧱 Key Components
- 🏗️ Program.cs — DI setup, Key Vault wiring, endpoint registration
- 🧩 AuthHeaderHandler — adds Bearer token from configuration
- 🌐 WebhookApiClient — typed client for outbound calls
- 🔐 Key Vault Integration — configuration provider setup
- 🧪 Unit Tests — Moq‑based tests for handler + client

## 🛠️ Prerequisites
- .NET 8 SDK
- Azure Key Vault
- An Azure identity capable of accessing the vault (Managed Identity or Azure CLI login)
- webhook.site URL (or any similar request‑inspection service) for testing

## 🔧 Configuration
1. Add your Key Vault URL to appsettings.json
```json
{
  "KeyVaultUrl": "https://YOUR-VAULT-NAME.vault.azure.net/"
}
```

2. Store a secret in Key Vault 🔐

| Name        | Value                                  |
|--------------|------------------------------------------|
| `api-token-kwc `| `your-test-token-here-kw-cheung`|

## ▶️ Running the Project
1. Start the application:
```
dotnet run
```
2. Call the test endpoint:
```
GET https://localhost:7140/call-webhook
```
3. Check webhook.site to see the incoming request.
