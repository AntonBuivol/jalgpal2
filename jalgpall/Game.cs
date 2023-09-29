using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jalgpall
{
    public class Game
    {
        public Team HomeTeam { get; } //объект команда местных
        public Team AwayTeam { get; } //команда гостей
        public Stadium Stadium { get; } //стадион
        public Ball Ball { get; private set; }  //мяч

        public Game(Team homeTeam, Team awayTeam, Stadium stadium)
        {
            HomeTeam = homeTeam;
            homeTeam.Game = this; //присваивание значений
            AwayTeam = awayTeam;
            awayTeam.Game = this;
            Stadium = stadium;
        }

        public void Start()
        {
            Ball = new Ball(Stadium.Width / 2, Stadium.Height / 2, this);
            Ball.DrawBall(Ball);
            HomeTeam.StartGameHome(Stadium.Width / 2, Stadium.Height, ConsoleColor.Green);
            AwayTeam.StartGameAway(Stadium.Width / 2, Stadium.Width, Stadium.Height, ConsoleColor.Red);
        }

        private (double, double) GetPositionForAwayTeam(double x, double y) //определение координат для комнады
        {
            return (Stadium.Width - x, Stadium.Height - y);
        }

        public (double, double) GetPositionForTeam(Team team, double x, double y)
        {
            return team == HomeTeam ? (x, y) : GetPositionForAwayTeam(x, y); //определение позиции на основе переданных координат
        }

        public (double, double) GetBallPositionForTeam(Team team)
        {
            return GetPositionForTeam(team, Ball.X, Ball.Y); //получаем позицию мяча для указанной команды
        }

        public void SetBallSpeedForTeam(Team team, double vx, double vy)
        {
            if (team == HomeTeam)
            {
                Ball.SetSpeed(vx, vy); //скорость мяча для команды HomeTeam
            }
            else
            {
                Ball.SetSpeed(-vx, -vy); //скорость мяча для команды AwayTeam
            }
        }
        public void Move() //инициируем движение
        {
            Console.ForegroundColor = ConsoleColor.Green;
            HomeTeam.Move();
            Console.ForegroundColor = ConsoleColor.Red;
            AwayTeam.Move();
            Ball.Move();
            Console.ForegroundColor = ConsoleColor.White;
            Ball.DrawBall(Ball);
        }
    }
}