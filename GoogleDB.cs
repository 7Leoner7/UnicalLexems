using FireSharp.Interfaces;
using FireSharp;

namespace ConsoleApp1
{
    abstract class GoogleDB
    {
        public FirebaseClient fCl;

        public IFirebaseConfig ifc;

        public void set<T>(string path, T data) => fCl.Set(path, data);

        public string get(string path) => fCl.Get(path).Body;
    }
}
