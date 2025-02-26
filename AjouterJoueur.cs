namespace Base_CafetClash
{
    internal partial class AjouterJoueur : Form
    {
        private bool TextChange = false;
        private PlayerList _Data;
        public AjouterJoueur(PlayerList data)
        {
            InitializeComponent();
            this._Data = data;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (TextChange)
            {
                Player player = new Player(textBox1.Text);
                this._Data.AddPlayer(player);
                this.Close();
            }
            else
            {
                MessageBox.Show("Veuillez entrer un nom");
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextChange = true;
        }
    }
}
