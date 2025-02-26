using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Base_CafetClash
{
    internal class Program
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool AllocConsole();
        /*
         * 
         * Classe programme qui permet de lancer le programme
         * et de gérer les joueurs et les matchs
         * 
         * 
         */
        public PlayerList _PlayerList;
        public Program()
        {
            _PlayerList = new PlayerList();
            _PlayerList = FileGestionner.LoadPlayerFromFile();
        }
        public static void AddMatchPlayed(TypeGame GameType, Player Player1, Player Opponent, bool Player1Win, int NbRound, int RoundGap)
        {
            int NewEloPlayer1 = Player1.AddPlayedMatch(GameType, Player1Win, Opponent, NbRound, RoundGap);
            int NewEloOpponent = Opponent.AddPlayedMatch(GameType, !Player1Win, Player1, NbRound, RoundGap);
            Player1.GameStat[GameType].Elo = NewEloPlayer1;
            Opponent.GameStat[GameType].Elo = NewEloOpponent;
            FileGestionner.AddMatchToPlayerHistory(Player1, GameType);
            FileGestionner.AddMatchToPlayerHistory(Opponent, GameType);
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            //AllocConsole(); //for debugging
            foreach (Player playerToShow in program._PlayerList.Players)
            {
                Console.WriteLine("player load : ");
                Console.WriteLine(playerToShow.Name);
                foreach (TypeGame game in Enum.GetValues(typeof(TypeGame)))
                {
                    Console.WriteLine("game name: {0}", game);
                    Console.WriteLine("elo : {0}", playerToShow.GameStat[game].Elo);
                    Console.WriteLine("nb game played : {0}", playerToShow.GameStat[game].NbGamePlayed);
                }
                Console.WriteLine("----------------------------------");
            }
            ApplicationConfiguration.Initialize();
            Application.Run(new MainPage(program._PlayerList));

        }

        private static void InitializePlayerElo(Player player, int elo) //for hardcoding
        {
            player.GameStat[TypeGame.PingPong].Elo = elo;
            FileGestionner.ChangeELo(player);
        }
    }
}
