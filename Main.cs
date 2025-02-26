using System.Diagnostics;

namespace Base_CafetClash
{
    internal partial class MainPage : Form
    {
        private PlayerList _data;

        public MainPage(PlayerList data)
        {
            InitializeComponent();
            this._data = data;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AjouterJoueur ajouterJoueurForm = new AjouterJoueur(_data);
            ajouterJoueurForm.ShowDialog();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            MatchMaking matchMakingForm = new MatchMaking(_data);
            matchMakingForm.ShowDialog();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            EnregistrerMatch ajouterMatchForm = new EnregistrerMatch(_data);
            ajouterMatchForm.ShowDialog();
        }


        private void InitializeComponent()
        {
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(207, 42);
            button1.Name = "button1";
            button1.Size = new Size(355, 48);
            button1.TabIndex = 0;
            button1.Text = "Créer un joueur";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(207, 113);
            button2.Name = "button2";
            button2.Size = new Size(355, 46);
            button2.TabIndex = 1;
            button2.Text = "Trouvez un match";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(207, 189);
            button3.Name = "button3";
            button3.Size = new Size(355, 46);
            button3.TabIndex = 2;
            button3.Text = "Ajouter un match jouer";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // MainPage
            // 
            ClientSize = new Size(771, 308);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "MainPage";
            Text = "CafetClash";
            ResumeLayout(false);
        }


        private Button button1;
        private Button button2;
        private Button button3;
    }
}
