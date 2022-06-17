// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace IdentityServer.Client
{
    
    public class Program
    {
        private static async Task Main()
        {
            var identityUrl = "https://localhost:44301/";//"http://35.205.187.151:100";//"http://localhost:65341"//"https://ahmetcagriakca.com"
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync(identityUrl);
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            // request token
            var tokenreq = new PasswordTokenRequest()
            {
                ClientId = "client",
                UserName = "admin",
                Password = "admin",
                Scope = "api1",
                ClientSecret = "secret",
                Address = disco.TokenEndpoint,
                GrantType = "refresh_token"
            };

            var tokenResponse = await client.RequestPasswordTokenAsync(tokenreq);
            var refreshTokenReq = new RefreshTokenRequest()
            {
                ClientId = "client",
                Scope = "api1",
                ClientSecret = "secret",
                Address = disco.TokenEndpoint,
                GrantType = "refresh_token",
            };

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                Console.ReadLine();
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");

            var client1 = new HttpClient();
            client1.SetBearerToken(tokenResponse.AccessToken);

            var res = client1.GetAsync(disco.UserInfoEndpoint).Result;
            var claims = res.Content;
            // call api
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);
            var refresh = tokenResponse.RefreshToken;
            var userTokenResponse = await client.GetUserInfoAsync(new UserInfoRequest()
            {
                Address = disco.UserInfoEndpoint,
                Token = tokenResponse.AccessToken
            });
            try
            {
                 var responseServer = await apiClient.GetAsync(identityUrl +"api/Account/Show/5");
                if (!responseServer.IsSuccessStatusCode)
                {
                    Console.WriteLine(responseServer.StatusCode);
                }
                else
                {
                    var content = await responseServer.Content.ReadAsStringAsync();
                    Console.WriteLine(JObject.Parse(content));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
    }
}