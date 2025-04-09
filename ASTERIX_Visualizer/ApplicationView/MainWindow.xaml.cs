using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using DataManagementLibrary;
using GMap.NET.MapProviders;
using GMap.NET;
using GMap.NET.WindowsPresentation;
using static GMap.NET.Entity.OpenStreetMapRouteEntity;

using DataModel;
using MapManagementLibrary;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace ApplicationView;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        CustomMapView customMap = new CustomMapView();

        // Wrap CustomMapView in a WindowsFormsHost to make it compatible with UIElement
        formsHost = new WindowsFormsHost();
        formsHost.Child = customMap;

        // Set the WindowsFormsHost as the content of the Grid
        this.gridHost.Children.Add(formsHost);


    }
}
