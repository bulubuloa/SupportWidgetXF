using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using SupportWidgetXF.Models.Widgets;
using Xamarin.Forms;

namespace DemoWidget.ViewModels
{
    public class YourClass : IAutoDropItem
    {
        public string YouDefineTitle { set; get; }
        public string YouDefineDescription { set; get; }
        public string YouDefineIcon { set; get; }

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

        public YourClass(string _title)
        {
            YouDefineTitle = _title;
        }

        public YourClass(string _title, string _des)
        {
            YouDefineTitle = _title;
            YouDefineDescription = _des;
        }

        public YourClass(string _title, string _des, string _icon)
        {
            YouDefineTitle = _title;
            YouDefineDescription = _des;
            YouDefineIcon = _icon;
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

        public ICommand AddItemToSourceCommand => new Command(OnAddItemToSourceCommand);
        private void OnAddItemToSourceCommand()
        {
            ItemDemo.Add(new YourClass("Ben Affleck", "BatMan - DC Universe", "dc"));
        }

        private CancellationTokenSource tokenSearch;
        public ICommand AutocompleteTextChanged => new Command<TextChangedEventArgs>(OnAutocompleteTextChanged);
        private void OnAutocompleteTextChanged(TextChangedEventArgs eventArgs)
        {
            if (tokenSearch != null)
                tokenSearch.Cancel();
                tokenSearch = new CancellationTokenSource();
        

            IsSearching = true;
            Task.Delay(2000).ContinueWith(delegate {
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
            },tokenSearch.Token);
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
