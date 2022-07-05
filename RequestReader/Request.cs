using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestReader
{
    public class Request : IComparable<Request>, IRequest
    {
        private int count = 0;
        private int[] ip;
        private int port;

        public int[] IP
        {
            get
            {
                return ip;
            }

            private set
            {
                ip = value;
            }
        }
        public int Port
        {
            get
            {
                return port;
            }
            private set
            {
                port = value;
            }
        }
        public int Count { get { return count; } }
        public Request(int[] ip, int port)
        {
            this.ip = ip;
            this.port = port;
            count = 1;
        }
        public void IncreaseCount() => count++;

        public int CompareTo(Request? other)
        {
            if (this.Count > other.Count)
            {
                return 1;
            }
            else if (this.Count < other.Count)
            {
                return -1;
            }
            else // =
            {
                for (int i = 0; i < 4; i++)
                {
                    if (this.IP[i] > other.IP[i])
                    {
                        return 1;
                    }
                    else if (this.IP[i] < other.IP[i])
                    {
                        return -1;
                    }
                }
                return 0;
            }
        }
    }
}
