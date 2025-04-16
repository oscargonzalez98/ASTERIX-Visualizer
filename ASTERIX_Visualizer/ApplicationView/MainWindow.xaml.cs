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
using Microsoft.VisualBasic.ApplicationServices;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using DataModelLibrary;
using System.Diagnostics.Metrics;
using System.Collections.Concurrent;
using System.Drawing;

namespace ApplicationView;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    ProcessedAsterixData asterixData;
    CustomMapView customMap;

    public MainWindow()
    {
        InitializeComponent();

        // Initialize the map control
        customMap = new CustomMapView();
        // Wrap CustomMapView in a WindowsFormsHost to make it compatible with UIElement
        formsHost = new WindowsFormsHost();
        formsHost.Child = customMap;
        // Set the WindowsFormsHost as the content of the Grid
        this.mapGridHost.Children.Add(formsHost);
        reset_grids();
        selectFilesGrid.Visibility = Visibility.Visible;

        // Read and process ASTERIX File
        string asterixFilePath = @"C:\Users\oscar\Documents\C# Projects\ASTERIX-Visualizer\ASTERIX_Visualizer\src\ArchivosASTERIX\smr_mlat_adsb_v21.ast";
        AsterixDataProcessor asterixDataProcessor = new AsterixDataProcessor();
        List<string[]> preProcessedASTERIXMessages = asterixDataProcessor.loadData(asterixFilePath);
        asterixData = asterixDataProcessor.processData(preProcessedASTERIXMessages);

        double[] startAndEndTimes = asterixData.getStartAndFinishSeconds();
        double startTime = startAndEndTimes[0];
        double endTime = startAndEndTimes[1];

        double currentTime= startTime;
        double timeStep = 1.0; // 1 second time step

        GMapOverlay currentTimeMessagesOverlay = new GMapOverlay("currentTimeMessagesOverlay");
        GMapOverlay pastTimeMessagesOverlay = new GMapOverlay("pastTimeMessagesOverlay");

        _ = UpdateMapAsync(asterixData, startTime, endTime, timeStep); 
    }

    public async Task UpdateMapAsync(ProcessedAsterixData asterixData, double startTime, double endTime, double timeStep)
    {
        double currentTime = startTime;

        GMapOverlay currentTimeMessagesOverlay = new GMapOverlay("currentTimeMessagesOverlay");
        GMapOverlay previousTimeMessagesOverlay = new GMapOverlay("previousTimeMessagesOverlay");

        while (currentTime <= endTime)
        {
            currentTimeMessagesOverlay.Markers.Clear();
            previousTimeMessagesOverlay.Markers.Clear();

            // Process data for the current time step
            ConcurrentBag<ParsedMessage> messagesAtCurrentTime = asterixData.getMessagesBySecond(currentTime);
            foreach (ParsedMessage message in messagesAtCurrentTime)
            {
                if (message != null)
                {
                    var marker = new CustomVehicleMarker(message, false).getMarkerWithPropoerties();
                    currentTimeMessagesOverlay.Markers.Add(marker);
                }
            }

            ConcurrentBag<ParsedMessage> messagesAtCurrentTimeRange = asterixData.getMessagesByRangeSecond(currentTime - 20, currentTime - 1);
            foreach (ParsedMessage message in messagesAtCurrentTimeRange)
            {
                if (message != null)
                {
                    var marker = new CustomVehicleMarker(message, true).getMarkerWithPropoerties();
                    previousTimeMessagesOverlay.Markers.Add(marker);
                }
            }

            // Update the map
            customMap.Overlays.Clear();
            customMap.Overlays.Add(previousTimeMessagesOverlay);
            customMap.Overlays.Add(currentTimeMessagesOverlay);
            customMap.Refresh();

            currentTime += timeStep;

            // Asynchronous delay to allow UI updates
            await Task.Delay(250);
        }
    }

    private void reset_grids()
    {
        // Fix for CS0029: Correctly handle the return value of getStartAndFinishSeconds(), which is a double[].
        selectFilesGrid.Visibility = Visibility.Hidden;
        mapGridHost.Visibility = Visibility.Hidden;
        tableGrid.Visibility = Visibility.Hidden;
        grid3.Visibility = Visibility.Hidden;

    }

    private void selectFilesButton_Click(object sender, RoutedEventArgs e)
    {
        reset_grids();
        selectFilesGrid.Visibility = Visibility.Visible;
    }

    private void mapButton_Click(object sender, RoutedEventArgs e)
    {
        reset_grids();
        mapGridHost.Visibility = Visibility.Visible;
    }

    private void tableButtonButton_Click(object sender, RoutedEventArgs e)
    {
        reset_grids();
        tableButton.Visibility = Visibility.Visible;
    }

    private void otherButtonButton_Click(object sender, RoutedEventArgs e)
    {
        reset_grids();
        grid3.Visibility = Visibility.Visible;
    }
}
