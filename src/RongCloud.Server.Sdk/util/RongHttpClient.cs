﻿using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RongCloud.Server.util
{
    public class RongHttpClient
    {
        public static async Task<string> ExecuteGet(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(30);
                using (var result = await client.GetAsync(url))
                {
                    if (result.IsSuccessStatusCode)
                    {
                        return await result.Content.ReadAsStringAsync();
                    }

                    throw new HttpRequestException("RongCloud Request Error: " +
                                                   await result.Content.ReadAsStringAsync());
                }
            }
            //HttpWebRequest myRequest = WebRequest.Create(url) as HttpWebRequest;
            //myRequest.Method = "GET";
            //myRequest.ReadWriteTimeout = 30 * 1000;

            //return ReturnResult(myRequest);
        }


        public static async Task<string> ExecutePost(string appkey, string appSecret, string postStr, string methodUrl,
            string contentType)
        {
            if (string.IsNullOrWhiteSpace(methodUrl))
            {
                throw new ArgumentNullException(nameof(methodUrl));
            }

            Random rd = new Random();
            int rd_i = rd.Next();
            string nonce = Convert.ToString(rd_i);
            string timestamp = Convert.ToString(ConvertDateTimeInt(DateTime.Now));
            string signature = GetHash(appSecret + nonce + timestamp);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("App-Key", appkey);
                client.DefaultRequestHeaders.Add("Nonce", nonce);
                client.DefaultRequestHeaders.Add("Timestamp", timestamp);
                client.DefaultRequestHeaders.Add("Signature", signature);
                client.Timeout = TimeSpan.FromSeconds(30);
                HttpContent content = new ByteArrayContent(Encoding.UTF8.GetBytes(postStr));
                content.Headers.ContentType = MediaTypeHeaderValue.Parse(string.IsNullOrWhiteSpace(contentType)
                    ? "application/x-www-form-urlencoded"
                    : contentType);
                using (var result = await client.PostAsync(methodUrl, content))
                {
                    if (result.IsSuccessStatusCode)
                    {
                        return await result.Content.ReadAsStringAsync();
                    }

                    throw new HttpRequestException("RongCloud Request Error: " +
                                                   await result.Content.ReadAsStringAsync());
                }
            }

            //HttpWebRequest myRequest = (HttpWebRequest) WebRequest.Create(methodUrl);

            //myRequest.Method = "POST";
            //if (contentType == null || contentType.Equals("") || contentType.Length < 10)
            //{
            //    myRequest.ContentType = "application/x-www-form-urlencoded";
            //}
            //else
            //{
            //    myRequest.ContentType = contentType;
            //}

            //myRequest.ProtocolVersion = HttpVersion.Version10;

            //myRequest.Headers.Add("App-Key", appkey);
            //myRequest.Headers.Add("Nonce", nonce);
            //myRequest.Headers.Add("Timestamp", timestamp);
            //myRequest.Headers.Add("Signature", signature);
            //myRequest.ReadWriteTimeout = 30 * 1000;

            //byte[] data = Encoding.UTF8.GetBytes(postStr);
            //myRequest.ContentLength = data.Length;

            //Stream newStream = myRequest.GetRequestStream();

            //// Send the data.
            //newStream.Write(data, 0, data.Length);
            //newStream.Close();

            //return ReturnResult(myRequest);
        }

        /// <summary>  
        /// DateTime时间格式转换为Unix时间戳格式  
        /// </summary>  
        /// <param name="time"> DateTime时间格式</param>  
        /// <returns>Unix时间戳格式</returns>  
        public static int ConvertDateTimeInt(DateTime time)
        {
            DateTime startTime = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
            return (int) (time - startTime).TotalSeconds;
        }

        public static string GetHash(string input)
        {
            //建立SHA1对象
            SHA1 sha = new SHA1CryptoServiceProvider();

            //将mystr转换成byte[]
            UTF8Encoding enc = new UTF8Encoding();
            byte[] dataToHash = enc.GetBytes(input);

            //Hash运算
            byte[] dataHashed = sha.ComputeHash(dataToHash);

            //将运算结果转换成string
            string hash = BitConverter.ToString(dataHashed).Replace("-", "");

            return hash;
        }

        /// <summary>
        /// Certificate validation callback.
        /// </summary>
        private static bool ValidateRemoteCertificate(object sender, X509Certificate cert, X509Chain chain,
            SslPolicyErrors error)
        {
            // If the certificate is a valid, signed certificate, return true.
            if (error == SslPolicyErrors.None)
            {
                return true;
            }

            Console.WriteLine("X509Certificate [{0}] Policy Error: '{1}'",
                cert.Subject,
                error.ToString());

            return false;
        }


        public static string ReturnResult(HttpWebRequest myRequest)
        {
            HttpWebResponse myResponse;
            string content;
            try
            {
                myResponse = (HttpWebResponse) myRequest.GetResponse();
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);

                content = reader.ReadToEnd();
            }
            catch (WebException e)
            {
                //异常请求
                myResponse = (HttpWebResponse) e.Response;
                using (Stream errData = myResponse.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(errData))
                    {
                        content = reader.ReadToEnd();
                    }
                }
            }

            return content;
        }
    }
}