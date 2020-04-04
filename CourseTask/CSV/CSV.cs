using System;
using System.IO;


namespace CSV
{
    class CSV
    {
        public bool endline = false;
        private static bool ParseCsvLine(StreamWriter writer, string line, bool newRow, byte end)
        {
            bool newCell;
            bool cellWithQuotes;
            bool openQuotes;

            if (newRow)
            {
                if (end >= 1)
                {
                    writer.Write("</td>");
                }
                writer.WriteLine("<tr>");
                writer.Write("<td>");
                newCell = true;
                cellWithQuotes = false;
                openQuotes = false;
            }
            else
            {
                newCell = false;
                cellWithQuotes = true;
                openQuotes = true;
                writer.Write("<br>");
            }

            foreach (char symbol in line)
            {
                switch (symbol)
                {
                    case '"':
                        if (newCell)
                        {
                            cellWithQuotes = true;
                            openQuotes = true;
                            newCell = false;
                            break;
                        }

                        if (openQuotes)
                        {
                            openQuotes = false;
                        }
                        else
                        {
                            writer.Write("\"");
                            openQuotes = true;
                        }
                        break;
                    case ',':
                        if (cellWithQuotes && openQuotes)
                        {
                            writer.Write(",");
                        }
                        else
                        {
                            newCell = true;
                            cellWithQuotes = false;
                            writer.Write("</td>");
                            writer.Write("<td>");
                        }
                        break;
                    default:
                        newCell = false;
                        switch (symbol)
                        {
                            case '<':
                                writer.Write("&lt;");
                                break;
                            case '>':
                                writer.Write("&gt;");
                                break;
                            case '&':
                                writer.Write("&amp;");
                                break;
                            default:
                                writer.Write(symbol);
                                break;
                        }

                        break;
                }
            }

            return (openQuotes == false);
        }


        class CSVProgram
        {
            static void Main(string[] args)
            {
                if (args.Length != 2)
                {
                    Console.WriteLine("Неверное количество аргументов");
                    Console.WriteLine("Использование: {0} <имя_файла.csv> <имя_файла.html>", System.Reflection.Assembly.GetExecutingAssembly().Location);
                    Console.ReadKey();

                    return;
                }

                string inputCsvFile = args[0];
                string outputHtmlFile = args[1];

                using (StreamWriter writer = new StreamWriter(outputHtmlFile))
                {
                    writer.WriteLine("<!DOCTYPE html>{0}<html>{0}<head>{0}<meta charset=\"utf-8\" />", Environment.NewLine);
                    writer.WriteLine("<title>CSV table</title>{0}</head>{0}<body>{0}<table border=\"1\">{0}<tbody>", Environment.NewLine);

                    using (StreamReader reader = new StreamReader(inputCsvFile))
                    {
                        string currentLine;
                        bool newRow = true;
                        byte i = 0;

                        while ((currentLine = reader.ReadLine()) != null)
                        {
                            newRow = ParseCsvLine(writer, currentLine, newRow, i);
                            i++;
                        }
                    }
                    writer.WriteLine("</td>", Environment.NewLine);
                    writer.WriteLine("</tbody>{0}</table>{0}</body>{0}</html>", Environment.NewLine);
                }

                Console.WriteLine("Чтение CSV выполнено");

                Console.ReadKey();
            }
        }
    }
}
