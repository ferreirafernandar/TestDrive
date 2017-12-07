using SQLite;
using System.IO;
using TestDrive.Data;
using TestDrive.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(SqLiteAndroid))]
namespace TestDrive.Droid
{
    public class SqLiteAndroid : ISQLite
    {
        private const string NomeArquivoDb = "Agendamento.db3";

        public SQLiteConnection PegarConexao()
        {
            var caminhoDb = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.Path,
                NomeArquivoDb);

            return new SQLiteConnection(caminhoDb);
        }
    }
}