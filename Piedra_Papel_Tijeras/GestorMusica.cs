using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WMPLib;

namespace Piedra_Papel_Tijeras
{
    public static class GestorMusica
    {
        [DllImport("winmm.dll")]
        private static extern long mciSendString(string command, string buffer, int bufferSize, IntPtr hwndCallback);

        private static string cancionActual = "";

        public static void ReproducirEnBucle(UnmanagedMemoryStream recursoAudio, string idCancion)
        {
            if (cancionActual == idCancion || recursoAudio == null) return;

            try
            {
                Detener();

                // Creamos un archivo temporal con el audio extraído de Resources
                string rutaTemporal = Path.Combine(Path.GetTempPath(), $"{idCancion}.wav");

                using (FileStream fs = new FileStream(rutaTemporal, FileMode.Create, FileAccess.Write))
                {
                    recursoAudio.CopyTo(fs);
                }

                // Reproducimos el archivo temporal
                mciSendString($"open \"{rutaTemporal}\" type mpegvideo alias bgm", null, 0, IntPtr.Zero);
                mciSendString("play bgm repeat", null, 0, IntPtr.Zero);

                cancionActual = idCancion;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al reproducir audio: {ex.Message}");
            }
        }

        public static void Detener()
        {
            mciSendString("close bgm", null, 0, IntPtr.Zero);
            cancionActual = "";
        }
    }
}
