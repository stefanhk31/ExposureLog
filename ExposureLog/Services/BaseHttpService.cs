﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace ExposureLog.Services
{
    public abstract class BaseHttpService
    {
        protected async Task<T> SendRequestAsync<T>(
        Uri url,
        HttpMethod httpMethod = null,
        IDictionary<string, string> headers = null,
        object requestData = null)
        {
            var result = default(T);
            var method = httpMethod ?? HttpMethod.Get;
            var data = requestData == null
                ? null
                : JsonConvert.SerializeObject(requestData);
            using (var request = new HttpRequestMessage(method, url))
            {
                if (data != null)
                {
                    request.Content = new StringContent(data, Encoding.UTF8, "application/json");
                }
                if (headers != null)
                {
                    foreach (var h in headers)
                    {
                        request.Headers.Add(h.Key, h.Value);
                    }
                }

                using (var client = new HttpClient())
                using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead))
                {
                    var content = response.Content == null
                        ? null
                        : await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        result = JsonConvert.DeserializeObject<T>(content);
                    }
                    else 
                    {
                        if (response.StatusCode == HttpStatusCode.Unauthorized) 
                        { 
                            throw new UnauthorizedAccessException(); 
                        } 
                    }
                }
            }
            return result;
        }
    }
}
