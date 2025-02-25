namespace Base_CafetClash
{
    class GameStat
    {

        /*
         * 
         * Classe qui list stats par rapport a un jeu
         * 
         * 
         */
        private List<String> _Opponent;
        private List<bool> _Win; // resultat de chaque match
        private List<int> _NbRound; //savoir le nombre de partie qui ont été joué
        private List<int> _NbRoundGap; // ecart de manche entre les deux joueurs
        private int _Elo; // elo du joueur
        private int _NbGamePlayed; // nombre de game joué

        /*
        Flechette
        elo
        nb, win, Nbround, roundGap, Opponent
        1#  T,   3,       1,        Opponentt.name
        2#  ...
        ...
         */

        public GameStat()
        {
            this._Elo = 1000;
            this._Opponent = new List<String>();
            this._Win = new List<bool>();
            this._NbRound = new List<int>();
            this._NbRoundGap = new List<int>();
            this._NbGamePlayed = 0;
        }

        public string[] Opponent
        {
            get => this._Opponent.ToArray();
        }

        public int Elo
        {
            get => this._Elo;
            set => this._Elo = value;
        }

        public List<bool> Win
        {
            get => this._Win;
        }
        public int NbGamePlayed
        {
            get => this._NbGamePlayed;
            set => this._NbGamePlayed = value;
        }
        public int[] NbRound
        {
            get => this._NbRound.ToArray();
        }

        public int[] NbRoundGap
        {
            get => this._NbRoundGap.ToArray();
        }

        public void AddWin(bool win)
        {
            this._Win.Add(win);
        }

        public void AddOpponent(String opponent)
        {
            this._Opponent.Add(opponent);
        }

        public void AddRound(int round)
        {
            this._NbRound.Add(round);
        }

        public void AddRoundGap(int roundGap)
        {
            this._NbRoundGap.Add(roundGap);
        }
        public void AddGamePlayed()
        {
            this._NbGamePlayed++;
        }

    }
}