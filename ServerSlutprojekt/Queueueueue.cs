using System;

namespace ServerSlutprojekt
{
    public class Queueueueue<T>
    {
        static Random genrator = new Random();
        List<T> idList = new List<T>();

        public void Add(T item)
        {
            idList.Add(item);
        }

        public T Get()
        {
            return idList[genrator.Next(0, idList.Count - 1)];
        }
    }
}