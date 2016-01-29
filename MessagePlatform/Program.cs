using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using MessagePlatform;
using ToolFunction;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;

namespace MessagePlatform
{
    public class Program
    {
        /// <summary>
        /// 抛出的信息
        /// </summary>
        public static string _strTemp = "";

        static void Main(string[] args)
        {
            try
            {
                //声明一个TCP通道指定端口8085
                TcpChannel channel = new TcpChannel(8085);
                //注册通道
                ChannelServices.RegisterChannel(channel, false);
                //从哪个类，发出消息
                RemotingConfiguration.RegisterWellKnownServiceType(typeof(ToolFunction.ConsoleMessage), "ConsoleMessage", WellKnownObjectMode.SingleCall);
                System.Console.WriteLine("Server:Press Enter key to exit");
                //等待输入
                System.Console.ReadLine();
            }
            catch (Exception)
            {
                throw;
            }
           
        }

        public static string ReadLine()
        {
            string _strLine = string.Empty;
            try
            {
                using (StreamReader sr = new StreamReader(App.Default.FilePath))
                {
                    _strLine = sr.ReadLine();
                }
            }
            catch (Exception exp)
            {
                CommonFunction.WriteError("读取信息错误:" + exp.Message);
                Thread.Sleep(100);
            }
            if (_strTemp != _strLine && "" != _strLine)
            {
                _strTemp = _strLine;
                Console.WriteLine(_strLine);
            }
            return _strLine;
        }


    }
}
