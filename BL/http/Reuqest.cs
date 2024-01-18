﻿using BE.Counter;
using BL.Helper;
using DB.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace BL.http
{
    public class Reuqest<T> where T : class,new()
    {
        private T Value { get; set; }
        protected virtual HttpClient _httpClient { get; set; }
        public Reuqest() 
        {
        }
        public async Task<string> PostRequest(T Model, string Url)
        {
            using (var httpClient = _httpClient ?? new HttpClient())
            {
                var convertJson = new ConvertJson<T>(Model);
                var Json = convertJson.ConverModelToJson();
                var content = new StringContent(Json, Encoding.UTF8, "application/json");
                var resultPostRequest = await httpClient.PostAsync(Url, content);
                if(resultPostRequest != null && resultPostRequest.StatusCode == HttpStatusCode.OK) 
                {
                    var result = await resultPostRequest.Content.ReadAsStringAsync();
                    return result;
                }
                throw new Exception();
                
            }
        }
        public async Task<string> PostRequestWithTocken(T Model, string Url, string Token)
        {
            using (var httpClient = _httpClient ?? new HttpClient())
            {
                var convertJson = new ConvertJson<T>(Model);
                var Json = convertJson.ConverModelToJson();
                var content = new StringContent(Json, Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "Token");
                var resultPostRequest = await httpClient.PostAsync(Url, content);
                if (resultPostRequest != null && resultPostRequest.StatusCode == HttpStatusCode.OK)
                {
                    var result = await resultPostRequest.Content.ReadAsStringAsync();
                    return result;
                }
                throw new Exception();

            }
        }
        public async Task<byte[]> GetRequestWithTockenAsync(string Url, string Token)
        {
            using (var httpClient = _httpClient ?? new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                await Task.CompletedTask;
                var resultPostRequest = await httpClient.GetAsync(Url);
                if (resultPostRequest != null && resultPostRequest.StatusCode == HttpStatusCode.OK)
                {
                    var result = await resultPostRequest.Content.ReadAsStringAsync();
                    byte[] buffer = Encoding.GetEncoding("windows-1251").GetBytes(result);
                    return buffer;
                }
                throw new Exception();

            }
        }
        public async Task<byte[]> UploadFile(string Url, string Token, byte[] fules, string FileName)
        {
            using (var httpClient = _httpClient ?? new HttpClient())
            {
                using (var form = new MultipartFormDataContent())
                {
                    var imageContent = new ByteArrayContent(fules);
                    imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

                    form.Add(imageContent, "Test", FileName);
                    var response = httpClient.PostAsync(Url, form).Result;
                    if (response != null && response.StatusCode == HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        byte[] buffer = Encoding.GetEncoding("windows-1251").GetBytes(result);
                        return buffer;
                    }
                    throw new Exception();
                }
            }
        }
    }
}
