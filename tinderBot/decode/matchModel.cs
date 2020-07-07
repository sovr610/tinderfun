using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tinderBot.decode
{
    public class matchDecode
    {
        public List<matchModel> decode(string data)
        {
            JObject obj = JObject.Parse(data);
            var kids = obj.Children();
            List<matchModel> matches = new List<matchModel>();
            foreach(var kid in kids)
            {
                JToken t = kid;
                string dat = t.ToString();

                int one = dat.IndexOf('{');
                string newDat = dat.Substring(one);
                matchModel match = JsonConvert.DeserializeObject<matchModel>(newDat);
                matches.Add(match);
            }

            return matches;

        }
    }
    public class matchModel
    {
        public int age;
        public int avg_successRate;
        public string bio;
        public int distance;
        public int gender;
        public string last_activity_date;
        public string match_id;
        public int message_count;
        public message[] messages;
        public string name;
        public string[] photos;
    }

    public class message
    {
        public string _id;
        public string created_date;
        public string from;
        public string match_id;
        public string messageStr;
        public string sent_date;
        public string timestamp;
        public string to;
    }
}
