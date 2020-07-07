using System;
using System.IO;
using System.Net;
using tinderBot.decode;

namespace tinderBot
{
    class Program
    {
        static void Main(string[] args)
        {

            server srv = new server();
            var matches = srv.getMatches();
            foreach(var m in matches)
            {
                Console.WriteLine("---------------------");
                Console.WriteLine(m.match_id);
                Console.WriteLine(m.name);
            }
            //File.WriteAllText(Environment.CurrentDirectory + "\\test.txt", data);
            Console.Write("enter matchID:");
            string match_id = Console.ReadLine();
            Console.Write("enter msg:");
            string msg = Console.ReadLine();

            srv.sendMessage(match_id, msg);

        }
    }

    
}
