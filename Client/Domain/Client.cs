using Client.Exceptions;
using Client.Models;
using Client.Responses;
using LanguageExt.Common;
using System.Text;
using System.Text.Json;

namespace Client.Domain;

internal class Client
{
    public async Task<Result<AccessToken>> RegisterAccountAsync(RegistrationRequest account, Uri uri)
    {
        using(var httpClient = new HttpClient())
        {
            var response = await MakePostRequestAsync<RegistrationRequest>(httpClient, uri, account);
            var authenticationResult = DeserializeJson<AuthenticationResponse>(response);

            if (authenticationResult.access_token == null)
                return new Result<AccessToken>(new AuthorizationException(authenticationResult.errors));

            return new Result<AccessToken>(new AccessToken { access_token = authenticationResult.access_token });
        }
    }
    public async Task<Result<AccessToken>> LoginAsync(LoginRequest request, Uri uri)
    {
        using (var httpClient = new HttpClient())
        {
            var response = await MakePostRequestAsync<LoginRequest>(httpClient, uri, request);
            var authenticationResult = DeserializeJson<AuthenticationResponse>(response);

            if (authenticationResult.access_token == null)
                return new Result<AccessToken>(new AuthorizationException(authenticationResult.errors));

            return new Result<AccessToken>(new AccessToken { access_token = authenticationResult.access_token });
        }
    }
     
    private T DeserializeJson<T>(string json)
    {
        var deserializedObject = JsonSerializer.Deserialize<T>(json);
        if (deserializedObject == null)
            throw new NullReferenceException();

        return deserializedObject;
    }

    private static async Task<string> MakePostRequestAsync<T>(HttpClient httpClient, Uri uri, T request)
    {
        var json = JsonSerializer.Serialize<T>(request);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(uri, data);

        var result = await response.Content.ReadAsStringAsync();
        return result;
    }
}
