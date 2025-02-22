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
        }
        /*        private int AskNbRound()
                {
                    int NbRound = 0;
                    while (true)
                    {
                        Console.WriteLine("Combien de manche ont étais jouer ? :");
                        string buffer = "";
                        buffer = Console.ReadLine();
                        if (int.TryParse(buffer, out NbRound))
                        {
                            if (NbRound == 1 || NbRound == 3 || NbRound == 5)
                            {
                                return NbRound;
                            }
                        }
                        Console.WriteLine("veuillez rentrez un nombre valide (1, 3, 5):");
                    }
                }
                private int AskWin()
                {
                    int Choice;
                    while (true)
                    {
                        Console.WriteLine("Combien de manche ont étais jouer ?");
                        string buffer = "";
                        buffer = Console.ReadLine();
                        if (int.TryParse(buffer, out Choice))
                        {
                            if (Choice == 1 || Choice == 2)
                            {
                                return Choice;
                            }
                        }
                        Console.WriteLine("veuillez rentrez un nombre entre 1 et 2 :");
                    }
                }

                public void AddPlayedMatch(TypeGame GameType, Player Player1, Player Opponent)
                {
                    *//*
                     *
                     * a terminer rename askpoint en askwin
                     *//*
                    int NbRound = AskNbRound();
                    int NbRoundWinByPlayer1 = 0;
                    int NbRoundWinByPlayer2 = 0;
                    for (int numberOfRound = 0; numberOfRound < NbRound; numberOfRound++)
                    {
                        Console.WriteLine("Qui à gagné le round 1 (1: {1}, 2: {2}) :", Player1.Name, Opponent.Name);
                        int Choice = AskWin();
                        if (Choice == 1)
                        {
                            NbRoundWinByPlayer1++;
                        }
                        else
                        {
                            NbRoundWinByPlayer2++;
                        }
                    }
                    bool Player1Win = NbRoundWinByPlayer1 > NbRoundWinByPlayer2;
                    int RoundGap = NbRoundWinByPlayer1 - NbRoundWinByPlayer2;
                    Player1.AddPlayedMatch(GameType, Player1Win, Opponent, NbRound, RoundGap);
                    Opponent.AddPlayedMatch(GameType, !Player1Win, Player1, NbRound, RoundGap);
                }*/

        void AddMatchPlayed(TypeGame GameType, Player Player1, Player Opponent, bool Player1Win, int NbRound, int RoundGap)
        {
            Console.WriteLine("Match played between {0} and {1}, Player1 win : {2} with {3} round gap in {4} round", Player1.Name, Opponent.Name, Player1Win, RoundGap, NbRound);
            int NewEloPlayer1 = Player1.AddPlayedMatch(GameType, Player1Win, Opponent, NbRound, RoundGap);
            int NewEloOpponent = Opponent.AddPlayedMatch(GameType, !Player1Win, Player1, NbRound, RoundGap);
            Player1.GameStat[GameType].Elo = NewEloPlayer1;
            Opponent.GameStat[GameType].Elo = NewEloOpponent;
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            Player player = new("Toto");
            Player player2 = new("Titi");
            Player player3 = new("Tété");
            Player player4 = new("Tutu");
            Player player5 = new("Tata");
            Player player6 = new("Tyty");
            Player player7 = new("Pd");
            Player player8 = new("Caca");
            program._PlayerList.AddPlayer(player);
            program._PlayerList.AddPlayer(player2);
            program._PlayerList.AddPlayer(player3);
            program._PlayerList.AddPlayer(player4);
            program._PlayerList.AddPlayer(player5);
            program._PlayerList.AddPlayer(player6);
            program._PlayerList.AddPlayer(player7);
            program._PlayerList.AddPlayer(player8);
            int EloPlayer1 = player.GameStat[TypeGame.PingPong].Elo;
            int EloPlayer2 = player2.GameStat[TypeGame.PingPong].Elo;
            Console.WriteLine(EloPlayer1);
            Console.WriteLine(EloPlayer2);
            program.AddMatchPlayed(TypeGame.PingPong, player, player2, true, 5, 3);
            EloPlayer1 = player.GameStat[TypeGame.PingPong].Elo;
            EloPlayer2 = player2.GameStat[TypeGame.PingPong].Elo;
            Console.WriteLine(EloPlayer1);
            Console.WriteLine(EloPlayer2);
            program.AddMatchPlayed(TypeGame.PingPong, player, player2, true, 5, 1);
            EloPlayer1 = player.GameStat[TypeGame.PingPong].Elo;
            EloPlayer2 = player2.GameStat[TypeGame.PingPong].Elo;
            Console.WriteLine(EloPlayer1);
            Console.WriteLine(EloPlayer2);
            program.AddMatchPlayed(TypeGame.PingPong, player, player2, true, 3, 2);
            EloPlayer1 = player.GameStat[TypeGame.PingPong].Elo;
            EloPlayer2 = player2.GameStat[TypeGame.PingPong].Elo;
            Console.WriteLine(EloPlayer1);
            Console.WriteLine(EloPlayer2);
            program.AddMatchPlayed(TypeGame.PingPong, player, player2, false, 5, 3);
            EloPlayer1 = player.GameStat[TypeGame.PingPong].Elo;
            EloPlayer2 = player2.GameStat[TypeGame.PingPong].Elo;
            Console.WriteLine(EloPlayer1);
            Console.WriteLine(EloPlayer2);
            program.AddMatchPlayed(TypeGame.PingPong, player, player2, true, 5, 3);
            EloPlayer1 = player.GameStat[TypeGame.PingPong].Elo;
            EloPlayer2 = player2.GameStat[TypeGame.PingPong].Elo;
            Console.WriteLine(EloPlayer1);
            Console.WriteLine(EloPlayer2);
            player.MakeAvailable();
            player2.MakeAvailable();
            player3.MakeAvailable();
            player4.MakeAvailable();
            player5.MakeAvailable();
            player6.MakeAvailable();
            player7.MakeAvailable();
            player8.MakeAvailable();
            player3.GameStat[TypeGame.PingPong].Elo = 800;
            player4.GameStat[TypeGame.PingPong].Elo = 1100;
            player5.GameStat[TypeGame.PingPong].Elo = 1500;
            player6.GameStat[TypeGame.PingPong].Elo = 1200;
            player7.GameStat[TypeGame.PingPong].Elo = 1800;
            player8.GameStat[TypeGame.PingPong].Elo = 1800;
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
    }
}
