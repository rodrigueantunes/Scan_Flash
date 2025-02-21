using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ZXing;


namespace Scan_Flash
{
    public static class BarcodeScanner
    {
        public static List<string> DecodeAllBarcodes(Bitmap image)
        {
            var reader = new BarcodeReader();
            var result = reader.DecodeMultiple(image);
            return result.Select(r => r.Text).ToList();
        }

        public static string DecodeBarcode(Bitmap image)
        {
            var reader = new BarcodeReader
            {
                AutoRotate = true,  // Activer l'auto-rotation pour une meilleure détection
                
                Options = new ZXing.Common.DecodingOptions
                {
                    PossibleFormats = new List<BarcodeFormat>
                    {
                        BarcodeFormat.CODE_128,
                        BarcodeFormat.QR_CODE,
                        BarcodeFormat.DATA_MATRIX,
                        BarcodeFormat.EAN_13,
                        BarcodeFormat.EAN_8,
                        BarcodeFormat.UPC_A,
                        BarcodeFormat.UPC_E,
                        BarcodeFormat.PDF_417,
                        BarcodeFormat.ITF,
                        BarcodeFormat.UPC_A,
                        BarcodeFormat.UPC_E,
                        BarcodeFormat.EAN_13,
                        BarcodeFormat.EAN_8,
                        BarcodeFormat.CODE_39,
                        BarcodeFormat.CODE_128,
                        BarcodeFormat.ITF,
                        BarcodeFormat.CODABAR,
                        BarcodeFormat.QR_CODE,
                        BarcodeFormat.DATA_MATRIX,
                        BarcodeFormat.PDF_417,
                        BarcodeFormat.AZTEC,
                        BarcodeFormat.RSS_14,
                        BarcodeFormat.RSS_EXPANDED,
                        BarcodeFormat.CODE_93,
                        BarcodeFormat.MSI,
                        BarcodeFormat.MAXICODE

                    }
                }
            };

            var result = reader.Decode(image);
            return result?.Text;
        }

        private static Bitmap PreprocessImage(Bitmap image)
        {
            Bitmap grayImage = ConvertToGrayScale(image);
            Bitmap thresholdImage = ApplyThreshold(grayImage, 150);
            return thresholdImage;
        }

        private static Bitmap ConvertToGrayScale(Bitmap image)
        {
            Bitmap grayImage = new Bitmap(image.Width, image.Height);
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    int grayValue = (int)(pixelColor.R * 0.3 + pixelColor.G * 0.59 + pixelColor.B * 0.11);
                    grayImage.SetPixel(x, y, Color.FromArgb(grayValue, grayValue, grayValue));
                }
            }
            return grayImage;
        }

        private static Bitmap ApplyThreshold(Bitmap image, int threshold)
        {
            Bitmap thresholdImage = new Bitmap(image.Width, image.Height);
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    int pixelBrightness = pixelColor.R;
                    Color newColor = pixelBrightness >= threshold ? Color.White : Color.Black;
                    thresholdImage.SetPixel(x, y, newColor);
                }
            }
            return thresholdImage;
        }

        public static Bitmap CaptureScreen()
        {
            try
            {
                // Capture du premier écran ou de tous les écrans
                var screenBounds = Screen.PrimaryScreen.Bounds;

                // Si vous souhaitez capturer un écran spécifique parmi plusieurs
                // var allScreens = Screen.AllScreens;
                // screenBounds = allScreens[1].Bounds; // Exemple pour capturer le deuxième écran

                if (screenBounds.Width <= 0 || screenBounds.Height <= 0)
                {
                    throw new ArgumentException("Les dimensions de l'écran sont invalides.");
                }

                Bitmap bitmap = new Bitmap(screenBounds.Width, screenBounds.Height);

                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    if (g == null)
                    {
                        throw new InvalidOperationException("L'objet Graphics n'a pas pu être créé.");
                    }

                    g.CopyFromScreen(screenBounds.Location, Point.Empty, screenBounds.Size);
                }

                return bitmap;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erreur lors de la capture d'écran : " + ex.Message, ex);
            }
        }

        public static Bitmap CaptureSelectedArea(Rectangle selectedArea)
        {
            try
            {
                if (selectedArea.Width <= 0 || selectedArea.Height <= 0)
                {
                    throw new ArgumentException("La zone sélectionnée n'est pas valide.");
                }

                Bitmap bitmap = new Bitmap(selectedArea.Width, selectedArea.Height);
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    if (g == null)
                    {
                        throw new InvalidOperationException("L'objet Graphics n'a pas pu être créé.");
                    }

                    g.CopyFromScreen(selectedArea.Location, Point.Empty, selectedArea.Size);
                }

                return bitmap;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erreur lors de la capture de la zone sélectionnée : " + ex.Message, ex);
            }
        }
    }
}
