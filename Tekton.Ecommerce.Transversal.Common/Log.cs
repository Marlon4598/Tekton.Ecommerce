using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekton.Ecommerce.Transversal.Common
{
    public class Log
    {
        private readonly string endpoint;
        private readonly DateTime begin;
        private readonly DateTime end;
        private readonly string status;

        public Log(string endpoint, DateTime begin, DateTime end, string status)
        {
            this.endpoint = endpoint;
            this.begin = begin;
            this.end = end;
            this.status = status;
        }

        public void PrintLog()
        {
            var sb = new StringBuilder();

            sb.Append("/ Endpoint: " + endpoint);
            sb.Append("/ Inicio: " + begin.ToShortDateString() + "-" + begin.ToLongTimeString());
            sb.Append("/ Fin: " + end.ToShortDateString() + "-" + end.ToLongTimeString());
            sb.Append("/ Duración: " + (end - begin).TotalSeconds.ToString() + " segundos.");
            sb.Append("/ Estado: " + status);

            var id = DateTime.Now.ToString("yyyMMdd");

            TextWriter tw = new StreamWriter(@"Logs\log" + id + ".txt",true);
            tw.WriteLine(sb.ToString());
            tw.Close();
            sb.Clear();
        }
    }
}
