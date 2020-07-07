using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using tinderBot.decode;
using System.Text.Json;
using System.IO;

namespace tinderBot
{
    public class server
    {
        public List<matchModel> getMatches()
        {
            try
            {
                WebClient client = new WebClient();
                client.BaseAddress = "http://10.0.0.215:5000";
                string data = client.DownloadString("/getMatches");
                matchDecode decode = new matchDecode();
                return decode.decode(data);
            }
            catch(Exception i)
            {
                Console.WriteLine(i);
                return null;
                
            }
        }

        public bool sendMessage(string match_id, string msg)
        {
            try
            {
                var send = new sndMsg();
                send.match_id = match_id;
                send.msg = msg;
                string info = call(null, null, "http://10.0.0.215/sendMessage", "POST", send).Result;
                Console.WriteLine(info);
                return false;
            }
            catch(Exception i)
            {
                Console.WriteLine(i);
                return false;
            }
        }

        private async Task<string> call(List<Tuple<string,string>> query, List<Tuple<string,string>> headers, string endpoint, string type, object body = null)
        {
            try
            {
                HttpClient client = new HttpClient();
                if(query != null)
                {
                    endpoint = endpoint + "?";
                    foreach(var q in query)
                    {
                        endpoint = endpoint + q.Item1 + "=" + q.Item2 + "&";
                    }
                    endpoint = endpoint.Substring(0, endpoint.Length - 1);
                }

                if(headers != null)
                {
                    foreach(var head in headers)
                    {
                        client.DefaultRequestHeaders.Add(head.Item1, head.Item2);
                    }
                }

                HttpResponseMessage response = null;
                if(type == "GET")
                {
                    response = client.GetAsync(endpoint).Result;
                }

                if(type == "POST")
                {
                    var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
                    response = await client.PostAsync(endpoint, content);
                }

                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    var status = response.StatusCode;
                    if(status == HttpStatusCode.NotFound)
                    {
                        return "404 not found";
                    }

                    if(status == HttpStatusCode.BadRequest)
                    {
                        return "400 bad request";
                    }
                }
                return null;
            }
            catch(Exception i)
            {
                Console.WriteLine(i);
                return null;
            }
        }
    }

    public class sndMsg
    {
        public string match_id;
        public string msg;
    }
}
