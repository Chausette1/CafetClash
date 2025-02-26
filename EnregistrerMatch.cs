using System.Reflection.Emit;

namespace Base_CafetClash
{
    internal partial class EnregistrerMatch : Form
    {
        private PlayerList _data;
        private bool _comboBox1Selected = false;
        private bool _comboBox2Selected = false;
        private bool _comboBox3Selected = false;
        private bool _comboBox4Selected = false;
        private bool _comboBox5Selected = false;
        private bool _comboBox6Selected = false;

        public EnregistrerMatch(PlayerList data)
        {
            this._data = data;
            this._comboBox1Selected = false;
            this._comboBox2Selected = false;
            InitializeComponent();
        }

        private void EnregistrerMatch_Load(object sender, EventArgs e)
        {
            foreach (Player player in this._data.Players)
            {
                comboBox1.Items.Add(player.Name);
            }
            foreach (Player player in this._data.Players)
            {
                comboBox2.Items.Add(player.Name);
            }
            comboBox3.Items.Add("1");
            comboBox3.Items.Add("3");
            comboBox3.Items.Add("5");
            foreach (TypeGame game in Enum.GetValues(typeof(TypeGame)))
            {
                comboBox6.Items.Add(game.ToString());
            }
            comboBox3.Hide();
            label3.Hide();
            label4.Hide();
            button2.Hide();
            label5.Hide();
            comboBox4.Hide();
            comboBox4.Items.Add("0");
            comboBox4.Items.Add("1");
            comboBox4.Items.Add("2");
            comboBox4.Items.Add("3");
            comboBox5.Hide();
            comboBox5.Items.Add("0");
            comboBox5.Items.Add("1");
            comboBox5.Items.Add("2");
            comboBox5.Items.Add("3");
            button3.Hide();
            label6.Hide();
            label7.Hide();
            comboBox6.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _comboBox1Selected = true;
            comboBox2.Items.Remove(comboBox1.Items[comboBox1.SelectedIndex]);
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            _comboBox2Selected = true;
            comboBox1.Items.Remove(comboBox2.Items[comboBox2.SelectedIndex]);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!_comboBox1Selected || !_comboBox2Selected)
            {
                MessageBox.Show("Veuillez sélectionner deux joueurs");
                return;
            }
            Player player1 = this._data.GetPlayerByName(comboBox1.Text);
            Player player2 = this._data.GetPlayerByName(comboBox2.Text);
            comboBox3.Show();
            label3.Show();
            label4.Show();
            button2.Show();
            label7.Show();
            comboBox6.Show();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            _comboBox3Selected = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!_comboBox3Selected || !_comboBox6Selected)
            {
                MessageBox.Show("Veuillez sélectionner le nombre de round");
                return;
            }
            if (comboBox3.Text == "1")
            {
                comboBox4.Items.Remove("2");
                comboBox4.Items.Remove("3");
                comboBox5.Items.Remove("2");
                comboBox5.Items.Remove("3");
            }
            else if (comboBox3.Text == "3")
            {
                comboBox4.Items.Remove("3");
                comboBox5.Items.Remove("3");
            }
            comboBox4.Show();
            comboBox5.Show();
            label6.Show();
            label5.Show();
            button3.Show();
        }


        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            _comboBox4Selected = true;
            comboBox5.Items.Remove(comboBox4.Items[comboBox4.SelectedIndex]);
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            _comboBox5Selected = true;
            comboBox4.Items.Remove(comboBox5.Items[comboBox5.SelectedIndex]);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (!_comboBox4Selected || !_comboBox5Selected)
            {
                MessageBox.Show("Veuillez sélectionner deux joueurs");
                return;
            }
            Player player1 = this._data.GetPlayerByName(comboBox1.Text);
            Player player2 = this._data.GetPlayerByName(comboBox2.Text);
            int NbRound = int.Parse(comboBox3.Items[comboBox3.SelectedIndex].ToString());
            int NbRoundWinByPlayer1 = int.Parse(comboBox5.Items[comboBox5.SelectedIndex].ToString());
            int NbRoundWinByPlayer2 = int.Parse(comboBox4.Items[comboBox4.SelectedIndex].ToString());
            int NbRoundGap = Math.Abs(NbRoundWinByPlayer1 - NbRoundWinByPlayer2);
            TypeGame game = (TypeGame)Enum.Parse(typeof(TypeGame), comboBox6.Text);
            MessageBox.Show($"Match enregistré {NbRoundWinByPlayer1} {NbRoundWinByPlayer2}");
            if (NbRoundWinByPlayer1 > NbRoundWinByPlayer2)
            {
                Program.AddMatchPlayed(game, player1, player2, true, NbRound, NbRoundGap);
            }
            else
            {
                Program.AddMatchPlayed(game, player2, player1, true, NbRound, NbRoundGap);
            }
            this.Close();
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            _comboBox6Selected = true;
        }
    }
}
