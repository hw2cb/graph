using System;
using System.IO;
using System.Linq;

namespace Graph
{
    //иницализация нового графа
    //убрать веса из вершин 
    public class Core
    {
        public void NewGraph()
        {
            if(MathGraph.countVertex > 0)
            {
                MathGraph.New();
                Console.WriteLine("Создан новый граф, старый отчищен");
            }
            else
            {
                Console.WriteLine("Создан новый граф");
                int countVertex = MathGraph.countVertex;
                Console.WriteLine($"Текущее количество вершин: {countVertex}");
            }
        }
        public void AddVertex()
        { 
            int countVertex = MathGraph.countVertex;
            Console.WriteLine($"\n Добавление вершины {countVertex+1}");
            MathGraph.AddVertex();
            Console.WriteLine($"Вершина {countVertex+1} добавлена!");
        }
        public void AddEdges()
        {
            Console.WriteLine("\n Добавление ребра графу.");
            int fitrstVertex = 0;
            int secondVertex = 0;
            float ves = 0;
            try
            {
                Console.WriteLine("Первая вершина: ");
                fitrstVertex = int.Parse(Console.ReadLine());
                Console.WriteLine("Вторая вершина: ");
                secondVertex = int.Parse(Console.ReadLine());
                Console.WriteLine("Вес ребра: ");
                ves = float.Parse(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Номера вершин и вес ребра должны быть числами");
                AddEdges();
            }
            //проверка на существование вершин
            if(MathGraph.GetVertex().ContainsKey(fitrstVertex) && MathGraph.GetVertex().ContainsKey(secondVertex))
            {
                //проверка на то, есть ли ребро в графе
                if(!MathGraph.GetEdges().ContainsKey((fitrstVertex, secondVertex)))
                {
                    MathGraph.AddEdges(fitrstVertex, secondVertex, ves);
                    Console.WriteLine($"Ребро с весом {ves} добавлено!");
                }
                else
                {
                    Console.WriteLine("Ребро уже существует");
                }
                
            }
            else
            {
                Console.WriteLine("Одна или несколько вершин не найдены, добавьте сначало вершины");
            }
        }
        public void RemoveEdges()
        {
            Console.WriteLine("\n Удаление ребра.");
            int firstVertex = 0;
            int secondVertex = 0;

            try
            {
                Console.WriteLine("Первая вершина: ");
                firstVertex = int.Parse(Console.ReadLine());
                Console.WriteLine("Вторая вершина: ");
                secondVertex = int.Parse(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Номера вершин и вес ребра должны быть числами");
                AddEdges();
            }
            //проверка на существование ребер
            if (CheckedEdges(firstVertex, secondVertex))
            {
                MathGraph.RemoveEdges(firstVertex, secondVertex);
                Console.WriteLine($"Ребро удалено!");
            }
            else
            {
                Console.WriteLine("Ребро не найдено");
            }
        }
        //вывод на экран количество вершин
        public void GetCountVertex()
        {
            Console.WriteLine($"Количество вершин: {MathGraph.countVertex}");
        }
        //вывод на экран количество ребер
        public void GetCountEdges()
        {
            Console.WriteLine($"Количество ребер: {MathGraph.countEdges}");
        }

        public void GetWeight()
        {
            Console.WriteLine("\n Узнать вес ребра.");
            int firstVertex = 0;
            int secondVertex = 0;
            try
            {
                Console.WriteLine("Первая вершина: ");
                firstVertex = int.Parse(Console.ReadLine());
                Console.WriteLine("Вторая вершина: ");
                secondVertex = int.Parse(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Номера вершин должны быть числами");
                GetWeight();
            }
            //проверка на существование ребер
            if (CheckedEdges(firstVertex, secondVertex))
            {
                Console.WriteLine($"Вес ребра: {MathGraph.GetWeight(firstVertex, secondVertex)}");
            }
            else
            {
                Console.WriteLine("Ребро не найдено");
            }
        }
        public void IsAdjacent()
        {
            Console.WriteLine("\n Узнать, смежны ли две вершины.");
            int firstVertex = 0;
            int secondVertex = 0;
            try
            {
                Console.WriteLine("Первая вершина: ");
                firstVertex = int.Parse(Console.ReadLine());
                Console.WriteLine("Вторая вершина: ");
                secondVertex = int.Parse(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Номера вершин должны быть числами");
                IsAdjacent();
            }
            Console.WriteLine($"Вершины {firstVertex} и {secondVertex} смежны: {MathGraph.IsAdjacent(firstVertex, secondVertex)}");
        }
        public bool CheckedEdges(int firstVertex, int secondVertex)
        {
            if (MathGraph.GetEdges().ContainsKey((firstVertex, secondVertex)))
            {
                return true;
            }
            return false;
        }
        public void LoadFile()
        {
            Console.WriteLine("\n Для загрузки файла необходимо выбрать вид загружаемого файла: \n 1 - список ребер \n 2 - матрица");
            int typeFile = 0;
            string pathToFile = "";
            try
            {
                typeFile = int.Parse(Console.ReadLine());
                
                Console.WriteLine("Введите путь к файлу: ");
                pathToFile = Console.ReadLine();
                FileInfo file = new FileInfo(pathToFile);
                if (!file.Exists) 
                { 
                    Console.WriteLine("Путь к файлу не верный или файл не существует.");
                    LoadFile();
                }
                if (typeFile == 1) MathGraph.LoadGraphListEdges(pathToFile);
                else if (typeFile == 2) MathGraph.LoadGraphMatrix(pathToFile);
                else 
                {
                    Console.WriteLine("1 - список ребер, 2 - матрица");
                    LoadFile();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("1 - список ребер, 2 - матрица");
                LoadFile();
            }
        }
        public void SaveGraph()
        {
            Console.WriteLine("\n Для сохранения файла необходимо выбрать тип сохранения: \n 1 - список ребер \n 2 - матрица");
            int typeFile = 0;
            string pathToDirectory = "";
            string nameFile = "";
            try
            {
                typeFile = int.Parse(Console.ReadLine());

                Console.WriteLine("Введите путь сохранения файла и название самого файла (пример: C:\\): ");
                pathToDirectory = Console.ReadLine();
                Console.WriteLine("Введите название файла с расширением .txt (пример: example.txt): ");
                nameFile = Console.ReadLine();
                if (!Directory.Exists(pathToDirectory)) //проверить существует ли каталог
                {
                    Console.WriteLine("Каталог не существует");
                    SaveGraph();
                }
                var splitString = nameFile.Split('.');
                if(splitString[1]!= "txt")
                {
                    Console.WriteLine("Расширение файла не верно");
                    SaveGraph();
                }


                if (typeFile == 1) MathGraph.SaveGraphEdges(pathToDirectory, nameFile);
                else if (typeFile == 2) MathGraph.SaveGraphMatrix(pathToDirectory, nameFile);
                else
                {
                    Console.WriteLine("1 - список ребер, 2 - матрица");
                    SaveGraph();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("1 - список ребер, 2 - матрица");
                SaveGraph();
            }
        }
        public void IsGraphFull()
        {
            if(MathGraph.IsGraphFull())
            {
                Console.WriteLine("Граф полный");
            }
            else Console.WriteLine("Граф не полный");
        }
        public void ColorizeGraph()
        {
            //полный граф, если цветов будет меньше чем вершин полного графа, ошибка
            //все вершины полного графа смежны
            Console.WriteLine("\n Разукрасить граф.");
            int countColor = 0;
            try
            {
                Console.WriteLine("Введите кол-во цветов: ");
                countColor = int.Parse(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Количество должно быть цифрой");
                ColorizeGraph();
            }
            if (countColor > SD.COLORS.Count)
            {
                Console.WriteLine("Пока что в программе существует 10 цветов");
                ColorizeGraph();
            }
            //проверка на полный граф
            if (MathGraph.IsGraphFull() && countColor < MathGraph.countVertex)
            {
                Console.WriteLine("Ваш граф полный, цветов должно быть больше или равно количеству вершин.");
            }
            else
            {
                MathGraph.ColorizeGraph(countColor);

                Console.WriteLine("-------------------------------");
                Console.WriteLine("Результаты работы алгоритма: " + SD.Task);
                Console.WriteLine($"Студент: {SD.FirstName} {SD.Name} {SD.Patronymic}");
                Console.WriteLine($"Группа: {SD.Group}");
                Console.WriteLine($"Дата: {DateTime.Now}");
                Console.WriteLine($"Матрица смежности: ");
                for (int i = 1; i <= MathGraph.countVertex; i++)//строки
                {
                    for (int j = 1; j <= MathGraph.countVertex; j++)//столбцы
                    {
                        if (MathGraph.GetEdges().ContainsKey((i, j)))
                        {
                            Console.Write(MathGraph.GetEdges()[(i, j)]);
                            Console.Write(' ');
                        }
                        else
                        {
                            Console.Write(0);
                            Console.Write(' ');
                        }
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("Вершины и цвета: ");
                for (int i=0; i<MathGraph.countVertex; i++)
                {
                    var color = SD.COLORS[MathGraph.GetVertex().ElementAt(i).Value];
                    Console.WriteLine($"Вершина: {MathGraph.GetVertex().ElementAt(i).Key}, цвет: {color}");
                }
                Console.WriteLine("Результат проверки раскраски: ");
                CheckRightColorize();
            }
        }
        public void CheckRightColorize()
        {
            if (MathGraph.IsColorize)
            {
                if (MathGraph.CheckRightColorize()) Console.WriteLine($"Вершинная раскраска является правильной. Количество используемых цветов = {MathGraph.GetColorUse()}");
                else Console.WriteLine("Вершинная раскраска является не верной");
            }
            else
                Console.WriteLine("Граф не раскрашен");
        }
    }
}
