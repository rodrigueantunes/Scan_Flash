using System;
using System.Drawing;
using System.Windows.Forms;

namespace Scan_Flash
{
    public partial class SelectionForm : Form
    {
        // Remplacer la propriété par un champ privé
        private Rectangle _selectedArea;

        public Rectangle SelectedArea
        {
            get { return _selectedArea; }
            private set { _selectedArea = value; }
        }

        private int _width;
        private int _height;

        public SelectionForm(int width, int height)
        {
            InitializeComponent();
            _width = width;
            _height = height;
            SelectedArea = new Rectangle(0, 0, width, height);

            // Rendre le formulaire transparent mais garder un fond noir semi-transparent
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.Black;  // Couleur noire de fond (opaque)
            this.Opacity = 0.5;  // Transparence du formulaire (50%)

            this.StartPosition = FormStartPosition.Manual;

            // Définir la taille de la fenêtre en fonction de l'écran
            this.Width = _width;
            this.Height = _height;

            // Déplacement dynamique
            this.MouseDown += SelectionForm_MouseDown;
            this.MouseMove += SelectionForm_MouseMove;
            this.MouseUp += SelectionForm_MouseUp;
        }
        private void SelectionForm_MouseDown(object sender, MouseEventArgs e)
        {
            // Initialisation du rectangle de sélection lors du clic
            _selectedArea = new Rectangle(e.X, e.Y, 0, 0);
        }

        private void SelectionForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Calculer la nouvelle largeur et hauteur pour que la sélection soit toujours positive
                int width = e.X - _selectedArea.X;
                int height = e.Y - _selectedArea.Y;

                // Si la largeur ou la hauteur est négative, on les inverse
                if (width < 0)
                {
                    _selectedArea.X = e.X;
                    width = -width;
                }

                if (height < 0)
                {
                    _selectedArea.Y = e.Y;
                    height = -height;
                }

                _selectedArea.Width = width;
                _selectedArea.Height = height;

                this.Invalidate(); // Redessiner la fenêtre pour actualiser le rectangle
            }
        }

        private void SelectionForm_MouseUp(object sender, MouseEventArgs e)
        {
            // Terminer la sélection lorsque le bouton de la souris est relâché
            if (_selectedArea.Width > 0 && _selectedArea.Height > 0)
            {
                DialogResult = DialogResult.OK; // Fermer la fenêtre et retourner la sélection
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Dessiner le rectangle de sélection
            if (_selectedArea.Width > 0 && _selectedArea.Height > 0)
            {
                e.Graphics.DrawRectangle(Pens.Red, _selectedArea);
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // SelectionForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "SelectionForm";
            this.Load += new System.EventHandler(this.SelectionForm_Load);
            this.ResumeLayout(false);
        }

        private void SelectionForm_Load(object sender, EventArgs e)
        {
            // Logique lors du chargement du formulaire
        }
    }
}
