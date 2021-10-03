using System;
using System.IO;

namespace ConsoleApp1
{
    class HtmlParser : Parser, IHtmlPage
    {
        public string direct { get; set; }
        protected const string extens = IHtmlPage.extens;
        internal string ParseProperty0 = "<*>,&*;";
        internal string ParseProperty1 = "&rsquo;";
        public Action<string> Error;

        static void ErrorAct(string err)
        {
            Console.WriteLine(err);
        }

        public HtmlParser(string direct)
        {
            this.all = '*';
            this.direct = direct;
            FileInfo fi = new FileInfo(direct);
            this.Error = (string err) => ErrorAct(err);
            try
            {
                if (!fi.Exists) throw new ArgumentException("Файла с данным названием несуществует");
                if (fi.Extension != extens) throw new ArgumentException("Данный файл не является html-страницой");
            }
            catch (Exception e)
            {
                Error(e.Message);
            }
        }

        public HtmlParser(string direct, char all)
        {
            this.all = all;
            this.direct = direct;
            FileInfo fi = new FileInfo(direct);
            this.Error = (string err) => ErrorAct(err);
            try
            {
                if (!fi.Exists) throw new ArgumentException("Файла с данным названием несуществует");
                if (fi.Extension != extens) throw new ArgumentException("Данный файл не является html-страницой");
            }catch(Exception e)
            {
                Error(e.Message);
            }
        }

        public string ParseHtml(string str) => Parse(str.Replace(ParseProperty1,""), ParseProperty0, " ");

        public string ParseHtml()
        {
            var str = "";
            try
            {
                using (StreamReader sr = new StreamReader(direct))
                {
                    str = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Error(e.Message);
            }
            return Parse(str.Replace(ParseProperty1, ""), ParseProperty0, " ");
        }
        /// <summary>
        /// SavePath - полный путь файла
        /// </summary>
        /// <param name="SavePath"></param>
        public void ParseHtmlInFile(string SavePath)
        {
            try
            {
            FileInfo fi = new FileInfo(direct);
            var str = "";
            
                using (StreamReader sr = new StreamReader(direct))
                {
                    str = sr.ReadToEnd();
                    sr.Close();
                }

                using (StreamWriter sr = new StreamWriter(SavePath))
                {
                    sr.Write(Parse(str.Replace(ParseProperty1, ""), ParseProperty0, " "));
                }
            }
            catch (Exception e)
            {
                Error(e.Message);
            }
        }
    }
}
