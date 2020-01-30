namespace FormSever
{
    static partial class Configs
    {
        public class NetConfig
        {
            public int TCPBuffersize;
            public int UDPBuffersize;

            public int UDPPort;
            public int TCPPort;

            public int TCPMaxConn;
            public int UDPMaxConn;


            public int TCPCheckSpace;
            public int UDPCheckSpace;

            public int TCPConnTimeOut;
            public int UDPConnTimeOut;
        }
    }
}
