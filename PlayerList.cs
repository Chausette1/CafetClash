﻿using System.Runtime.CompilerServices;

namespace Base_CafetClash
{
    class PlayerList
    {
        /*
         * 
         * Classe qui list les joueurs et qui permet le match making
         * 
         * 
         */

        private List<Player> _players;
        public PlayerList()
        {
            _players = new List<Player>();
        }

        public List<Player> Players
        {
            get => _players;
        }
        public void AddPlayer(Player player)
        {
            try
            {
                for (int i = 0; i < _players.Count; i++)
                {
                    if (_players[i].Name == player.Name)
                    {
                        throw new Exception("player already exist");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur : {0}", ex.Message);
                return;
            }
            _players.Add(player);
        }

        public Player? GetPlayerByName(string name)
        {
            Player player = null;
            player = _players.Find(player => player.Name == name);
            return player;
        }
        private bool CheckFiveGamePlayed(TypeGame game, Player opponent)
        {
            if (opponent.GameStat[game].NbGamePlayed > 5) // si on a joué plus de 5 game
            {
                return true;
            }
            return false;
        }
        private void CheckFiveLose(TypeGame game, List<Player> PotentialOpponent, Player opponent)
        {
            int NbLose = 0;
            int Last5GamePlayed = 5;
            if (this.CheckFiveGamePlayed(game, opponent))
            {
                for (int i = 0; i < Last5GamePlayed; i++)
                {
                    bool win = opponent.GameStat[game].Win[i];
                    if (!win)
                    {
                        NbLose++;
                    }
                }
            }
            if (NbLose >= 3 && opponent.Available)
            {
                PotentialOpponent.Add(opponent);
            }
        }
        private void CheckFiveWin(TypeGame game, List<Player> PotentialOpponent, Player opponent)
        {
            int NbWin = 0;
            int Last5GamePlayed = 5;
            if (CheckFiveGamePlayed(game, opponent))
            {
                for (int i = 0; i < Last5GamePlayed; i++)
                {
                    bool win = opponent.GameStat[game].Win[i];
                    if (win)
                    {
                        NbWin++;
                    }
                }
            }
            if (NbWin >= 3 && opponent.Available)
            {
                PotentialOpponent.Add(opponent);
            }
        }


        private Player ChoseGoodOpponentInList(List<Player> PotentialOpponent, TypeGame game, Player player, Difficulte difficulte)
        {
            /*
             * on tire un aléatoire entre 1 et 100, si on est entre 1 et 80  on prend un des joueurs entre 
             * 100 et 300 de diff/les plus ou moins fort qui gagnent/perdent, entre 81 et 90 on prend un 
             * joueur entre 300 et 500 de diff et entre 91 et 100 le reste.
             */
            Random rand = new Random();
            Player opponentFind = null;
            if (!(difficulte == Difficulte.Fair))
            {
                int random = rand.Next(1, 101);
                if (random <= 80)
                {
                    List<Player> PotentialOpponent2 = new List<Player>();
                    foreach (Player opponent in PotentialOpponent)
                    {
                        int deltaElo = player.GameStat[game].Elo - opponent.GameStat[game].Elo;
                        if (Math.Abs(deltaElo) >= 300 || this.CheckFiveGamePlayed(game, opponent))
                        {
                            PotentialOpponent2.Add(opponent);
                        }
                    }
                    PotentialOpponent = PotentialOpponent2;
                }
                else if (random <= 90)
                {
                    List<Player> PotentialOpponent2 = new List<Player>();
                    foreach (Player opponent in PotentialOpponent)
                    {
                        int deltaElo = player.GameStat[game].Elo - opponent.GameStat[game].Elo;
                        if (Math.Abs(deltaElo) >= 500)
                        {
                            PotentialOpponent2.Add(opponent);
                        }
                    }
                    PotentialOpponent = PotentialOpponent2;
                }
                else
                {
                    List<Player> PotentialOpponent2 = new List<Player>();
                    foreach (Player opponent in PotentialOpponent)
                    {
                        int deltaElo = player.GameStat[game].Elo - opponent.GameStat[game].Elo;
                        PotentialOpponent2.Add(opponent);
                    }
                    PotentialOpponent = PotentialOpponent2;
                }
            }
            if (PotentialOpponent.Count > 0)
            {
                int index = rand.Next(PotentialOpponent.Count);
                opponentFind = PotentialOpponent[index];
            }
            return opponentFind;
        }

        public Player FindMatch(Difficulte difficulte, TypeGame NameGame, Player player) //Algorithm to find a match
        {
            Player Opponent = null;
            switch (difficulte)
            {
                case Difficulte.Easy:
                    Opponent = FindEasyMatch(player, NameGame);
                    break;
                case Difficulte.Fair:
                    Opponent = FindFairMatch(player, NameGame);
                    break;
                case Difficulte.Hard:
                    Opponent = FindHardMatch(player, NameGame);
                    break;
            }
            return Opponent;
        }

        private Player FindEasyMatch(Player player, TypeGame game)
        {
            // Trouver un match facile
            /*
             * 100 élo de moins ou élo similaire avec défaite récement (3 defaite les 5 derinière game)
             */
            List<Player> PotentialOpponent = new List<Player>();
            foreach (Player opponent in _players)
            {
                if (opponent.Name == player.Name) // on ne peut pas jouer contre soi-même
                {
                    continue;
                }

                int deltaElo = player.GameStat[game].Elo - opponent.GameStat[game].Elo;

                if (deltaElo > 100 && opponent.Available) //si moins de 100 elo de diff
                {
                    PotentialOpponent.Add(opponent);
                    continue;
                }
                else if (Math.Abs(deltaElo) >= 100) //on est élo similaire
                {
                    CheckFiveLose(game, PotentialOpponent, opponent);
                }
            }
            // joueur trouvé
            Player opponentFind = ChoseGoodOpponentInList(PotentialOpponent, game, player, Difficulte.Easy);
            return opponentFind;
        }

        private Player FindFairMatch(Player player, TypeGame game)
        {
            // Trouver un match équilibré
            /*
             * moins de 100 élo de diff ou
             * low 100 élo avec 3w pour 5g
             * hihg 100 élo avec 3l pour 5g
             */
            List<Player> PotentialOpponent = new List<Player>();
            foreach (Player opponent in _players)
            {
                if (opponent.Name == player.Name) // on ne peut pas jouer contre soi-même
                {
                    continue;
                }

                int deltaElo = player.GameStat[game].Elo - opponent.GameStat[game].Elo;

                if (Math.Abs(deltaElo) <= 100 && opponent.Available) //si moins de 100 elo de diff
                {
                    PotentialOpponent.Add(opponent);
                    continue;
                }
                else if (deltaElo >= -100) //si plus de 100 de diff
                {
                    CheckFiveLose(game, PotentialOpponent, opponent);
                }
                else if (deltaElo <= 100)
                {
                    CheckFiveWin(game, PotentialOpponent, opponent);
                }
            }
            // joueur trouvé
            Player opponentFind = ChoseGoodOpponentInList(PotentialOpponent, game, player, Difficulte.Fair);
            return opponentFind;
        }

        private Player FindHardMatch(Player player, TypeGame game)
        {
            // Trouver un match difficile
            /*
             * plus de 100 élo de diff 
             * same elo avec 3w pour 5g
             *
             */
            List<Player> PotentialOpponent = new List<Player>();
            foreach (Player opponent in _players)
            {
                if (opponent.Name == player.Name) // on ne peut pas jouer contre soi-même
                {
                    continue;
                }

                int deltaElo = player.GameStat[game].Elo - opponent.GameStat[game].Elo;

                if (deltaElo < -100 && opponent.Available) //si 100 elo de plus
                {
                    PotentialOpponent.Add(opponent);
                    continue;
                }
                else if (Math.Abs(deltaElo) <= 100) //on est élo similaire
                {
                    CheckFiveWin(game, PotentialOpponent, opponent);
                }
            }
            // joueur trouvé
            Player opponentFind = ChoseGoodOpponentInList(PotentialOpponent, game, player, Difficulte.Hard);
            return opponentFind;
        }
    }
}
