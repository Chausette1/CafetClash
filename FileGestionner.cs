using System;
using System.Security.Cryptography.X509Certificates;

namespace Base_CafetClash
{
    class FileGestionner
    {
        private static readonly int EnumSize = Enum.GetNames(typeof(TypeGame)).Length;
        public static PlayerList LoadPlayerFromFile()
        {
            PlayerList listPlayer = new PlayerList();
            List<string> players = new List<string>();
            string path = Environment.CurrentDirectory + "\\main.txt";
            LoadPlayerFile(path, players);
            if (players.Count > 0)
            {
                foreach (string player in players)
                {
                    string playerPath = Environment.CurrentDirectory + ("\\" + player + ".txt");
                    Player newPlayer = FileGestionner.LoadPlayer(playerPath);
                    listPlayer.AddPlayer(newPlayer);
                }
                foreach (Player player in listPlayer.Players)
                {
                    FileGestionner.CalcultateElo(player, listPlayer);
                }
            }
            return listPlayer;
        }

        private static void CalcultateElo(Player player, PlayerList listPlayer)
        {
            foreach (TypeGame game in Enum.GetValues(typeof(TypeGame)))
            {
                for (int i = 0; i < player.GameStat[game].NbGamePlayed; i++)
                {
                    bool win = player.GameStat[game].Win[i];
                    string opponentName = player.GameStat[game].Opponent[i];
                    Player opponent = listPlayer.GetPlayerByName(opponentName);
                    int nbRound = player.GameStat[game].NbRound[i];
                    int nbRoundGap = player.GameStat[game].NbRoundGap[i];
                    player.AddPlayedMatch(game, win, opponent, nbRound, nbRoundGap);
                }
            }
        }

        private static Player LoadPlayer(string path)
        {
            Player player = null;
            using (StreamReader streamReader = new StreamReader(path))
            {
                string name = streamReader.ReadLine();
                player = new Player(name);
                for (int i = 0; i < EnumSize; i++)
                {

                    string gameString = streamReader.ReadLine();
                    TypeGame game;
                    Enum.TryParse(gameString, out game);
                    int elo = int.Parse(streamReader.ReadLine());
                    int nb = int.Parse(streamReader.ReadLine());
                    player.GameStat[game].Elo = elo;
                    player.GameStat[game].NbGamePlayed = nb;
                    string history = streamReader.ReadLine();
                    string[] gamePlayed = history.Split("/");
                    if (gamePlayed[0] != "")
                    {
                        foreach (string gameInfo in gamePlayed)
                        {
                            string[] gameInfoSplit = gameInfo.Split("|");
                            if (gameInfoSplit[0] == " ")
                            {
                                continue;
                            }
                            bool win = bool.Parse(gameInfoSplit[0]);
                            int nbRound = int.Parse(gameInfoSplit[1]);
                            int nbRoundGap = int.Parse(gameInfoSplit[2]);
                            string opponent = gameInfoSplit[3];
                        }
                    }
                }
            }
            return player;
        }

        private static void LoadPlayerFile(string path, List<string> players)
        {
            using (StreamReader streamReader = new StreamReader(path))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    players.Add(line);
                }
            }
        }

        public static void CreatePlayerFile(Player player)
        {
            string fileName = $"{player.Name}.txt";
            string path = Path.Combine(Environment.CurrentDirectory, fileName);
            try
            {
                if (File.Exists(path))
                {
                    return;
                }
                using (StreamWriter streamWriter = new StreamWriter(path))
                {
                    streamWriter.WriteLine(player.Name);
                    string[] enumItem = Enum.GetNames(typeof(TypeGame));
                    for (int index = 0; index < EnumSize; index++)
                    {
                        streamWriter.WriteLine(enumItem[index]);
                        TypeGame game = (TypeGame)Enum.Parse(typeof(TypeGame), enumItem[index]);
                        streamWriter.WriteLine(player.GameStat[game].Elo);
                        streamWriter.WriteLine("0");
                        streamWriter.WriteLine();
                    }
                }
            }
            catch (Exception exeption)
            {
                Console.WriteLine(exeption.Message);
            }
            FileGestionner.AddPlayerToMainFile(player);
        }

        public static void AddMatchToPlayerHistory(Player player, TypeGame game)
        {
            string fileName = $"{player.Name}.txt";
            string path = Path.Combine(Environment.CurrentDirectory, fileName);
            string[] gameList = Enum.GetNames(typeof(TypeGame));
            int indexOfGame = Array.IndexOf(gameList, game.ToString()) + 1;

            try
            {
                string[] lines = File.ReadAllLines(fileName);
                int lineToChange = (4 * indexOfGame);
                int lastGame = player.GameStat[game].NbGamePlayed - 1;
                bool win = player.GameStat[game].Win[lastGame];
                int nbRound = player.GameStat[game].NbRound[lastGame];
                int nbRoundGap = player.GameStat[game].NbRoundGap[lastGame];
                String OpponentName = player.GameStat[game].Opponent[lastGame];
                lines[lineToChange] += $"{win.ToString()} | {nbRound.ToString()} | {nbRoundGap.ToString()} | {OpponentName} / ";
                int eloToChange = (4 * indexOfGame) - 2;
                lines[eloToChange] = $"{player.GameStat[game].Elo.ToString()}";
                File.WriteAllLines(path, lines);
            }
            catch (Exception exeption)
            {
                Console.WriteLine(exeption.Message);
            }
        }

        private static void AddPlayerToMainFile(Player player)
        {

            string fileName = "main.txt";
            string path = Path.Combine(Environment.CurrentDirectory, fileName);
            if (File.Exists(path))
            {
                string[] playerName = File.ReadLines(path).ToArray();
                try
                {
                    using (StreamWriter writer = new StreamWriter(path, true))
                    {
                        if (!playerName.Contains(player.Name))
                        {
                            writer.WriteLine(player.Name);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur : " + ex.Message);
                }
            }
            else
            {
                try
                {
                    using (StreamWriter streamWriter = new StreamWriter(path))
                    {
                        streamWriter.WriteLine(player.Name);
                    }
                }
                catch (Exception exeption)
                {
                    Console.WriteLine(exeption.Message);
                }
            }
        }

        public static void ChangeELo(Player player)
        {
            TypeGame game = TypeGame.PingPong;
            string fileName = $"{player.Name}.txt";
            string path = Path.Combine(Environment.CurrentDirectory, fileName);
            string[] gameList = Enum.GetNames(typeof(TypeGame));
            int indexOfGame = Array.IndexOf(gameList, game.ToString()) + 1;
            try
            {
                string[] lines = File.ReadAllLines(fileName);
                int eloToChange = (2 * indexOfGame);
                lines[eloToChange] = $"{player.GameStat[game].Elo.ToString()}";
                File.WriteAllLines(path, lines);
            }
            catch (Exception exeption)
            {
                Console.WriteLine(exeption.Message);
            }
        }
    }
}
