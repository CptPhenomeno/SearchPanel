# SearchPanel
A simple search bar for Xamarin Forms that appears when tapped
# Usage
Add the following namespace to your xaml file
    
    xmlns:search="clr-namespace:SearchPanel;assembly=SearchPanel"
    
then you can add the SearchBar component to your view

    <search:SearchBar
            BackgroundColor="#10FFFFFF"
            ClosePanelIconColor="#8F8F8F"
            SearchCommand="{Binding YourSearchCommand}"
            SearchIconBackgroundColor="#22223D"
            SearchIconColor="#FFFFFF"
            Text="{Binding YourTextProperty}"
            TextColor="#8F8F8F" />
