using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public static class HandlerCommand
    {
        private static bool Flag = false;
        public static void Process(string command, Core core)
        {
            Flag = false;
            foreach(var item in SD.COMMANDS)
            {
                if(item.Key == command)
                {
                    Console.WriteLine(item.Value);
                    Flag = true;
                    if(command!="help")
                    {
                        Type type = typeof(Core);
                        MethodInfo mi = type.GetMethod(item.Value);
                        mi.Invoke(core, null);
                    }
                }
            }
            if(!Flag)
            {
                Console.WriteLine("Команда не найдена, введите help что бы узнать список команд");
            }
        }
    }
}
