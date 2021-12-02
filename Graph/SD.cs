using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public static class SD
    {
        
        public static Dictionary<string, string> COMMANDS;
        public static Dictionary<int, string> COLORS;
        public static string Name = "Дмитрий";
        public static string FirstName = "Крячко";
        public static string Patronymic = "Витальевич";
        public static string Task = "21. Случайная раскраска графа, проверка правильности предоженной раскраски.";
        public static string Group = "Б2-ИФСТ-32";
        static SD()
        {
            COMMANDS = new Dictionary<string, string>();
            COMMANDS.Add("help", "newGraph - создание нового графа (удаление старого)\n addVertex - добавление вершины в граф \n addEdges - добавление ребра " +
                "\n removeEdges - удаление ребра \n getWeight - узнать вес ребра " +
                "\n getCountVertex - количество вершин \n getCountEdges - количество ребер \n isAdjacent - узнать смежность вершин" +
                "\n loadFile - загрузка файла \n saveGraph - сохранение графа \n colorizeGraph \n checkRightColorize - запусть проверку раскраски графа");
            COMMANDS.Add("addVertex", "AddVertex");
            COMMANDS.Add("addEdges", "AddEdges");
            COMMANDS.Add("removeEdges", "RemoveEdges");
            COMMANDS.Add("getWeight", "GetWeight");
            COMMANDS.Add("getCountVertex", "GetCountVertex");
            COMMANDS.Add("getCountEdges", "GetCountEdges");
            COMMANDS.Add("isAdjacent", "IsAdjacent");
            COMMANDS.Add("loadFile", "LoadFile");
            COMMANDS.Add("saveGraph", "SaveGraph");
            COMMANDS.Add("newGraph", "NewGraph");
            COMMANDS.Add("isGraphFull", "IsGraphFull");
            COMMANDS.Add("colorizeGraph", "ColorizeGraph");
            COMMANDS.Add("checkRightColorize", "CheckRightColorize");

            //цвета
            COLORS = new Dictionary<int, string>();
            COLORS.Add(1, "Красный");
            COLORS.Add(2, "Белый");
            COLORS.Add(3, "Желтый");
            COLORS.Add(4, "Синий");
            COLORS.Add(5, "Зеленый");
            COLORS.Add(6, "Черный");
            COLORS.Add(7, "Серый");
            COLORS.Add(8, "Оранжевый");
            COLORS.Add(9, "Бордовый");
            COLORS.Add(10, "Голубой");
        }

    }
}
