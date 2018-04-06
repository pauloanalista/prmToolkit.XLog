using prmToolkit.XLog.SqlServer;
using System;

namespace prmToolkit.XLog.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Iniciando gravação de logs");

            var log = new Log("prmToolkit.XLog", @"Server=.\sqlexpress;Database=Log;Trusted_Connection=True;");

            //log.SaveAsync("Gravando para app XLog aaaa " , Domain.Enum.EnumMessageType.Error);


            for (int i = 0; i <= 100000; i++)
            {
                //log.Save("Gravando para app XLog aaaa " + i.ToString(), Domain.Enum.EnumMessageType.Error);
                log.SaveAsync("Gravando para app XLog aaaa " + i.ToString(), Domain.Enum.EnumMessageType.Error);
            }

            //log.Dispose();

            Console.WriteLine("---------- FIM ---------- ");

            Console.ReadKey();
        }
    }
}
