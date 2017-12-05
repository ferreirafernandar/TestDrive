using System;
using System.Reflection;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Provider;
using Java.IO;
using TestDrive.Droid;
using TestDrive.Media;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(MainActivity))]
namespace TestDrive.Droid
{
    [Activity(Label = "TestDrive", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, ICamera
    {
        static File arquivoImagem;
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode != Result.Ok) return;
            byte[] bytes;

            using (var stream = new FileInputStream(arquivoImagem))
            {
                bytes = new byte[arquivoImagem.Length()];
                stream.Read(bytes);
            }

            MessagingCenter.Send(bytes, "FotoTirada");
        }

        public void TirarFoto()
        {
            var intent = new Intent(MediaStore.ActionImageCapture);


            arquivoImagem = PegarArquivoImagem();
            intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(arquivoImagem));
            var activity = Forms.Context as Activity;

            activity?.StartActivityForResult(intent, 0);
        }

        private static File PegarArquivoImagem()
        {
            var diretorio = new File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), "Imagens");

            if (!diretorio.Exists())
                diretorio.Mkdirs();

            arquivoImagem = new File(diretorio, "MinhaFoto.jpg");
            return arquivoImagem;
        }
    }
}

