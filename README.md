# Xamarin Forms Custom Control
I create this package to sharing all custom controls renderer i know, it help you get a better result with xamarin

Add assembly references

    xmlns:ultimateChart="clr-namespace:SupportWidgetXF.Widgets;assembly=SupportWidgetXF"

Setup for iOS project (add to AppDelegate before LoadApplication)

    SupportWidgetXFSetup.Initialize();

Setup for Android project (add to MainActivity before LoadApplication)

    SupportWidgetXFSetup.Initialize(this);
## Support Widget Package

 - SupportAutoComplete (Complete)
 - SupportEntry
 - SupportButton
 - SupportActionMenu
 - SupportBindableStackLayout
 - SupportSearchView

| Controls |ScreenShot  | 
|--|--|
| <b>SupportAutocomplete</b> with 4 row templates:- Single Title - Title With Description - Icon with Title - FullText with Icon - Autocomplete source from API | ![Alt Text](https://github.com/bulubuloa/SupportWidgetXF/blob/master/ScreenShots/demo_autocomplete.gif) |
