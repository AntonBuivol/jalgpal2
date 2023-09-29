using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jalgpall
{
    public class Player
    {
        //поля и атрибуты
        public string Name { get; } //Имя игрока
        public double X { get; private set; } //X координата
        public double Y { get; private set; } //Y координата
        private double _vx, _vy; //расстояние между игроком и мячом
        public Team? Team { get; set; } = null; //команда в которой играет игрок

        private const double MaxSpeed = 5; //скорость бега
        private const double MaxKickSpeed = 25; //максимальная скорость удара
        private const double BallKickDistance = 10; //расстояния полета мяча

        private Random _random = new Random(); //создаем объект рандом

        //конструкторы
        public Player(string name) //Конструктор, который запрашивает текстовое значение и присваевает к полю "Name"
        {
            Name = name;
        }

        public Player(string name, double x, double y, Team team) //устанавливаем позицию по координатам
        {
            Name = name;
            X = x;
            Y = y;
            Team = team;
        }

        public void SetPosition(double x, double y)//ставит позицию по коардинатам
        {
            X = x;
            Y = y;
        }

        public (double, double) GetAbsolutePosition() //возвращает позицию игрока при условии что Team не равен null
        {
            return Team!.Game.GetPositionForTeam(Team, X, Y);
        }

        public double GetDistanceToBall()//расчитываем растояние до мяча
        {
            var ballPosition = Team!.GetBallPosition();
            var dx = ballPosition.Item1 - X;
            var dy = ballPosition.Item2 - Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public void MoveTowardsBall() //Передвигаем игрока к мячу
        {
            var ballPosition = Team!.GetBallPosition(); //Узнаем позицию мяча 
            var dx = ballPosition.Item1 - X;
            var dy = ballPosition.Item2 - Y;
            var ratio = Math.Sqrt(dx * dx + dy * dy) / MaxSpeed;
            _vx = dx / ratio; //расчитываем путь к мячу
            _vy = dy / ratio;
        }

        public void DrawPlayers(Player player) //отрисовка игрока
        {
            Console.SetCursorPosition(Convert.ToInt32(Math.Round(X)), Convert.ToInt32(Math.Round(Y)));
            Console.Write(player.Name);
        }

        public void Move()
        {
            if (Team.GetClosestPlayerToBall() != this)
            {
                _vx = 0;
                _vy = 0;
            }

            if (GetDistanceToBall() < BallKickDistance)//если расстояние до мяча меньше, чем требуется для удара, то задаем скорость мячу
            {
                Team.SetBallSpeed(
                    MaxKickSpeed * _random.NextDouble(),
                    MaxKickSpeed * (_random.NextDouble() - 0.5)
                    );
            }


            var newX = X + _vx;
            var newY = Y + _vy;
            var newAbsolutePosition = Team.Game.GetPositionForTeam(Team, newX, newY);
            if (Team.Game.Stadium.IsIn(newAbsolutePosition.Item1, newAbsolutePosition.Item2)) //если игрок вышел за поле, то задаем новую позицию
            {
                X = newX;
                Y = newY;
            }
            else
            {
                _vx = _vy = 0;
            }
        }
    }
}
