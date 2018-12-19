
# Xamarin Forms Custom Control
I create this package to sharing all custom controls renderer i know, it help you get a better result with xamarin

### Available on NuGet: 
![Build status](https://ci.appveyor.com/api/projects/status/7g3sppml9ewumr9i/branch/master?svg=true) [![NuGet Badge](https://buildstats.info/nuget/SupportWidgetXF)](https://www.nuget.org/packages/SupportWidgetXF/)

Add assembly references

    xmlns:widgets="clr-namespace:SupportWidgetXF.Widgets;assembly=SupportWidgetXF"

Setup for iOS project (add to AppDelegate before LoadApplication)

    SupportWidgetXFSetup.Initialize(this);

Setup for Android project (add to MainActivity before LoadApplication)

    SupportWidgetXFSetup.Initialize(this, bundle);
    
## Support Widget Package

 - SupportAutoComplete **(Complete)**
 - SupportResultList **(Complete)**
 - SupportDropList **(Complete)**
 - SupportEntry **(Complete)**
 - SupportButton  **(Complete)**
 - SupportActionMenu  **(Complete)**
 - SupportBindableStackLayout  **(Complete)**
 - SupportFlowLayout  **(Complete)**
 - SupportSearchView  **(Complete)**
 - SupportShadowView  **(Complete)**
 - SupportGradientView  **(Complete)**
 - SupportMapView  **(Complete)**
 - SupportRadioButton  **(Complete)**
 - SupportCalendarView  **(Complete)**
  
<table>
	<tr>
		<td>Controls</td>
		<td>Screenshots</td>
	</tr>
	<tr>
		<td>
			<b>SupportAutocomplete</b> with 4 row templates: support binding Itemsource, etc..
			<ul>
				<li>Single Title</li>
				<li>Title With Description</li>
				<li>Icon with Title</li>
				<lip>FullText with Icon</li>
				<li>Autocomplete source from API</li>
			</ul>
		</td>
		<td><img src="https://github.com/bulubuloa/SupportWidgetXF/blob/master/ScreenShots/demo_autocomplete.gif" width="324" height="639" /></td>
	</tr>
	<tr>
		<td>
			<b>SupportDropList</b> with 4 row templates: support binding Itemsource, multi select
			<ul>
				<li>Single Title</li>
				<li>Title With Description</li>
				<li>Icon with Title</li>
				<lip>FullText with Icon</li>
				<li>Autocomplete source from API</li>
			</ul>
		</td>
		<td><img src="https://github.com/bulubuloa/SupportWidgetXF/blob/master/ScreenShots/demo_droplist.gif" width="300" height="472" /></td>
	</tr>
</table>
