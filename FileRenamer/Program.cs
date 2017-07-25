using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FileRenamer
{
    class Program
    {
        private static int _counter;
        static void Main(string[] args)
        {
            //---Test directory---
            //var dirPath = "C:\\Users\\danie\\Documents\\asdasd";

            var dirPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var files = Directory.GetFiles(dirPath, "*.pdf");

            PrintInstructions();

            string answer = CheckForAnswer();

            RenameFiles(files, answer);
            PrintResult();
            Console.ReadKey();
        }

        private static void PrintInstructions()
        {
            Console.WriteLine("Detta program ändrar å,ä och ö till a,a och o, samt mellanslag till _ i filnamnen på alla pdf-filer i mappen programmet ligger i.");
            Console.WriteLine("Vill du även ändra Å,Ä och Ö, alltså versalerna? Svara j eller n och tryck enter.");
        }

        private static string CheckForAnswer()
        {
            Console.Write("Svar: ");
            string answer;
            while (true)
            {
                answer = Console.ReadLine().ToLower();
                if (answer != "j" && answer != "n")
                {
                    Console.Write("Du angav ett felaktigt svar, svara j eller n: ");
                }
                else
                {
                    break;
                }
            }
            Console.WriteLine();
            return answer;
        }
        private static void RenameFiles(string[] files, string answer)
        {
            foreach (var file in files)
            {
                if (file.IndexOfAny("åäöÅÄÖ ".ToCharArray()) != -1)
                {
                    StringBuilder newFileName = ReplaceCharacters(answer, file);

                    File.Move(file, newFileName.ToString());
                    PrintNewFileNames(file, newFileName);
                }
            }
        }

        private static StringBuilder ReplaceCharacters(string answer, string file)
        {
            var newFileName = new StringBuilder(file);
            newFileName.Replace('å', 'a');
            newFileName.Replace('ä', 'a');
            newFileName.Replace('ö', 'o');
            newFileName.Replace(' ', '_');

            if (answer == "j")
            {
                newFileName.Replace('Å', 'A');
                newFileName.Replace('Ä', 'A');
                newFileName.Replace('Ö', 'O');
            }

            return newFileName;
        }

        private static void PrintNewFileNames(string file, StringBuilder newFileName)
        {
            if (file != newFileName.ToString())
            {
                _counter++;
                Console.WriteLine(newFileName);
            }
        }

        private static void PrintResult()
        {
            Console.WriteLine($"\n{_counter} filnamn har ändrats.\nTryck på valfri tangent för att avsluta...");
        }
    }
}
