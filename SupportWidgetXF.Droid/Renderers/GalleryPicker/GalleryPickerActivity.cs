using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using Android.Widget;
using Java.Text;
using SupportWidgetXF.Models;
using SupportWidgetXF.Widgets.Interface;
using Xamarin.Forms;

namespace SupportWidgetXF.Droid.Renderers.GalleryPicker
{
    [Activity(Label = "GalleryPickerActivity")]
    public class GalleryPickerActivity : Activity, IGalleryPickerSelected
    {
        private List<GalleryDirectory> galleryDirectories;
        private bool FlagDirectory;

        private GridView gridView;
        private GalleryImageAdapter galleryImageAdapter;

        private Spinner spinner;
        private GalleryDirectoryAdapter galleryDirectoryAdapter;

        private Android.Widget.Button bttDone;
        private ImageButton bttBack;

        const int REQUEST_PERMISSIONS_LIBRARY = 100;
        const int REQUEST_CAMERA_CAPTURE = 102;

        private List<ImageSet> GetImageSetSelected()
        {
            return galleryDirectories.SelectMany(directory => directory.Images).Where(Image => Image.Checked).ToList();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_gallery_picker);

            if (ActionBar != null)
                ActionBar.Hide();

            galleryDirectories = new List<GalleryDirectory>();

            gridView = (GridView)FindViewById(Resource.Id.gridView);
            spinner = (Spinner)FindViewById(Resource.Id.spinnerGallery);
            bttBack = (Android.Widget.ImageButton)FindViewById(Resource.Id.bttBack);
            bttDone = (Android.Widget.Button)FindViewById(Resource.Id.bttDone);

            galleryDirectoryAdapter = new GalleryDirectoryAdapter(this, galleryDirectories);
            spinner.Adapter = galleryDirectoryAdapter;
            spinner.ItemSelected += (sender, e) => {
                galleryImageAdapter = new GalleryImageAdapter(this, galleryDirectories, spinner.SelectedItemPosition,this);
                gridView.Adapter = galleryImageAdapter;
            };

            bttBack.Click += (object sender, System.EventArgs e) => {
                Finish();
            };


            bttDone.Click += (object sender, System.EventArgs e) => {
                MessagingCenter.Send<GalleryPickerActivity,List<ImageSet>>(this, "ReturnImage", GetImageSetSelected());
                Finish();
            };

            if(CheckPermissionLibrary())
            {
                FillAllPhotosFromGallery();
            }
            else
            {
                RequestPermissionLibrary();
            }
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            switch (requestCode)
            {
                case REQUEST_CAMERA_CAPTURE:
                    if(resultCode == Result.Ok)
                    {
                        Console.WriteLine(mCurrentPhotoPath);
                        var result = new List<ImageSet>()
                        {
                            new ImageSet(){
                                Path = mCurrentPhotoPath
                            }
                        };
                        MessagingCenter.Send<GalleryPickerActivity, List<ImageSet>>(this, "ReturnImage", result);
                        Finish();
                    }
                    break;
                default:
                    break;
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            switch (requestCode)
            {
                case REQUEST_PERMISSIONS_LIBRARY:
                    for (int i = 0; i < grantResults.Length; i++)
                    {
                        if (grantResults.Length > 0 && grantResults[i] == Permission.Granted)
                        {
                            FillAllPhotosFromGallery();
                        }
                        else
                        {
                            Toast.MakeText(this, "The app was not allowed to read or write to your storage. Hence, it cannot function properly. Please consider granting it this permission", ToastLength.Long).Show();
                        }
                    }
                    break;
            }
        }

        private bool CheckPermissionLibrary()
        {
            if((ContextCompat.CheckSelfPermission(ApplicationContext, Manifest.Permission.WriteExternalStorage) != Permission.Granted) && (ContextCompat.CheckSelfPermission(ApplicationContext, Manifest.Permission.ReadExternalStorage) != Permission.Granted))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void RequestPermissionLibrary()
        {
            if ((ActivityCompat.ShouldShowRequestPermissionRationale(this, Manifest.Permission.WriteExternalStorage)) 
                && (ActivityCompat.ShouldShowRequestPermissionRationale(this, Manifest.Permission.ReadExternalStorage))
                    && (ActivityCompat.ShouldShowRequestPermissionRationale(this, Manifest.Permission.Camera)))
            {
                //show explain
            }
            else
            {
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.WriteExternalStorage, Manifest.Permission.ReadExternalStorage, Manifest.Permission.Camera }, REQUEST_PERMISSIONS_LIBRARY);
            }
        }

        private void OpenCameraCapture()
        {
            Intent takePictureIntent = new Intent(MediaStore.ActionImageCapture);
            if (takePictureIntent.ResolveActivity(PackageManager) != null)
            {
                Java.IO.File photoFile = null;
                try
                {
                    photoFile = createImageFile();
                }
                catch (IOException ex)
                {
                }
                if (photoFile != null)
                {
                    Android.Net.Uri photoURI = FileProvider.GetUriForFile(this,PackageName,photoFile);
                    takePictureIntent.PutExtra(MediaStore.ExtraOutput, photoURI);
                    StartActivityForResult(takePictureIntent, REQUEST_CAMERA_CAPTURE);
                }
            }
        }

        string mCurrentPhotoPath;
        private Java.IO.File createImageFile()
        {
            // Create an image file name
            string timeStamp = new SimpleDateFormat("yyyyMMdd_HHmmss").Format(new Java.Util.Date());
            string imageFileName = "JPEG_" + timeStamp + "_";
            Java.IO.File storageDir = GetExternalFilesDir(Android.OS.Environment.DirectoryPictures);
            Java.IO.File image = Java.IO.File.CreateTempFile(
                imageFileName,  /* prefix */
                ".jpg",         /* suffix */
                storageDir      /* directory */
            );

            // Save a file: path for use with ACTION_VIEW intents
            mCurrentPhotoPath = image.AbsolutePath;
            return image;
        }

        private List<GalleryDirectory> FillAllPhotosFromGallery()
        {
            galleryDirectories.Clear();

            int PositionDirectory = 0;
            Android.Net.Uri uri;
            ICursor cursor;
            int columnPhotoIndex, columnDirectoryIndex;
            string absolutePathOfImage = null;

            //Query photos from gallery
            uri = MediaStore.Images.Media.ExternalContentUri;
            string[] projection = { MediaStore.Images.Thumbnails.Data, MediaStore.Images.ImageColumns.BucketDisplayName };
            string orderBy = MediaStore.Images.ImageColumns.DateTaken;
            cursor = ApplicationContext.ContentResolver.Query(uri, projection, null, null, orderBy + " DESC");
            columnPhotoIndex = cursor.GetColumnIndexOrThrow(MediaStore.Images.Thumbnails.Data);
            columnDirectoryIndex = cursor.GetColumnIndexOrThrow(MediaStore.Images.ImageColumns.BucketDisplayName);

            List<GalleryDirectory> galleriesRaw = new List<GalleryDirectory>();

            //Loop to add data to collection
            while (cursor.MoveToNext())
            {
                absolutePathOfImage = cursor.GetString(columnPhotoIndex);

                for (int i = 0; i < galleriesRaw.Count; i++)
                {
                    if (galleriesRaw[i].Name.Equals(cursor.GetString(columnDirectoryIndex)))
                    {
                        FlagDirectory = true;
                        PositionDirectory = i;
                        break;
                    }
                    else
                    {
                        FlagDirectory = false;
                    }
                }

                if (FlagDirectory)
                {
                    var imageSets = new List<ImageSet>();
                    imageSets.AddRange(galleriesRaw[PositionDirectory].Images);
                    imageSets.Add(new ImageSet(){Path = absolutePathOfImage });

                    galleriesRaw[PositionDirectory].Images = (imageSets);
                }
                else
                {
                    var imageSets = new List<ImageSet>();
                    imageSets.Add(new ImageSet() { Path = absolutePathOfImage });

                    var galleryDirectory = new GalleryDirectory();
                    galleryDirectory.Name = (cursor.GetString(columnDirectoryIndex));
                    galleryDirectory.Images = (imageSets);

                    galleriesRaw.Add(galleryDirectory);
                }
            }

            galleryDirectories.AddRange(galleriesRaw.Where(obj => obj.Images.Count > 0).OrderBy(obj=>obj.Name));
            galleryDirectories.ForEach(obj => obj.Images.Insert(0, new ImageSet()));
            galleryDirectoryAdapter.NotifyDataSetChanged();
            return galleryDirectories;
        }

        public void IF_ImageSelected(int positionDirectory, int positionImage)
        {
            try
            {
                var item = galleryDirectories[positionDirectory].Images[positionImage];
                item.Checked = !item.Checked;

                if (galleryImageAdapter != null)
                    galleryImageAdapter.NotifyDataSetChanged();

                var count = GetImageSetSelected().Count;
                if(count>0)
                {
                    bttDone.Text = "Done (" + count + ")";
                }
                else
                {
                    bttDone.Text = "Done";
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        public void IF_CameraSelected(int pos)
        {
            if(CheckPermissionLibrary())
            {
                OpenCameraCapture();
            }
            else
            {
                RequestPermissionLibrary();
            }
        }

        public void IF_ImageSelected(int positionDirectory, int positionImage, ImageSource imageSource)
        {
        }
    }
}