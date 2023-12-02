using System;

namespace wool
{
    class Graphics
    {
        public static string highlight(string text, int position)
        {
            string input = text+" ";

            string before = input.Substring(0, position);
            string highlighted = input.Substring(position, 1);
            string after = input.Substring(position + 1);

            string result = before + "\u001b[47;30m" + highlighted + "\u001b[0m" + after;

            return result;
        }

        public static string[] cut(string text, int index){
            string r2c = text.Insert(index, "¶");
            return r2c.Split('¶');
        }

        static  public void WriteCentered(string text)
        {
            int consoleWidth = Console.WindowWidth;
            int textLength = text.Length;
            int start = (consoleWidth - textLength) / 2;
            
            Console.SetCursorPosition(start, 0);
            Console.Write(text);
            Console.SetCursorPosition(0,1);
            Console.CursorVisible = false;
        }


    }
}