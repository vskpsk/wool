using System;

namespace wool
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CancelKeyPress += (sender, e) =>
            {
                Console.CursorVisible = true;
                Console.Clear();
            };
            List<string> memory = new List<string>();
            if(args.Count() > 0){
                FileStream fs = new FileStream(args[0], FileMode.OpenOrCreate);
                StreamReader sr = new StreamReader(fs);

                while(!sr.EndOfStream){
                    memory.Add(sr.ReadLine()??"");
                }
                
                if(memory.Count == 0){
                    memory.Add("");
                }
                Editor e = new Editor();
                e.EMain(memory, args[0]);
            }else{
                Console.WriteLine("Error: enter file name!");
            }

        }
    }
}