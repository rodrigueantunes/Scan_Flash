using System;
using System.Drawing;
using System.Windows.Forms;
using WindowsInput;  // InputSimulator
using ZXing;        // BarcodeScanner
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Linq;

namespace Scan_Flash
{
    public partial class Form1 : Form
    {
        private readonly InputSimulator _inputSimulator = new InputSimulator();

        // Constantes pour l'API Windows
        const int MOD_CTRL = 0x0002; // Modificateur pour Ctrl
        const int VK_F1 = 0x70;  // Code virtuel pour F1

        // Identifiant unique pour l'enregistrement de la touche
        private const int HotKeyId = 1;

        // Pointeur vers l'API RegisterHotKey
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        // Pointeur vers l'API UnregisterHotKey
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            this.FormClosing += Form1_FormClosing;
        }

        // Lorsque le formulaire est chargé, on enregistre le raccourci clavier
        private void Form1_Load(object sender, EventArgs e)
        {
            IntPtr hwnd = this.Handle;
            RegisterHotKey(hwnd, HotKeyId, MOD_CTRL, VK_F1); // Enregistrer Ctrl + F1
            InitializeScreenRadios(); // Initialiser les radios en fonction des écrans disponibles
        }

        // Lorsque le formulaire se ferme, on désenregistre le raccourci clavier
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            IntPtr hwnd = this.Handle;
            UnregisterHotKey(hwnd, HotKeyId); // Désenregistrer Ctrl + F1
        }

        // Méthode pour écouter la combinaison de touches Ctrl + F1
        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;
            if (m.Msg == WM_HOTKEY)
            {
                if (m.WParam.ToInt32() == HotKeyId)
                {
                    Screen selectedScreen = GetSelectedScreen(); // Obtenir l'écran sélectionné
                    if (selectedScreen != null)
                    {
                        OpenManualSelection(selectedScreen);  // Appeler la méthode pour ouvrir la sélection de zone
                    }
                    else
                    {
                        MessageBox.Show("Aucun écran sélectionné.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            base.WndProc(ref m);
        }

        private void ScanFullScreenButton_Click(object sender, EventArgs e)
        {
            try
            {
                var screenCapture = BarcodeScanner.CaptureScreen();
                var barcodes = BarcodeScanner.DecodeAllBarcodes(screenCapture);

                if (barcodes.Count > 0)
                {
                    MessageBox.Show("Codes détectés :\n" + string.Join("\n", barcodes));
                }
                else
                {
                    MessageBox.Show("Aucun code-barres détecté.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de capture d'écran : " + ex.Message);
            }
        }

        private void OpenManualSelection()
        {
            Screen selectedScreen = GetSelectedScreen();
            OpenSelectionFormForScreen(selectedScreen);
        }


        private Screen selectedScreen;
        

        private void OpenSelectionFormForScreen(Screen selectedScreen)
        {
            int width = selectedScreen.Bounds.Width;
            int height = selectedScreen.Bounds.Height;

            SelectionForm selectionWindow = new SelectionForm(width, height);
            selectionWindow.StartPosition = FormStartPosition.Manual;
            selectionWindow.Location = selectedScreen.Bounds.Location;

            if (selectionWindow.ShowDialog() == DialogResult.OK)
            {
                if (selectionWindow.SelectedArea.Width > 0 && selectionWindow.SelectedArea.Height > 0)
                {
                    var selectedCapture = BarcodeScanner.CaptureSelectedArea(selectionWindow.SelectedArea);
                    string barcodeText = BarcodeScanner.DecodeBarcode(selectedCapture);
                    if (!string.IsNullOrEmpty(barcodeText))
                    {
                        SimulateKeyEntry(barcodeText);
                    }
                    else
                    {
                        MessageBox.Show("Aucun code-barres détecté.");
                    }
                }
            }
        }
        private void OpenManualSelection(Screen selectedScreen)
        {
            OpenSelectionForm(selectedScreen); // Appeler OpenSelectionForm avec l'écran sélectionné
        }

        private void ScanSelectedAreaButton_Click(object sender, EventArgs e)
        {
            Screen selectedScreen = GetSelectedScreen(); // Obtenir l'écran sélectionné
            if (selectedScreen != null)
            {
                OpenManualSelection(selectedScreen);  // Appeler la méthode pour ouvrir la sélection de zone
            }
            else
            {
                MessageBox.Show("Aucun écran sélectionné.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private Screen GetSelectedScreen()
        {
            // Vérifie quel radioButton est sélectionné et retourne l'écran correspondant
            foreach (Control control in this.Controls)
            {
                if (control is RadioButton radioButton && radioButton.Checked)
                {
                    int screenIndex = Convert.ToInt32(radioButton.Tag);
                    return Screen.AllScreens[screenIndex]; // Retourne l'écran sélectionné
                }
            }
            return null; // Aucun écran sélectionné
        }

        private void ShowScreenPopup(Screen selectedScreen)
        {
            Form popup = new Form
            {
                StartPosition = FormStartPosition.Manual,
                Location = selectedScreen.Bounds.Location,
                Size = new Size(300, 100),
                FormBorderStyle = FormBorderStyle.None,
                BackColor = Color.LightGray,
                Opacity = 0.8,
                ShowInTaskbar = false,
                TopMost = true
            };

            Label label = new Label
            {
                Text = $"Sélection de l'écran: {selectedScreen.DeviceName}",
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };

            popup.Controls.Add(label);
            popup.Show();

            Task.Delay(1000).ContinueWith(_ => popup.Invoke((Action)(() => popup.Close())));
        }

        // Méthode pour ouvrir la fenêtre de sélection sur l'écran spécifié
        private void OpenSelectionForm(Screen selectedScreen)
        {
            if (selectedScreen == null) return;

            // Récupérer les dimensions et la position de l'écran sélectionné
            Rectangle screenBounds = selectedScreen.Bounds;
            int screenX = screenBounds.X;
            int screenY = screenBounds.Y;
            int screenWidth = screenBounds.Width;
            int screenHeight = screenBounds.Height;

            // Création de la fenêtre de sélection sur l'écran choisi
            var selectionWindow = new SelectionForm(screenWidth, screenHeight)
            {
                StartPosition = FormStartPosition.Manual,
                Location = new Point(screenX, screenY), // Assure que la sélection s'affiche sur l'écran choisi
                TopMost = true // S'assurer qu'elle apparaît au premier plan
            };

            // Afficher la fenêtre de sélection
            if (selectionWindow.ShowDialog() == DialogResult.OK)
            {
                if (selectionWindow.SelectedArea.Width > 0 && selectionWindow.SelectedArea.Height > 0)
                {
                    var selectedCapture = BarcodeScanner.CaptureSelectedArea(selectionWindow.SelectedArea);
                    string barcodeText = BarcodeScanner.DecodeBarcode(selectedCapture);
                    if (!string.IsNullOrEmpty(barcodeText))
                    {
                        SimulateKeyEntry(barcodeText);
                    }
                    else
                    {
                        MessageBox.Show("Aucun code-barres détecté.");
                    }
                }
            }
        }




        private void SimulateKeyEntry(string text)
        {
            _inputSimulator.Keyboard.TextEntry(text);
        }

        // Méthode pour initialiser les boutons radio en fonction des écrans disponibles
        private void InitializeScreenRadios()
        {
            // Récupérer tous les écrans connectés
            var screens = Screen.AllScreens;
            int radioYPosition = 30;

            // Créer un bouton radio pour chaque écran disponible
            foreach (var screen in screens)
            {
                var screenRadio = new RadioButton
                {
                    Text = $"Scanner écran {Array.IndexOf(screens, screen) + 1}",
                    Size = new Size(200, 30),
                    Location = new Point(50, radioYPosition),
                    BackColor = Color.Transparent,
                    ForeColor = Color.Black,
                    Tag = Array.IndexOf(screens, screen)  // Enregistrer l'index de l'écran dans le Tag
                };

                screenRadio.CheckedChanged += OnScreenRadioCheckedChanged;

                // Ajouter le bouton radio au formulaire
                this.Controls.Add(screenRadio);

                // Espacer les boutons radio verticalement
                radioYPosition += 40;
            }
        }

        // Méthode appelée lorsque l'utilisateur change de sélection sur un bouton radio
        // Méthode appelée lorsque l'utilisateur change de sélection sur un bouton radio
        private void OnScreenRadioCheckedChanged(object sender, EventArgs e)
        {
            var radioButton = sender as RadioButton;
            if (radioButton?.Checked == true)
            {
                // Récupérer l'index de l'écran depuis le Tag du RadioButton
                int screenIndex = (int)radioButton.Tag;

                // Accéder à l'écran sélectionné
                Screen selectedScreen = Screen.AllScreens[screenIndex];

                // Afficher un popup non-bloquant et ouvrir la sélection de l'écran
                ShowNonBlockingPopup(selectedScreen);

                // Passer le Screen à la méthode OpenSelectionForm
                OpenSelectionForm(selectedScreen);  // Appeler OpenSelectionForm avec l'écran sélectionné
            }
        }



        private void OpenSelectionFormOnScreen(Screen selectedScreen)
        {
            SelectionForm selectionWindow = new SelectionForm(selectedScreen.Bounds.Width, selectedScreen.Bounds.Height);
            selectionWindow.StartPosition = FormStartPosition.Manual;
            selectionWindow.Location = selectedScreen.Bounds.Location;

            if (selectionWindow.ShowDialog() == DialogResult.OK)
            {
                if (selectionWindow.SelectedArea.Width > 0 && selectionWindow.SelectedArea.Height > 0)
                {
                    var selectedCapture = BarcodeScanner.CaptureSelectedArea(selectionWindow.SelectedArea);
                    string barcodeText = BarcodeScanner.DecodeBarcode(selectedCapture);
                    if (!string.IsNullOrEmpty(barcodeText))
                    {
                        SimulateKeyEntry(barcodeText);
                    }
                    else
                    {
                        MessageBox.Show("Aucun code-barres détecté.");
                    }
                }
            }
        }

        private void ShowNonBlockingPopup(Screen selectedScreen)
        {
            Form popup = new Form
            {
                FormBorderStyle = FormBorderStyle.None,  // Pas de bordures
                StartPosition = FormStartPosition.Manual,
                BackColor = Color.Black,
                ForeColor = Color.White,
                Opacity = 0.8,  // Transparence légère
                Width = 300,
                Height = 100,
                ShowInTaskbar = false,
                TopMost = true
            };

            // Positionne la popup en haut au centre de l'écran sélectionné
            popup.Location = new Point(
                selectedScreen.Bounds.X + (selectedScreen.Bounds.Width - popup.Width) / 2,
                selectedScreen.Bounds.Y + 50
            );

            // Ajouter un Label avec les infos de l'écran
            Label label = new Label
            {
                Text = $"Écran sélectionné : {selectedScreen.DeviceName}\n" +
                       $"Résolution : {selectedScreen.Bounds.Width} x {selectedScreen.Bounds.Height}",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", 10, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Black
            };
            popup.Controls.Add(label);

            // Afficher la popup et la fermer automatiquement après 2 secondes
            popup.Show();
            Task.Delay(2000).ContinueWith(t => popup.Invoke((Action)(() => popup.Close())));
        }


        // Méthode pour gérer la capture d'écran complète en fonction de l'écran sélectionné
        private void OpenScreenCaptureForm(Screen selectedScreen)
        {
            if (selectedScreen == null) return;

            // Récupérer les dimensions et la position de l'écran sélectionné
            Rectangle screenBounds = selectedScreen.Bounds;
            int screenX = screenBounds.X;
            int screenY = screenBounds.Y;
            int screenWidth = screenBounds.Width;
            int screenHeight = screenBounds.Height;

            // Création de la fenêtre de sélection sur l'écran choisi
            var selectionWindow = new SelectionForm(screenWidth, screenHeight)
            {
                StartPosition = FormStartPosition.Manual,
                Location = new Point(screenX, screenY), // Assure que la sélection s'affiche sur l'écran choisi
                TopMost = true // S'assurer qu'elle apparaît au premier plan
            };

            // Afficher la fenêtre de sélection
            if (selectionWindow.ShowDialog() == DialogResult.OK)
            {
                if (selectionWindow.SelectedArea.Width > 0 && selectionWindow.SelectedArea.Height > 0)
                {
                    var selectedCapture = BarcodeScanner.CaptureSelectedArea(selectionWindow.SelectedArea);
                    string barcodeText = BarcodeScanner.DecodeBarcode(selectedCapture);
                    if (!string.IsNullOrEmpty(barcodeText))
                    {
                        SimulateKeyEntry(barcodeText);
                    }
                    else
                    {
                        MessageBox.Show("Aucun code-barres détecté.");
                    }
                }
            }
        }

    }
}
