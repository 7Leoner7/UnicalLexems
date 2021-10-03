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
            Console.WriteLine(@"Здравствуйте, многоуважаемый пользователь.
            Я - программа для поиска уникальных слов в html файлах.
            На данный момент вы можете использовать только две команды:
            1. Вывести в консоль уникальные слова из файла
            2. Сохранить парсированный файл html
            3. Вывести в консоль уникальные слова из RealTimeDB
            Жду номер вашей команды:
");         try
            {
                while (true)
                {
                    var num = Console.ReadLine();
                    if (num == "1")
                    {
                        Console.WriteLine(@"Укажите путь к html файлу:
");
                        var dir = Console.ReadLine();
                        HtmlParser parser = new HtmlParser(dir);
                        if (parser.HasError) { Console.WriteLine("Действие успешно провалено! Введите номер следующего действия!"); continue; }
                        Console.WriteLine(@"Укажите куда можно сохранить файл с уникальными словами:
");
                        dir = Console.ReadLine();
                        parser.ParseHtmlInFile(dir);

                        UnixLexem lexem = new UnixLexem(dir);
                        if (lexem.HasError) { Console.WriteLine("Действие успешно провалено! Введите номер следующего действия!"); continue; }
                        FileInfo fi = new FileInfo(dir);
                        if (!fi.Exists) fi.Create();
                        lexem.CreateFileOfListOfLexemAndRank(fi.Directory + "\\" + fi.Name + "UL" + fi.Extension);
                        Visualize(fi.Directory + "\\" + fi.Name + "UL" + fi.Extension);

                        new FileInfo(dir).Delete();
                        Console.WriteLine("Действие выполнено успешно!");
                        Console.WriteLine("Вы хотите добавить в GoogleDB(Y - да, N - нет)?");
                        if (Console.ReadLine() != "N") { Console.WriteLine("Действие выполнено успешно! Введите номер следующего действия!"); continue; }
                        Console.WriteLine("Введите сначало ссылку на GoogleDB, потом AuthSecret");
                        lexem.ExportLexemsDB(Console.ReadLine(), Console.ReadLine(), fi.Directory + "\\" + fi.Name + "UL" + fi.Extension, fi.Name);
                        Console.WriteLine("Действие выполнено успешно! Введите номер следующего действия!");
                    }
                    else
                    {
                        if (num == "2")
                        {
                            Console.WriteLine(@"Укажите путь к html файлу:
");
                            var dir = Console.ReadLine();
                            HtmlParser parser = new HtmlParser(dir);
                            if (parser.HasError) { Console.WriteLine("Действие успешно провалено! Введите номер следующего действия!"); continue; }
                            Console.WriteLine(@"Укажите куда можно сохранить файл с уникальными словами:
");
                            dir = Console.ReadLine();
                            parser.ParseHtmlInFile(dir);

                            Console.WriteLine("Действие выполнено успешно! Введите номер следующего действия!");
                        }
                        else
                        {
                            if (num == "3")
                            {
                                Console.WriteLine("Введите сначало ссылку на GoogleDB, потом AuthSecret, потом полный путь к файлу сохранения и имя файла");
                                UnixLexem lexem = new UnixLexem();
                                if (lexem.HasError) { Console.WriteLine("Действие успешно провалено! Введите номер следующего действия!"); continue; }
                                lexem.ImportLexemsDB(Console.ReadLine(), Console.ReadLine(), Console.ReadLine(), Console.ReadLine());
                                Console.WriteLine("Действие выполнено успешно! Введите номер следующего действия?");
                            }
                            else
                            {
                                Console.WriteLine("Такой команды нет(\nХотите выйти(Y - да, N - нет)?");
                                if (Console.ReadLine() != "N") break;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
