
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Database;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using SupportWidgetXF.Models;

namespace SupportWidgetXF.Droid.Renderers.GalleryPicker
{
    [Activity(Label = "GalleryPickerActivity")]
    public class GalleryPickerActivity : Activity
    {
        public static List<GalleryDirectory> al_images = new List<GalleryDirectory>();
        bool boolean_folder;
        GalleryDirectoryAdapter obj_adapter;
        GridView gv_folder;

        const int REQUEST_PERMISSIONS = 100;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_gallery_picker);
            // Create your application here
            gv_folder = (GridView)FindViewById(Resource.Id.gv_folder);
            gv_folder.ItemClick += (sender, e) => {
               // StartActivity(this,typeof())
            };
            if ((ContextCompat.CheckSelfPermission(ApplicationContext, Manifest.Permission.WriteExternalStorage) != Permission.Granted)
                && (ContextCompat.CheckSelfPermission(ApplicationContext,Manifest.Permission.ReadExternalStorage) != Permission.Granted))
            {
                if ((ActivityCompat.ShouldShowRequestPermissionRationale(this, Manifest.Permission.WriteExternalStorage)) 
                    && (ActivityCompat.ShouldShowRequestPermissionRationale(this,Manifest.Permission.ReadExternalStorage)))
                {

                }
                else
                {
                    ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.WriteExternalStorage, Manifest.Permission.ReadExternalStorage }, REQUEST_PERMISSIONS);
                }
            }
            else
            {
                fn_imagespath();
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            switch (requestCode)
            {
                case REQUEST_PERMISSIONS:
                    {
                        for (int i = 0; i < grantResults.Length; i++)
                        {
                            if (grantResults.Length > 0 && grantResults[i] == Permission.Granted)
                            {
                                fn_imagespath();
                            }
                            else
                            {
                                Toast.MakeText(this, "The app was not allowed to read or write to your storage. Hence, it cannot function properly. Please consider granting it this permission", ToastLength.Long).S();
                            }
                        }
                    }
                    break;
            }
        }

        public List<GalleryDirectory> fn_imagespath()
        {
            al_images.Clear();

            int int_position = 0;
            Android.Net.Uri uri;
            ICursor cursor;
            int column_index_data, column_index_folder_name;

            string absolutePathOfImage = null;
            uri = MediaStore.Images.Media.ExternalContentUri;

            string[] projection = { MediaStore.MediaColumns.Data, MediaStore.Images.ImageColumns.BucketDisplayName };

            string orderBy = MediaStore.Images.ImageColumns.DateTaken;
            cursor = ApplicationContext.ContentResolver.Query(uri, projection, null, null, orderBy + " DESC");

            column_index_data = cursor.GetColumnIndexOrThrow(MediaStore.MediaColumns.Data);
            column_index_folder_name = cursor.GetColumnIndexOrThrow(MediaStore.Images.ImageColumns.BucketDisplayName);
            while (cursor.MoveToNext())
            {
                absolutePathOfImage = cursor.GetString(column_index_data);
                //Log.e("Column", absolutePathOfImage);
                //Log.e("Folder", cursor.getString(column_index_folder_name));

                for (int i = 0; i < al_images.Count; i++)
                {
                    if (al_images[i].Name.Equals(cursor.GetString(column_index_folder_name)))
                    {
                        boolean_folder = true;
                        int_position = i;
                        break;
                    }
                    else
                    {
                        boolean_folder = false;
                    }
                }


                if (boolean_folder)
                {

                    List<string> al_path = new List<string>;
                    al_path.AddRange(al_images[int_position].Images);
                    al_path.Add(absolutePathOfImage);
                    al_images[int_position].Images = (al_path);

                }
                else
                {
                    List<string> al_path = new List<string>();
                    al_path.Add(absolutePathOfImage);

                    GalleryDirectory obj_model = new GalleryDirectory();
                    obj_model.Name = (cursor.GetString(column_index_folder_name));
                    obj_model.Images = (al_path);

                    al_images.Add(obj_model);
                }
            }

            obj_adapter = new GalleryDirectoryAdapter(ApplicationContext, al_images);
            gv_folder.Adapter = (obj_adapter);
            return al_images;
        }
    }
}