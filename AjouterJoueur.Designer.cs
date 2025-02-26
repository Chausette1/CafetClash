namespace Base_CafetClash
{
    partial class AjouterJoueur : Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBox1 = new TextBox();
            button1 = new Button();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(179, 192);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(323, 39);
            textBox1.TabIndex = 0;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // button1
            // 
            button1.Location = new Point(568, 192);
            button1.Name = "button1";
            button1.Size = new Size(328, 46);
            button1.TabIndex = 1;
            button1.Text = "Validée";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // AjouterJoueur
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1244, 450);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Name = "AjouterJoueur";
            Text = "AjouterJoueur";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Button button1;
    }
}