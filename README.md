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
 - SupportResultList (Complete)
 - SupportDropList (Complete)
 - SupportEntry (Complete)
 - SupportButton
 - SupportActionMenu
 - SupportBindableStackLayout
 - SupportSearchView
  
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
</table>
