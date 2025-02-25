using System.ComponentModel;

namespace Base_CafetClash
{
    internal class Program
    {
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

        void AddMatchPlayed(TypeGame GameType, Player Player1, Player Opponent, bool Player1Win, int NbRound, int RoundGap)
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
            Player player = program._PlayerList.GetPlayerByName("Toto");
            Player player2 = program._PlayerList.GetPlayerByName("Titi");
            Player player3 = program._PlayerList.GetPlayerByName("Tété");
            Player player4 = program._PlayerList.GetPlayerByName("Tutu");
            Player player5 = program._PlayerList.GetPlayerByName("Tata");
            Player player6 = program._PlayerList.GetPlayerByName("Tyty");
            Player player7 = program._PlayerList.GetPlayerByName("Pd");
            Player player8 = program._PlayerList.GetPlayerByName("Caca");
            player.MakeAvailable();
            player2.MakeAvailable();
            player3.MakeAvailable();
            player4.MakeAvailable();
            player5.MakeAvailable();
            player6.MakeAvailable();
            player7.MakeAvailable();
            player8.MakeAvailable();
            Player First = program._PlayerList.FindMatch(Difficulte.Easy, TypeGame.PingPong, player);
            Player Second = program._PlayerList.FindMatch(Difficulte.Easy, TypeGame.PingPong, player);
            Player Third = program._PlayerList.FindMatch(Difficulte.Easy, TypeGame.PingPong, player);
            Player Four = program._PlayerList.FindMatch(Difficulte.Fair, TypeGame.PingPong, player);
            Player Five = program._PlayerList.FindMatch(Difficulte.Fair, TypeGame.PingPong, player);
            Player Six = program._PlayerList.FindMatch(Difficulte.Fair, TypeGame.PingPong, player);
            Player Seven = program._PlayerList.FindMatch(Difficulte.Hard, TypeGame.PingPong, player);
            Player Eight = program._PlayerList.FindMatch(Difficulte.Hard, TypeGame.PingPong, player);
            Player Nine = program._PlayerList.FindMatch(Difficulte.Hard, TypeGame.PingPong, player);
            Console.WriteLine("easy match : {0},{1},{2}", First.Name, Second.Name, Third.Name);
            Console.WriteLine("fair match : {0},{1},{2}", Four.Name, Five.Name, Six.Name);
            Console.WriteLine("hard match : {0},{1},{2}", Seven.Name, Eight.Name, Nine.Name);

        }

        private static void InitializePlayerElo(Player player, int elo) //for hardcoding
        {
            player.GameStat[TypeGame.PingPong].Elo = elo;
            FileGestionner.ChangeELo(player);
        }
    }
}
