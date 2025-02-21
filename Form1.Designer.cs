using System;
using System.Windows.Forms;
using System.Drawing;

namespace Scan_Flash
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        private void InitializeComponent()
        {
            this.ScanFullScreenButton = new System.Windows.Forms.Button();
            this.ScanSelectedAreaButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.screenSelectionGroupBox = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ScanFullScreenButton
            // 
            this.ScanFullScreenButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.ScanFullScreenButton.FlatAppearance.BorderSize = 0;
            this.ScanFullScreenButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(149)))), ((int)(((byte)(237)))));
            this.ScanFullScreenButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ScanFullScreenButton.ForeColor = System.Drawing.Color.White;
            this.ScanFullScreenButton.Location = new System.Drawing.Point(12, 240);
            this.ScanFullScreenButton.Name = "ScanFullScreenButton";
            this.ScanFullScreenButton.Size = new System.Drawing.Size(216, 50);
            this.ScanFullScreenButton.TabIndex = 0;
            this.ScanFullScreenButton.Text = "Scanner écran entier";
            this.ScanFullScreenButton.UseVisualStyleBackColor = false;
            this.ScanFullScreenButton.Click += new System.EventHandler(this.ScanFullScreenButton_Click);
            this.ScanFullScreenButton.MouseEnter += new System.EventHandler(this.ScanFullScreenButton_MouseEnter);
            this.ScanFullScreenButton.MouseLeave += new System.EventHandler(this.ScanFullScreenButton_MouseLeave);
            // 
            // ScanSelectedAreaButton
            // 
            this.ScanSelectedAreaButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(179)))), ((int)(((byte)(113)))));
            this.ScanSelectedAreaButton.FlatAppearance.BorderSize = 0;
            this.ScanSelectedAreaButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(160)))), ((int)(((byte)(100)))));
            this.ScanSelectedAreaButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ScanSelectedAreaButton.ForeColor = System.Drawing.Color.White;
            this.ScanSelectedAreaButton.Location = new System.Drawing.Point(12, 175);
            this.ScanSelectedAreaButton.Name = "ScanSelectedAreaButton";
            this.ScanSelectedAreaButton.Size = new System.Drawing.Size(216, 50);
            this.ScanSelectedAreaButton.TabIndex = 1;
            this.ScanSelectedAreaButton.Text = "Scanner zone sélectionnée";
            this.ScanSelectedAreaButton.UseVisualStyleBackColor = false;
            this.ScanSelectedAreaButton.Click += new System.EventHandler(this.ScanSelectedAreaButton_Click);
            this.ScanSelectedAreaButton.MouseEnter += new System.EventHandler(this.ScanSelectedAreaButton_MouseEnter);
            this.ScanSelectedAreaButton.MouseLeave += new System.EventHandler(this.ScanSelectedAreaButton_MouseLeave);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::Scan_Flash.Properties.Resources.volume_software;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(234, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(264, 296);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // screenSelectionGroupBox
            // 
            this.screenSelectionGroupBox.Location = new System.Drawing.Point(12, 12);
            this.screenSelectionGroupBox.Name = "screenSelectionGroupBox";
            this.screenSelectionGroupBox.Size = new System.Drawing.Size(216, 19);
            this.screenSelectionGroupBox.TabIndex = 2;
            this.screenSelectionGroupBox.TabStop = false;
            this.screenSelectionGroupBox.Text = "Sélectionner l\'écran";
            this.screenSelectionGroupBox.Visible = false;
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(500, 320);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.screenSelectionGroupBox);
            this.Controls.Add(this.ScanSelectedAreaButton);
            this.Controls.Add(this.ScanFullScreenButton);
            this.Name = "Form1";
            this.Text = "Scan Flash / Antunes Rodrigue";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ScanFullScreenButton;
        private System.Windows.Forms.Button ScanSelectedAreaButton;

        // Gestion des effets de survol pour les boutons
        private void ScanFullScreenButton_MouseEnter(object sender, EventArgs e)
        {
            this.ScanFullScreenButton.BackColor = System.Drawing.Color.FromArgb(100, 149, 237); // Bleu clair au survol
        }

        private void ScanFullScreenButton_MouseLeave(object sender, EventArgs e)
        {
            this.ScanFullScreenButton.BackColor = System.Drawing.Color.FromArgb(70, 130, 180); // Bleu moderne
        }

        private void ScanSelectedAreaButton_MouseEnter(object sender, EventArgs e)
        {
            this.ScanSelectedAreaButton.BackColor = System.Drawing.Color.FromArgb(85, 160, 100); // Vert clair au survol
        }

        private void ScanSelectedAreaButton_MouseLeave(object sender, EventArgs e)
        {
            this.ScanSelectedAreaButton.BackColor = System.Drawing.Color.FromArgb(60, 179, 113); // Vert moderne
        }

        // Méthode pour créer dynamiquement les boutons radio en fonction des écrans disponibles
        private void CreateScreenSelectionRadios()
        {
            int screenCount = Screen.AllScreens.Length;

            // Effacer les anciens boutons radio si présents
            screenSelectionGroupBox.Controls.Clear();

            for (int i = 0; i < screenCount; i++)
            {
                RadioButton radioButton = new RadioButton
                {
                    Location = new Point(10, 20 + (i * 30)),
                    Name = "screenRadioButton" + i,
                    Text = "Écran " + (i + 1),
                    Tag = i, // Associe l'index de l'écran à l'objet radioButton
                    Checked = i == 0 // Sélectionne le premier écran par défaut
                };
                screenSelectionGroupBox.Controls.Add(radioButton);
            }
        }

        // Définir la forme arrondie pour les boutons
        [System.Runtime.InteropServices.DllImport("gdi32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        private PictureBox pictureBox1;
        private GroupBox screenSelectionGroupBox;
    }
}
