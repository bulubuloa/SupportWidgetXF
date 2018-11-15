using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SupportWidgetXF.Models.Widgets;
using Xamarin.Forms;

namespace SupportWidgetXF.Models
{
    public enum ImageAsyncStatus
    {
        InCloud, InLocal, SyncFromCloud, Uploading, Uploaded, SyncCloudError, UploadError, Dowloading
    }

    public class GalleryImageXF : BindableObject
    {
        [JsonIgnore]
        private ImageSource _ImageSourceXF;
        [JsonIgnore]
        public ImageSource ImageSourceXF 
        { 
            set 
            {
                _ImageSourceXF = value;
                OnPropertyChanged();
            }
            get => _ImageSourceXF;
        }

        private decimal? _InspectionImageId;
        public decimal? InspectionImageId
        {
            set
            {
                _InspectionImageId = value;
                OnPropertyChanged();
            }
            get => _InspectionImageId;
        }

        private bool _Checked;
        public bool Checked
        {
            set
            {
                _Checked = value;
                OnPropertyChanged();
            }
            get => _Checked;
        }

        [JsonIgnore]
        private byte[] _ImageRawData;
        [JsonIgnore]
        public byte[] ImageRawData
        { 
            set
            {
                _ImageRawData = value;
                OnPropertyChanged();
            }
            get => _ImageRawData;
        }

        private bool _CloudStorage;
        public bool CloudStorage
        {
            set
            {
                _CloudStorage = value;
                OnPropertyChanged();
                if (_CloudStorage)
                    AsyncStatus = ImageAsyncStatus.InCloud;
                else
                    AsyncStatus = ImageAsyncStatus.InLocal;
            }
            get => _CloudStorage;
        }

        private string _OriginalPath;
        public string OriginalPath
        {
            set
            {
                _OriginalPath = value;
                OnPropertyChanged();
            }
            get => _OriginalPath;
        }

        private string _UrlUploaded;
        public string UrlUploaded
        {
            set
            {
                _UrlUploaded = value;
                OnPropertyChanged();
            }
            get => _UrlUploaded;
        }

        private string _Name;
        public string Name
        {
            set
            {
                _Name = value;
                OnPropertyChanged();
            }
            get => _Name;
        }

        [JsonIgnore]
        private ImageAsyncStatus _AsyncStatus;
        [JsonIgnore]
        public ImageAsyncStatus AsyncStatus
        {
            set
            {
                _AsyncStatus = value;
                OnPropertyChanged();
            }
            get => _AsyncStatus;
        }

        public GalleryImageXF()
        {
            Checked = false;
            AsyncStatus = ImageAsyncStatus.InLocal;
        }
    }

    public class GalleryDirectory : IAutoDropItem
    {
        public string Name { set; get; }

        public List<GalleryImageXF> Images { set; get; }

        public GalleryDirectory()
        {
            Images = new List<GalleryImageXF>();
        }

        public string IF_GetTitle()
        {
            return Name;
        }

        public string IF_GetDescription()
        {
            return Name;
        }

        public string IF_GetIcon()
        {
            return Name;
        }

        public Action IF_GetAction()
        {
            return null;
        }

        public bool IF_GetChecked()
        {
            return false;
        }

        public void IF_SetChecked(bool _Checked)
        {

        }
    }
}