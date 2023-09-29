using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jalgpall
{
    public class Ball
    {
        public double X { get; private set; } //позиция мяча по X
        public double Y { get; private set; } //позиция мяча по Y

        private double _vx, _vy; //поля для хранения скорости мяча по осям X и Y

        private Game _game; //Приватное поле для хранения ссылки на объект игры
        public string ballSym { get; } = "o";

        public Ball(double x, double y, Game game) //устанавливаем скорость
        {
            _game = game;
            X = x;
            Y = y;
        }

        public void SetSpeed(double vx, double vy)
        {
            _vx = vx;
            _vy = vy;
        }

        public void Move()
        {
            double newX = X + _vx;
            double newY = Y + _vy;
            if (_game.Stadium.IsIn(newX, newY)) //Проверяем, остается ли мяч в пределах поля стадиона
            {
                X = newX; //если мяч остается в пределах поля, обновляем его координаты
                Y = newY;
            }
            else
            {
                _vx = 0; // если мяч покидает пределы поля, скорость = 0
                _vy = 0;
            }
        }

        public void DrawBall(Ball ball) //отрисовка мяча
        {
            Console.SetCursorPosition(Convert.ToInt32(Math.Round(X)), Convert.ToInt32(Math.Round(Y)));
            Console.Write(ball.ballSym);
        }
    }
}