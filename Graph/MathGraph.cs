using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Graph
{
    public static class MathGraph
    {
        private static Dictionary<int, int> Vertex;//!! номер вершины, номер цвета
        
        private static Dictionary<(int firstVertex, int secondVertex), float> Edges;
        public static bool IsColorize
        {
            get;
            private set;
        }
        public static int countVertex
        {
            get { return Vertex.Count; }
            private set { }
        }
        public static int countEdges
        {
            get { return Edges.Count/2; }
            private set { }
        }

        static MathGraph()
        {
            Console.WriteLine("\n Log: инициализация MathGraph...");
            IsColorize = false;
            Vertex = new Dictionary<int, int>();
            Edges = new Dictionary<(int firstVertex, int secondVertex), float>();
        }
        public static void New()
        {
            IsColorize = false;
            Vertex.Clear();
            Edges.Clear();
        }
        public static Dictionary<int, int> GetVertex()
        {
            return Vertex;
        }
        public static Dictionary<(int firstVertex, int secondVertex), float> GetEdges()
        {
            return Edges;
        }
        public static void AddVertex()
        {
            Vertex.Add(countVertex+1, 0); //!!
        }
        public static void AddEdges(int firstVertex, int secondVertex, float ves)
        {
            Edges.Add((firstVertex, secondVertex), ves);
            Edges.Add((secondVertex, firstVertex), ves);
        }
        public static void RemoveEdges(int firstVertex, int secondVertex)
        {
            Edges.Remove((firstVertex, secondVertex));
            Edges.Remove((secondVertex, firstVertex));
        }
        public static float GetWeight(int firstVertex, int secondVertex)
        {
            float res = Edges[(firstVertex, secondVertex)];
            return res;
        }
        //смежность
        public static bool IsAdjacent(int firstVertex, int secondVertex)
        {
            if(Edges.ContainsKey((firstVertex, secondVertex)) || Edges.ContainsKey((secondVertex, firstVertex)))
            {
                return true;
            }
            if (firstVertex == secondVertex) return true;
            return false;
        }
        public static void LoadGraphListEdges(string path)
        {
            //загрузка файла в виде списка ребер
            using(FileStream fstream = File.OpenRead(path))
            {
                //преобразуем в байты
                byte[] array = new byte[fstream.Length];
                //читаем данные
                fstream.Read(array, 0, array.Length);
                //декодируем байты в строку
                string resultReadFile = System.Text.Encoding.Default.GetString(array);
                char[] separators = new char[] { ' ', '\r', '\n' };
                string[] splitsString = resultReadFile.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                var cleanList = splitsString.ToList();
                for(int i=3; i<=cleanList.Count; i=i+3)
                {
                    cleanList.RemoveAt(i);
                    i = i - 1;
                }
                cleanList.RemoveAt(0);//удаляю первый элемент, кол-во ребер
                //добавление вершин
                foreach (var v in cleanList)
                {
                    if(!Vertex.ContainsKey(int.Parse(v)))//проверка на то, существует ли вершина уже
                    {
                        Vertex.Add(int.Parse(v), 0);
                    }
                }
                //добавление ребер
                for (int i=1, j=2, x=3; x<splitsString.Length; i= i+3, j = j+3, x = x+3)
                {
                    AddEdges(int.Parse(splitsString[i]), int.Parse(splitsString[j]), float.Parse(splitsString[x]));
                }
            }
        }
        public static void LoadGraphMatrix(string path)
        {
            //загрузка файла в виде матрицы
            using (FileStream fstream = File.OpenRead(path))
            {
                //преобразуем в байты
                byte[] array = new byte[fstream.Length];
                //читаем данные
                fstream.Read(array, 0, array.Length);
                //декодируем байты в строку
                string resultReadFile = System.Text.Encoding.Default.GetString(array);
                char[] separators = new char[] { ' ', '\r', '\n' };
                string[] splitsString = resultReadFile.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                int buffCountVertex = int.Parse(splitsString[0]);
                //добавление вершин
                for(int i=1; i<=buffCountVertex; i++)
                {
                    Vertex.Add(i, 0);
                }
                //добавление ребер
                int buff = 1;
                int count = 0;
                for (int i = 1; i <= buffCountVertex; i++)
                {
                    for (int j = 1; j <= buffCountVertex; j++)
                    {
                        count = count + 1;
                        if ((float.Parse(splitsString[count]) != 0) && (!Edges.ContainsKey((i, j)) && !Edges.ContainsKey((j, i))))
                        {
                            AddEdges(i, j, float.Parse(splitsString[count]));
                        }
                        buff = buff + 1;
                    }
                    buff = 0;
                }
            }
        }
        public static void SaveGraphEdges(string path, string nameFile)
        {
            string pathToFile = path + '/' + nameFile;
            string result = "";
            result = result + countEdges + '\n';
            foreach (var item in Edges)
            {
                if(!result.Contains(item.Key.secondVertex.ToString() + ' ' + item.Key.firstVertex + ' ' + item.Value + '\n'))
                {
                    result = result + item.Key.firstVertex + ' ' + item.Key.secondVertex + ' ' + item.Value +'\n';
                }
            }
            using(FileStream fstream = new FileStream(pathToFile, FileMode.OpenOrCreate))
            {
                byte[] array = System.Text.Encoding.Default.GetBytes(result);
                fstream.Write(array, 0, array.Length);
            }
        }
        public static void SaveGraphMatrix(string path, string nameFile)
        {
            string pathToFile = path + '/' + nameFile;

            string result = "";
            result = result + countVertex + '\n';
            for(int i=1; i<=countVertex; i++)//строки
            {
                for(int j=1; j<=countVertex; j++)//столбцы
                {
                    if(Edges.ContainsKey((i, j)))
                    {
                        result = result + Edges[(i, j)] + ' ';
                    }
                    else
                    {
                        result = result + 0 + ' ';
                    }
                }
                result = result + '\n';
            }
            using (FileStream fstream = new FileStream(pathToFile, FileMode.OpenOrCreate))
            {
                byte[] array = System.Text.Encoding.Default.GetBytes(result);
                fstream.Write(array, 0, array.Length);
            }
        }
        public static bool IsGraphFull()
        {
            for(int i=1; i<=Vertex.Count; i++)
            {
                for(int j=1; j<=Vertex.Count; j++)
                {
                    if(!IsAdjacent(i, j))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public static void ColorizeGraph(int countColor)
        {
            IsColorize = true;
            var rand = new Random();
            for(int i = 1; i<=Vertex.Count; i++)
            {
                Vertex[i] = rand.Next(1, countColor+1);
            }
        }
        public static bool CheckRightColorize()//! 
        {
            if (Edges.Count == 0) return true;
            foreach(var i in Edges)
            {
                if(Vertex[i.Key.firstVertex] == Vertex[i.Key.secondVertex])
                {
                    return false;
                }
            }
            return true;
            //у смежных вершин не должны быть одинаковые цвета
        }
        public static int GetColorUse()
        {
            Dictionary<int, int> valCount = new Dictionary<int, int>();

            foreach(var i in Vertex.Values)
            {
                if (valCount.ContainsKey(i))
                {
                    valCount[i]++;
                }
                else
                    valCount[i] = 1;
            }
            return valCount.Count;
        }
        
    }
}
