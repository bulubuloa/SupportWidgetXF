using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using SupportWidgetXF.Controllers.API;
using SupportWidgetXF.DependencyService;
using SupportWidgetXF.Models;
using SupportWidgetXF.Models.API.Request;
using SupportWidgetXF.Models.API.Response;
using SupportWidgetXF.Models.Widgets;
using Xamarin.Forms;

namespace DemoWidget.ViewModels
{
    public class YourClass : IAutoDropItem
    {
        public string YouDefineTitle { set; get; }
        public string YouDefineDescription { set; get; }
        public string YouDefineIcon { set; get; }
        public bool YouChecked { set; get; }

        public string IF_GetDescription()
        {
            return YouDefineDescription;
        }

        public string IF_GetIcon()
        {
            return YouDefineIcon;
        }

        public string IF_GetTitle()
        {
            return YouDefineTitle;
        }

        public Action IF_GetAction()
        {
            return null;
        }

        public bool IF_GetChecked()
        {
            return YouChecked;
        }

        public void IF_SetChecked(bool _Checked)
        {
            YouChecked = _Checked;
        }

        public YourClass(string _title)
        {
            YouDefineTitle = _title;
            YouChecked = false;
        }

        public YourClass(string _title, string _des)
        {
            YouDefineTitle = _title;
            YouDefineDescription = _des;
            YouChecked = false;
        }

        public YourClass(string _title, string _des, string _icon)
        {
            YouDefineTitle = _title;
            YouDefineDescription = _des;
            YouDefineIcon = _icon;
            YouChecked = false;
        }
    }

    public class DemoAutoCompletePageViewModel : BaseViewModel, IGalleryPickerResultListener
    {
        private List<IAutoDropItem> _ItemDemo;
        public List<IAutoDropItem> ItemDemo
        {
            get => _ItemDemo;
            set
            {
                _ItemDemo = value;
                OnPropertyChanged();
            }
        }

        private List<IAutoDropItem> _ItemDemoAsync;
        public List<IAutoDropItem> ItemDemoAsync
        {
            get => _ItemDemoAsync;
            set
            {
                _ItemDemoAsync = value;
                OnPropertyChanged();
            }
        }

        private bool _IsSearching;
        public bool IsSearching
        {
            get => _IsSearching;
            set
            {
                _IsSearching = value;
                OnPropertyChanged();
            }
        }

        private bool _IsValidSingle;
        public bool IsValidSingle
        {
            get => _IsValidSingle;
            set
            {
                _IsValidSingle = value;
                OnPropertyChanged();
            }
        }

       
        private string _TextIt;
        public string TextIt
        {
            get => _TextIt;
            set
            {
                _TextIt = value;
                OnPropertyChanged();
            }
        }

        public ICommand PickerCommand => new Command(OnPickerCommand);
        private void OnPickerCommand()
        {
            DependencyService.Get<IGalleryPicker>().IF_OpenGallery(this);
        }

        public ICommand CameraCommand => new Command(OnCameraCommand);
        private void OnCameraCommand()
        {
            DependencyService.Get<IGalleryPicker>().IF_OpenCamera(this);
        }


        public ICommand AddItemToSourceCommand => new Command(OnAddItemToSourceCommand);
        private void OnAddItemToSourceCommand()
        {
            //ItemDemo.Add(new YourClass("Ben Affleck", "BatMan - DC Universe", "dc"));
            //IsValidSingle = false;
            TextIt = "man";
        }

        private CancellationTokenSource tokenSearch;
        public ICommand AutocompleteTextChanged => new Command<TextChangedEventArgs>(OnAutocompleteTextChanged);
        private void OnAutocompleteTextChanged(TextChangedEventArgs eventArgs)
        {
            if (tokenSearch != null)
                tokenSearch.Cancel();
            tokenSearch = new CancellationTokenSource();


            IsSearching = true;
            Task.Delay(2000).ContinueWith(delegate
            {
                //get something from API

                var newResult = new List<IAutoDropItem>();
                newResult.Add(new YourClass("New Item 1", "New Item - DC Universe", "dc"));
                newResult.Add(new YourClass("New Item 2", "New Item - DC Universe", "dc"));
                newResult.Add(new YourClass("New Item 3", "New Item - DC Universe", "dc"));
                newResult.Add(new YourClass("New Item 4", "New Item - DC Universe", "dc"));
                newResult.Add(new YourClass("New Item 5", "New Item - DC Universe", "dc"));

                //plz set result by new source to refresh autocomplete
                ItemDemoAsync = newResult;
                IsSearching = false;
            }, tokenSearch.Token);
        }


        public ICommand TestOnItemSelected => new Command<IntegerEventArgs>(OnTestOnItemSelected);
        private void OnTestOnItemSelected(IntegerEventArgs eventArgs)
        {
            
        }

        private int _ItemSelectedPosition;
        public int ItemSelectedPosition
        {
            get => _ItemSelectedPosition;
            set
            {
                _ItemSelectedPosition = value;
                OnPropertyChanged();
            }
        }

        public ICommand SetPositionCommand => new Command(OnSetPositionCommand);
        private void OnSetPositionCommand()
        {
            ItemSelectedPosition = 1;
        }

        private ObservableCollection<string> _ImageItemsSet;
        public ObservableCollection<string> ImageItemsSet
        {
            get => _ImageItemsSet;
            set
            {
                _ImageItemsSet = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ImageSource> _ImageItems;
        public ObservableCollection<ImageSource> ImageItems
        {
            get => _ImageItems;
            set
            {
                _ImageItems = value;
                OnPropertyChanged();
            }
        }

        private bool IsPath = false;

        public void IF_PickedResult(List<ImageSet> result)
        {

            if(IsPath)
            {
                foreach (var item in result)
                {
                    ImageItemsSet.Add(item.Path);
                }
            }
            else
            {
                foreach (var item in result)
                {
                    ImageItems.Add(item.SourceXF);
                    Task.Delay(1000).ContinueWith(delegate
                    {
                        DependencyService.Get<IGalleryPicker>().IF_SyncPhotoFromCloud(this,item);
                        //UploadPhotoDemo(item);
                    });
                }
            }
        }

        public class Req_UpImage : AESRequestBaseModel
        {
            public string Type { set; get; }
            public string Name { set; get; }
            public int InstructionId { set; get; }
            public string Data { set; get; }
        }

        private async void UploadPhotoDemo(ImageSet imageSource)
        {
            var para = new Req_UpImage();
            para.Type = "Floor";
            para.Name = "huhu";
            para.InstructionId = 111;
            para.Data = Convert.ToBase64String(imageSource.Stream);
            string token = "52DGB6821oG0WE7wfufmGBGkNaK5YYu2PQXxKBl7k6+mfVriFYOp11FO1tHVZPfQytn/QVetQOx6LYIXlIGgxUJOcFOhq7EpobLNmRIg39Un2i1aSAZ4RiPe2tg3Di789RpVxwRdTE79pLwy+zl/eeMhaheXQxWz/XrVbZWtoK+ANVowmsFGTyR0ZHaF76x37CEJtxjQIvLozkVmLEZE7vO74v5hB5SHjwFKZnQfOI";
            string url = "https://172.16.10.138/jll/api/v1/PostInspectionImage?accessToken="+token;
            APIServiceAES aPIService = new APIServiceAES();
            var response = await aPIService.RequestPostAsync<AESBlankResponse>(url, para, token);
        }

        public DemoAutoCompletePageViewModel()
        {
            ImageItems = new ObservableCollection<ImageSource>();
            ImageItemsSet = new ObservableCollection<string>();

            ItemDemo = new List<IAutoDropItem>();
            ItemDemo.Add(new YourClass("Robert Downey Jr.","Iron Man - Marvel Universe","marvel"));
            ItemDemo.Add(new YourClass("Chris Evans", "Captain America - Marvel Universe", "marvel"));
            ItemDemo.Add(new YourClass("Scarlett Johansson", "Black Widow - Marvel Universe", "marvel"));
            ItemDemo.Add(new YourClass("Tom Hiddleston", "Loki - Marvel Universe", "marvel"));
            ItemDemo.Add(new YourClass("Mark Ruffalo", "The Hulk - Marvel Universe", "marvel"));
            ItemDemo.Add(new YourClass("Ben Affleck", "BatMan - DC Universe", "dc"));
            ItemDemo.Add(new YourClass("Henry Cavill", "Superman - DC Universe", "dc"));
            ItemDemo.Add(new YourClass("Gal Gadot", "Wonder Woman - DC Universe", "dc"));
            ItemDemo.Add(new YourClass("Ezra Miller", "The Flash - DC Universe", "dc"));
            ItemDemo.Add(new YourClass("Jason Momoa", "Aquaman - DC Universe", "dc"));
        }
    }
}
