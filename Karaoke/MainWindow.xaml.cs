using System;
using System.Collections.Generic;
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

using Microsoft.Win32;

using NAudio.Wave;
using NAudio.Wave.SampleProviders;

using System.Windows.Threading;

namespace Karaoke {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        DispatcherTimer timer;
        // Lector de archivos
        AudioFileReader reader;
        // Comunicación con la tarjeta de audio
        // Exclusivo para salidas
        WaveOut output;

        int indexActual;
        public MainWindow() {
            InitializeComponent();

            indexActual = 0;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e) {
            var segundosActuales = reader.CurrentTime.TotalSeconds;
            var segundosTotales = reader.TotalTime.TotalSeconds;
            var porcentaje = segundosActuales / segundosTotales * 100;

            pbCancion.Value = porcentaje;

            // Dice en que segundos cambiar de texto
            var listaCambiosSegundos = new List<int>() {
                28,
                37,
                46,
                53,
                62,
                70,
                80,
                88,
                95,
                104,
                113,
                121,
                138,
                146,
                156,
                164,
                171,
                180
            };
            var listaCambiosTextos = new List<String>() {
                "We're no strangers to love\nYou know the rules\nand so do I",
                "A full commitment's what I'm\nthinking of\nYou wouldn't get this from\nany other guy",
                "I just wanna tell you\nhow I'm feeling\nGotta make you\nunderstand",
                "Never gonna give you up\nNever gonna let you down\nNever gonna run around and\ndesert you",
                "Never gonna make you cry\nNever gonna say goodbye\nNever gonna tell a lie\nand hurt you",
                "We've known eachother\nfor so long\nYour heart's been aching but\nyou're too shy to say it",
                "Inside we both know\nwhat's been going on\nWe know the game and we're\ngonna play it",
                "And if you ask me\nhow I'm feeling\nDon't tell me you're too\nblind to see",
                "Never gonna give you up\nNever gonna let you down\nNever gonna run around and\ndesert you",
                "Never gonna make you cry\nNever gonna say goodbye\nNever gonna tell a lie\nand hurt you",
                "Never gonna give you up\nNever gonna let you down\nNever gonna run around and\ndesert you",
                "Never gonna make you cry\nNever gonna say goodbye\nNever gonna tell a lie\nand hurt you",
                "Never gonna give\nNever gonna give\nNever gonna give\nNever gonna give",
                "We've known eachother\nfor so long\nYour heart's been aching but\nyou're too shy to say it",
                "Inside we both know\nwhat's been going on\nWe know the game and we're\ngonna play it",
                "I just wanna tell you\nhow I'm feeling\nGotta make you\nunderstand",
                "Never gonna give you up\nNever gonna let you down\nNever gonna run around and\ndesert you",
                "Never gonna make you cry\nNever gonna say goodbye\nNever gonna tell a lie\nand hurt you",
            };

            var segundosCambioSiguiente = listaCambiosSegundos[indexActual];

            if (segundosActuales >= segundosCambioSiguiente) {
                var textoCambioSiguiente = listaCambiosTextos[indexActual];
                txtLetra.Text = textoCambioSiguiente;

                if (listaCambiosSegundos.Count > indexActual + 1) {
                    indexActual++;
                }
            }
        }

        private void BtnReproducir_Click(object sender, RoutedEventArgs e) {
            reader = new AudioFileReader("cancion.mp3");
            output = new WaveOut();
            output.Init(reader);
            output.Play();

            timer.Start();
        }
    }
}
