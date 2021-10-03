using FireSharp;
using FireSharp.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp1
{
    class UnixLexem : GoogleDB 
    {
        public string direction { get; set; }
        public string SaveDirection { get; set; }
        public Action<string> VisualizeUnicalLexem;
        public Action<string> Error;

        static void ErrorAct(string err)
        {
            Console.WriteLine(err);
        }

        protected bool ContainLexem(string lexem, string SavePath)
        {
            try
            {
                using (FileStream fs = new FileStream(SavePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read))
                {
                    var c = new char();
                    var SameLexem = "";
                    byte[] b;
                    while (fs.Length != fs.Position)
                    {
                        var byteR = fs.ReadByte();
                        if ((byteR == 208) || (byteR == 209))
                        {
                            b = new byte[2];
                            b[0] = (byte)byteR;
                            b[1] = (byte)fs.ReadByte();
                            c = Encoding.Default.GetString(b)[0];
                        }
                        else
                        {
                            b = new byte[1];
                            b[0] = (byte)byteR;
                            c = Encoding.Default.GetString(b)[0];
                        }
                        if (char.IsLetter(c)) SameLexem += char.ToLower(c);
                        else
                        {
                            if (lexem == SameLexem)
                            {
                                return true;
                            }
                            else { SameLexem = ""; }
                        }
                    }
                }    
            }
            catch(Exception e)
            {
                Error(e.Message);
            }
            return false;
        }

        protected int RankLexem(string lexem, FileStream fs0)
        { 
            var k = 1;
            try
            {
                var point = fs0.Position;
                FileStream fs = fs0;
                byte[] b;
                char c = new char();
                var SameLexem = "";
               
                while (fs.Length != fs.Position)
                {
                    var byteR = fs.ReadByte();
                    if ((byteR == 208) || (byteR == 209))
                    {
                        b = new byte[2];
                        b[0] = (byte)byteR;
                        b[1] = (byte)fs.ReadByte();
                        c = Encoding.Default.GetString(b)[0];
                    }
                    else
                    {
                        b = new byte[1];
                        b[0] = (byte)byteR;
                        c = Encoding.Default.GetString(b)[0];
                    }
                    if (char.IsLetter(c)) SameLexem += char.ToLower(c);
                    else
                    {
                        if (lexem == SameLexem) k++;
                        SameLexem = "";
                    }
                }
                fs0.Position = point;
            }
            catch(Exception e)
            {
                Error(e.Message);
            }
            return k;
        }

        protected void AddDataInFile(string SavePath, string lexem, int rank)//Возврат коретки байт = 10 по utf-8, 48<=byte<=57 - цифры по utf-8, пробел байт = 32, 239 191 189 - заменяющий символ
        {
            try
            {
                using (FileStream fs = new FileStream(SavePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    byte[] b;
                    var c = new char();
                    var lexem0 = "";
                    var rank0 = "";
                    var pos = fs.Position;
                    var posN = false;
                    while (fs.Length > fs.Position)
                    {

                        var byteR = fs.ReadByte();
                        if ((byteR == 208) || (byteR == 209))
                        {
                            b = new byte[2];
                            b[0] = (byte)byteR;
                            b[1] = (byte)fs.ReadByte();
                            c = Encoding.Default.GetString(b)[0];
                        }
                        else
                        {
                            b = new byte[1];
                            b[0] = (byte)byteR;
                            c = Encoding.Default.GetString(b)[0];
                        }
                        if (char.IsLetter(c)) lexem0 += c;
                        if (char.IsDigit(c)) rank0 += c;
                        if (((byteR == 13) || (byteR == 10)) && (lexem0 != ""))
                        {
                            var stroka = "";
                            byte[] b0 = new byte[0];
                            if (rank > int.Parse(rank0))
                            {
                                b0 = Encoding.Default.GetBytes(lexem + " " + rank + '\n');
                                b = Encoding.Default.GetBytes(lexem0 + " " + rank0 + '\n');

                                var lastpos = fs.Position;
                                List<byte> b1 = new List<byte>();
                                for (int i = 0; i < b.Length; i++)
                                    b1.Add(b[i]);
                                while (fs.Position != fs.Length)
                                    b1.Add((byte)fs.ReadByte());
                                fs.Position = lastpos;

                                pos = fs.Position ^ pos;
                                fs.Position = fs.Position ^ pos;
                                pos = fs.Position ^ pos;
                                fs.Write(b0, 0, b0.Length);

                                fs.Write(b1.ToArray(), 0, b1.Count);
                                lexem = lexem0;
                                rank = int.Parse(rank0);
                                posN = true;
                                return;
                            }
                            pos = fs.Position;
                            lexem0 = stroka;
                            rank0 = "";
                        }
                    }
                    fs.Position = pos == 0 ? fs.Position : pos;
                    if (posN == false)
                    {
                        fs.Position = fs.Length;
                        b = Encoding.Default.GetBytes(lexem + " " + rank + '\n');
                        fs.Write(b, 0, b.Length);
                    }
                }
            }
            catch(Exception e)
            {
                Error(e.Message);
            }
        }

        public void CreateFileOfListOfLexemAndRank(string SavePath)
        {
            try
            {
                using (FileStream fs = new FileStream(direction, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    var lexem = "";
                    char c = new char();
                    byte[] b;
                    while (fs.Length != fs.Position)
                    {
                        var byteR = fs.ReadByte();
                        if ((byteR == 208) || (byteR == 209))
                        {
                            b = new byte[2];
                            b[0] = (byte)byteR;
                            b[1] = (byte)fs.ReadByte();
                            c = Encoding.Default.GetString(b)[0];
                        }
                        else
                        {
                            b = new byte[1];
                            b[0] = (byte)byteR;
                            c = Encoding.Default.GetString(b)[0];
                        }

                        if ((char.IsLetter(c))) lexem += char.ToLower(c);
                        else
                        {
                            if ((!ContainLexem(lexem, SavePath)) && (lexem != ""))
                            {
                                AddDataInFile(SavePath, lexem, RankLexem(lexem, fs));
                            }
                            lexem = "";
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Error(e.Message);
            }
        }

        public void ExportLexemsDB(string BasePath, string AuthSecret, string DataPath, string NameDirSave)
        {
            ifc = new FirebaseConfig()
            {
                BasePath = BasePath,
                AuthSecret = AuthSecret
            };
            try
            { 
            fCl = new FirebaseClient(ifc);
            FileInfo fi = new FileInfo(DataPath);
            if (!fi.Exists) throw new ArgumentException("Такого файла нет");
            var PathInDB = "/Files/"+NameDirSave.Replace(".","");

            using(StreamReader sr = new StreamReader(DataPath))
            {
                List<string> ls = new List<string>();
                while (!sr.EndOfStream)
                    ls.Add(sr.ReadLine());
                set(PathInDB, ls.ToArray());
            }
            }catch(Exception e)
            {
                Error(e.Message);
            }
        }

        public void ImportLexemsDB(string BasePath, string AuthSecret, string SaveDataPath, string NameDirSave)
        {
            ifc = new FirebaseConfig()
            {
                BasePath = BasePath,
                AuthSecret = AuthSecret
            };
            try
            {
                fCl = new FirebaseClient(ifc);
                var PathInDB = "/Files/" + NameDirSave.Replace(".", "");
                var str = get(PathInDB).Replace("[","").Replace("]", "").Replace("\"", "").Split(",");
                using (StreamWriter sw = new StreamWriter(SaveDataPath))
                {
                    for(int i = 0; i<str.Length; i++)
                        sw.WriteLine(str[i]);
                }
            }
            catch (Exception e)
            {
                Error(e.Message);
            }
        }

        public UnixLexem() { }

        public UnixLexem(string direction)
        {
            this.direction = direction;
            this.Error = (string err) => ErrorAct(err);
            FileInfo fi = new FileInfo(direction);
            if (!fi.Exists) throw new ArgumentException("Такого файла нет");
        }

        public UnixLexem(string direction, string SaveDirection)
        {
            this.direction = direction;
            this.SaveDirection = SaveDirection;
            this.Error = (string err) => ErrorAct(err);
            FileInfo fi = new FileInfo(direction);
            if (!fi.Exists) throw new ArgumentException("Такого файла нет");
        }
    }
}
