using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using SupportWidgetXF.Models;
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

    public class DemoAutoCompletePageViewModel : BaseViewModel
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

        public DemoAutoCompletePageViewModel()
        {
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
