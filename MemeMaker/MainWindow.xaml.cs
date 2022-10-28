using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MemeMaker
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DescargarClick(object sender, RoutedEventArgs e)
        {
            RenderTargetBitmap bitmap = new RenderTargetBitmap((int)this.meme.ActualWidth, (int)this.meme.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(this.meme);

            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.Title = "Guardar como..."; 
            dialog.FileName = "Meme"; 
            dialog.Filter = "Imagenes | *.jpeg";
            dialog.DefaultExt = "jpeg";

            if (dialog.ShowDialog() == true)
            {
                string path = dialog.FileName;
                // If user has changed the filename, create the new directory
                using (FileStream stream = File.Create($"{path}"))
                {
                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                    encoder.QualityLevel = 100;
                    encoder.Frames.Add(BitmapFrame.Create(bitmap));
                    encoder.Save(stream);
                }
            }

            
        }
    }
}
