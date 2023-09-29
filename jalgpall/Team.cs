using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jalgpall
{
    public class Team
    {
        public List<Player> Players { get; } = new List<Player>(); //создает список объектов Player
        public string Name { get; private set; }
        public Game Game { get; set; }

        public Team(string name)
        {
            Name = name;
        }
        public void StartGameHome(int width, int height, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Random rnd = new Random(); //создаем объект Random для генерации случайных чисел
            foreach (var player in Players)
            {
                player.SetPosition(
                    rnd.NextDouble() * width,
                    rnd.NextDouble() * height
                    );
                player.DrawPlayers(player);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void StartGameAway(int widthFrom, int widthTo, int height, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Random rnd = new Random();
            foreach (var player in Players)
            {
                player.SetPosition(
                    rnd.Next(widthFrom, widthTo), //рандомно генерирет целое число от половины ширины поля и до его конца
                    rnd.NextDouble() * height
                    );
                player.DrawPlayers(player);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }


        public void AddPlayer(Player player) //Добавляем игрока в команду
        {
            if (player.Team != null) return;
            Players.Add(player);
            player.Team = this;
        }

        public void AutoAddPlayerToTeam()
        {
            for (int i = 1; i < 11; i++)
            {
                AddPlayer(new Player(""+i));
            }
        }

        public (double, double) GetBallPosition() //получаем координаты мяча
        {
            return (Game.Ball.X, Game.Ball.Y);
        }

        public void SetBallSpeed(double vx, double vy) //устанавливаем скорость мяча
        {
            Game.SetBallSpeedForTeam(this, vx, vy);
        }

        public Player GetClosestPlayerToBall() // Распознование самого ближайшего к мячу игрока
        {
            Player closestPlayer = Players[0];
            double bestDistance = Double.MaxValue;
            foreach (var player in Players)
            {
                var distance = player.GetDistanceToBall();
                if (distance < bestDistance)
                {
                    closestPlayer = player;
                    bestDistance = distance;
                }
            }
            return closestPlayer;
        }


        public void Move() //двигаем игрока к мячу
        {
            GetClosestPlayerToBall().MoveTowardsBall();
            Players.ForEach(player => player.Move());
            foreach (var player in Players)
            {
                player.DrawPlayers(player);
            }
        }
    }
}
