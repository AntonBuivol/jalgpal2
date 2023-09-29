using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jalgpall
{
    public class Stadium
    {
        public int Width { get; } //ширина стадиона

        public int Height { get; } //высота стадиона

        List<Figure> wallList;

        public Stadium(int width, int height)
        {
            Width = width; //устанавливаем ширину
            Height = height; //устанавливаем высоту
            wallList = new List<Figure>();

            HorizontalLine upLine = new HorizontalLine(0, width, 0, '-'); //создание верхней стены
            HorizontalLine downLine = new HorizontalLine(0, width, height, '-'); //создание нижний стены
            VerticalLine leftLine = new VerticalLine(0, height, 0, '|'); //создание левой стены
            VerticalLine rightLine = new VerticalLine(0, height, width, '|'); //создание правой стены

            //добавление всех стен в лист
            wallList.Add(upLine);
            wallList.Add(downLine);
            wallList.Add(leftLine);
            wallList.Add(rightLine);

            LeftGate();
            RightGate(Width);
        }

        public void LeftGate()
        {
            HorizontalLine LeftupLine = new HorizontalLine(5, 10, 7, '-');
            HorizontalLine LeftdownLine = new HorizontalLine(5, 10, 12, '-');
            VerticalLine LeftleftLine = new VerticalLine(7, 12, 5, '|');


            wallList.Add(LeftupLine);
            wallList.Add(LeftdownLine);
            wallList.Add(LeftleftLine);
            
        }

        public void RightGate(int width)
        {
            HorizontalLine RightupLine = new HorizontalLine(width - 10, width-5, 7, '-');
            HorizontalLine RightdownLine = new HorizontalLine(width-10, width-5, 12, '-');
            VerticalLine RightrightLine = new VerticalLine(7, 12, width-5, '|');

            wallList.Add(RightupLine);
            wallList.Add(RightdownLine);
            wallList.Add(RightrightLine);

        }

        public bool IsIn(double x, double y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height; //Находится ли мяч внутри поля или
        }
        public void Draw() //рисует стадион
        {
            foreach(var wall in wallList)
            {
                wall.Draw();
            }
        }
    }
}