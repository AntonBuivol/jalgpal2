namespace jalgpall
{
    public class Program
    {
        public static void Main()
        {
            Console.ForegroundColor= ConsoleColor.White;
            Stadium stadium = new Stadium(80, 20);
            stadium.Draw();
            Team home = new Team("home");
            Team away = new Team("away");
            home.AutoAddPlayerToTeam();
            away.AutoAddPlayerToTeam();
            Game game = new Game(home, away, stadium);
            game.Start();
            Thread.Sleep(1000);
            while (true)
            {
                Console.Clear();
                stadium.Draw();
                game.Move();
                Thread.Sleep(1000);
            }
        }
    }
}