using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Euro2020App.Interfaces;
using System.IO;
using Xamarin.Essentials;
using Android.Provider;
using System.Threading.Tasks;
using Xamarin.Forms;
using Euro2020App.Droid;

[assembly: Dependency(typeof(AndroidLocalPath))]
namespace Euro2020App.Droid
{
    public class AndroidLocalPath : ILocalPath
    {
        public async Task<string> GetPath()
        {
            var folder = System.IO.Path.Combine(
                Android.App.Application.Context.GetExternalFilesDir(
                    Android.OS.Environment.DirectoryDocuments).AbsolutePath, "pdfs");

            Directory.CreateDirectory(folder);

            var name = $"{System.Guid.NewGuid()}.pdf";
            var filename = System.IO.Path.Combine(folder, name);

            var status = await Permissions.RequestAsync<Permissions.StorageWrite>();

            if (status == PermissionStatus.Granted)
                return filename;

            return "";
        }

        public async Task<string> SaveImage()
        {
            var folder = System.IO.Path.Combine(
                Android.App.Application.Context.GetExternalFilesDir(
                    Android.OS.Environment.DirectoryDocuments).AbsolutePath, "flags");

            Directory.CreateDirectory(folder);

            var name = $"{System.Guid.NewGuid()}.png";
            var filename = System.IO.Path.Combine(folder, name);

            var status = await Permissions.RequestAsync<Permissions.StorageWrite>();

            if (status == PermissionStatus.Granted)
            {
                try
                {
                    var values = new ContentValues();
                    values.Put(MediaStore.Audio.Media.InterfaceConsts.DisplayName, System.IO.Path.GetFileNameWithoutExtension(filename));
                    values.Put(MediaStore.Audio.Media.InterfaceConsts.MimeType, "image/png");
                    values.Put(MediaStore.Images.Media.InterfaceConsts.Description, string.Empty);
                    values.Put(MediaStore.Images.Media.InterfaceConsts.DateTaken, Java.Lang.JavaSystem.CurrentTimeMillis());
                    values.Put(MediaStore.Images.ImageColumns.BucketId, filename.ToLowerInvariant().GetHashCode());
                    values.Put(MediaStore.Images.ImageColumns.BucketDisplayName, name.ToLowerInvariant());
                    var cr = Android.App.Application.Context.ContentResolver;
                    var albumUri = cr.Insert(MediaStore.Images.Media.ExternalContentUri, values);
                }
                catch (System.Exception ex)
                {

                }

                return filename;
            }
            /*
            var uri = Uri.FromFile(new Java.IO.File(filename));
            uri = MediaStore.Images.Media.ExternalContentUri;
            uri = Uri.WithAppendedPath(uri, name);*/

            return "";
        }
    }
}