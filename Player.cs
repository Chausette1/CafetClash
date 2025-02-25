namespace Base_CafetClash
{
    class Player
    {
        /*
         * 
         * Classe qui permet de gerer les utilisateur et de calculer l'elo
         * 
         * 
         */
        private string _name;
        private bool _available;
        private Dictionary<TypeGame, GameStat> _gameStats;

        public Player(string name)
        {
            this._name = name;
            this._available = false;
            this._gameStats = new Dictionary<TypeGame, GameStat>();
            this.InitGameProfile();
            FileGestionner.CreatePlayerFile(this);
        }

        public bool Available
        {
            get => this._available;
        }
        public Dictionary<TypeGame, GameStat> GameStat
        {
            get => this._gameStats;
        }

        public string Name
        {
            get => this._name;
        }
        private void InitGameProfile()
        {
            foreach (TypeGame game in Enum.GetValues(typeof(TypeGame)))
            {
                GameStat gameStat = new GameStat();
                _gameStats[game] = gameStat;
            }
        }

        public void MakeAvailable()
        {
            _available = true;
        }

        public void MakeUnavailable()
        {
            _available = false;
        }

        public int AddPlayedMatch(TypeGame Game, bool Win, Player Opponent, int NbRound, int NbRoundGap)
        {
            this._gameStats[Game].AddOpponent(Opponent.Name);
            this._gameStats[Game].AddWin(Win);
            this._gameStats[Game].AddRound(NbRound);
            this._gameStats[Game].AddRoundGap(NbRoundGap);
            this._gameStats[Game].AddGamePlayed();
            int NewElo = this.CalculateElo(Game, Opponent, Win);
            return NewElo;
        }

        public int CalculateElo(TypeGame Game, Player Opponent, bool win)
        {
            /*
             * SI le joueur gagne
             *      contre moins fort que lui (<100) -> +50 - (10 * (tranche de 100 de moins)) /!\ bloqué à 10
             *      contre egal (+-100)              -> +50
             *      contre plus fort (>100)          -> +50 + (10 * (tranche de 100 de plus)) /!\ bloqué à 90
             * 
             * SI le joueur perd
             *      contre moins fort que lui (<100) -> -50 + (-10 * (tranche de 100 de moins)) /!\ bloqué à -90
             *      contre egal (+-100)              -> -50
             *      contre plus fort (>100)          -> -50 + (10 * (tranche de 100 de plus)) /!\ bloqué à -10
             */
            int deltaElo = this._gameStats[Game].Elo - Opponent.GameStat[Game].Elo;
            int Gain = 0;
            if (win)
            {
                if (deltaElo > 100) // plus fort que lui
                {
                    Gain = 50;
                    int TrancheDeCent = deltaElo / 100;
                    Gain -= (10 * TrancheDeCent);
                    if (Gain < 10)
                    {
                        Gain = 10;
                    }
                }
                else if (deltaElo < -100) // moins fort que lui
                {
                    Gain = 50;
                    int TrancheDeCent = (-deltaElo) / 100;
                    Gain += (10 * TrancheDeCent);
                    if (Gain > 90)
                    {
                        Gain = 90;
                    }
                }
                else // égal
                {
                    Gain = 50;
                }
            }
            else
            {
                if (deltaElo > 100) // plus fort que lui
                {
                    Gain = -50;
                    int TrancheDeCent = deltaElo / 100;
                    Gain -= (10 * TrancheDeCent);
                    if (Gain < -90)
                    {
                        Gain = -90;
                    }
                }
                else if (deltaElo < -100) // moins fort que lui
                {
                    Gain = -50;
                    int TrancheDeCent = (-deltaElo) / 100;
                    Gain += (10 * TrancheDeCent);
                    if (Gain > -10)
                    {
                        Gain = -10;
                    }
                }
                else // égal
                {
                    Gain = -50;
                }
            }
            return this._gameStats[Game].Elo + Gain;
        }
    }
}
