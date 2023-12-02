using System;
using System.Collections.Generic;

namespace wool
{
    internal class Editor
    {
        public void EMain(List<string> memory, string filename)
        {
            int line = 0;
            string text = memory.Count > 0 ? memory[line] : "";
            int cursor = text.Length;
            string status = ";";

            Action refreshScreen = () =>
            {
                Console.Clear();
                Graphics.WriteCentered($"Wool v1 | {filename} | {status}");

                for (int i = 0; i < memory.Count; i++)
                {
                    if (i == line)
                    {
                        Console.WriteLine(Graphics.highlight(memory[i], cursor));
                    }
                    else
                    {
                        Console.WriteLine(memory[i]);
                    }
                }
            };

            refreshScreen();

            ConsoleKeyInfo keyinfo;
            do
            {
                keyinfo = Console.ReadKey(true);
                if (keyinfo.Modifiers == ConsoleModifiers.Control && keyinfo.Key == ConsoleKey.S)
                {
                    status=";";
                    
                    using (StreamWriter writer = new StreamWriter(filename))
                    {
                        foreach(string textodesu in memory){
                            writer.WriteLine(textodesu);
                        }
                    }
                }
                else{
                    switch (keyinfo.Key)
                    {
                        case ConsoleKey.LeftArrow:
                            if (cursor > 0)
                            {
                                cursor--;
                            }
                            break;

                        case ConsoleKey.RightArrow:
                            if (cursor < text.Length)
                            {
                                cursor++;
                            }
                            break;

                        case ConsoleKey.Backspace:
                            status = "*";
                            if (cursor > 0)
                            {
                                text = text.Remove(cursor - 1, 1);
                                cursor--;
                            }
                            else if (cursor == 0 && line != 0)
                            {
                                string oldText = text;
                                memory.RemoveAt(line);
                                line--;
                                text = memory[line];
                                cursor = text.Length;
                            }
                            break;

                        case ConsoleKey.Enter:
                            status = "*";
                            string[] cutted = Graphics.cut(text, cursor);
                            memory[line] = cutted[0];
                            line++;
                            memory.Insert(line, cutted.Length > 1 ? cutted[1] : "");
                            text = memory[line];
                            cursor = 0;
                            break;

                        case ConsoleKey.DownArrow:
                            if (line < memory.Count - 1)
                            {
                                memory[line] = text;
                                line++;
                                text = memory[line];
                                cursor = text.Length;
                            }
                            break;

                        case ConsoleKey.UpArrow:
                            if (line > 0)
                            {
                                memory[line] = text;
                                line--;
                                text = memory[line];
                                cursor = text.Length;
                            }
                            break;

                        default:
                            if (keyinfo.Key != ConsoleKey.Escape && keyinfo.Key != ConsoleKey.Delete)
                            {
                                status = "*";
                                text = text.Insert(cursor, keyinfo.KeyChar.ToString());
                                cursor++;
                            }
                            break;
                    }
                }

                memory[line] = text;
                refreshScreen();
            }
            while (keyinfo.Key != ConsoleKey.Delete);
        }
    }
}
