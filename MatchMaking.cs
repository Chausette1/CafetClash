namespace Base_CafetClash
{
    internal partial class MatchMaking : Form
    {
        private PlayerList _data;
        private bool _comboBox1Selected = false;
        private bool _comboBox2Selected = false;
        private bool _comboBox3Selected = false;
        private Player _playerSelected;
        private TypeGame _gameSelected;
        private Difficulte _difficultiesSelected;

        public MatchMaking(PlayerList data)
        {
            this._data = data;
            InitializeComponent();
            foreach (Player player in this._data.Players)
            {
                comboBox1.Items.Add(player.Name);
                checkedListBox1.Items.Add(player.Name);
            }
            foreach (TypeGame game in Enum.GetValues(typeof(TypeGame)))
            {
                comboBox2.Items.Add(game.ToString());
            }
            foreach (Difficulte difficulty in Enum.GetValues(typeof(Difficulte)))
            {
                comboBox3.Items.Add(difficulty.ToString());
            }
            _playerSelected = null;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _comboBox1Selected = true;
            _playerSelected = _data.GetPlayerByName(comboBox1.SelectedItem.ToString());
            checkedListBox1.Items.Remove(_playerSelected.Name);
            if (_comboBox2Selected)
            {
                label4.Text = _playerSelected.GameStat[_gameSelected].Elo.ToString();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            _comboBox2Selected = true;
            _gameSelected = (TypeGame)Enum.Parse(typeof(TypeGame), comboBox2.SelectedItem.ToString());
            if (_comboBox1Selected)
            {
                label4.Text = _playerSelected.GameStat[_gameSelected].Elo.ToString();
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            _comboBox3Selected = true;
            _difficultiesSelected = (Difficulte)Enum.Parse(typeof(Difficulte), comboBox3.SelectedItem.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!_comboBox1Selected || !_comboBox2Selected)
            {
                MessageBox.Show("Veuillez selectionner un joueur et un jeu");
                return;
            }
            Player Opponent = _data.FindMatch(_difficultiesSelected, _gameSelected, _playerSelected);
            if (Opponent == null)
            {
                MessageBox.Show("Aucun adversaire trouvé");
                this.Close();
            }
            else
            {
                label2.Text = $"{Opponent.Name} {Opponent.GameStat[_gameSelected].Elo}";
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Player selectedPlayer = _data.GetPlayerByName(checkedListBox1.SelectedItem.ToString());
            if (!selectedPlayer.Available)
            {
                selectedPlayer.MakeAvailable();
                checkedListBox1.SetItemChecked(checkedListBox1.SelectedIndex, true);
            }
            else
            {
                selectedPlayer.MakeUnavailable();
                checkedListBox1.SetItemChecked(checkedListBox1.SelectedIndex, false);
            }
        }

    }
}
