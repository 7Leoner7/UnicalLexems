using System;
using System.IO;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;
using FireSharp;

namespace ConsoleApp1
{
    class Program
    {
        static string BasePath = "https://unicallexems-default-rtdb.firebaseio.com/";
        static string AuthSecret = "IHOByN1ssSab8mScBR6gPmldOpzcbmliI7DbcOJs";

        static void Visualize(string direction)
        {
            using(StreamReader sr = new StreamReader(direction))
            {
                while(!sr.EndOfStream)
                Console.WriteLine(sr.ReadLine());
            }
        }

        static void Main(string[] args)
        {
            //            Console.WriteLine(@"Здравствуйте, многоуважаемый пользователь.
            //Я - программа для поиска уникальных слов в html файлах.
            //На данный момент вы можете использовать только две команды:
            //1. Вывести в консоль уникальные слова
            //2. Сохранить парсированный файл html
            //Жду номер вашей команды:
            //");         try
            //            {
            //                while (true)
            //                {
            //                    var num = Console.ReadLine();
            //                    if (num == "1")
            //                    {
            //                        Console.WriteLine(@"Укажите путь к html файлу:
            //");
            //                        var dir = Console.ReadLine();
            //                        HtmlParser parser = new HtmlParser(dir);
            //                        Console.WriteLine(@"Укажите куда можно сохранить файл с уникальными словами:
            //");
            //                        dir = Console.ReadLine();
            //                        parser.ParseHtmlInFile(dir);

            //                        UnixLexem lexem = new UnixLexem(dir);
            //                        FileInfo fi = new FileInfo(dir);
            //                        if (!fi.Exists) fi.Create();
            //                        lexem.CreateFileOfListOfLexemAndRank(fi.Directory + "\\" + fi.Name + "UL" + fi.Extension);
            //                        Visualize(fi.Directory + "\\" + fi.Name + "UL" + fi.Extension);

            //                        new FileInfo(dir).Delete();
            //                        Console.WriteLine("Действие выполнено успешно!");
            //                    }
            //                    else
            //                    {
            //                        if (num == "2")
            //                        {
            //                            Console.WriteLine(@"Укажите путь к html файлу:
            //");
            //                            var dir = Console.ReadLine();
            //                            HtmlParser parser = new HtmlParser(dir);
            //                            Console.WriteLine(@"Укажите куда можно сохранить файл с уникальными словами:
            //");
            //                            dir = Console.ReadLine();
            //                            parser.ParseHtmlInFile(dir);

            //                            Console.WriteLine("Действие выполнено успешно!");
            //                        }
            //                        else
            //                        {
            //                            Console.WriteLine("Такой команды нет(\nХотите выйти(Y - да, N - нет)?");
            //                            if (Console.ReadLine() != "N") break;
            //                        }
            //                    }
            //                }
            //            }
            //            catch(Exception e)
            //            {
            //                Console.WriteLine(e.Message);
            //            }
            UnixLexem lexem = new UnixLexem();
            //lexem.ExportLexemsDB(BasePath,AuthSecret, @"C:\Users\Leoner\source\repos\ConsoleApp2\file.txtUL.txt","filetxtUL");
            lexem.ImportLexemsDB(BasePath, AuthSecret, @"C:\Users\Leoner\source\repos\ConsoleApp2\fileUL.txt", "filetxtUL");
        }
    }
}
