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

<table>
    <tr>
        <td>Controls</td>
        <td>ScreenShots</td>
    </tr>    
</table>    

| Controls |ScreenShot  | 
|--|--|
| <b>SupportAutocomplete</b> with 4 row templates: <p>Single Title<p>Title With Description<p>Icon with Title<p>FullText with Icon<p>Autocomplete source from API | ![Alt Text](https://github.com/bulubuloa/SupportWidgetXF/blob/master/ScreenShots/demo_autocomplete.gif =250x) |
