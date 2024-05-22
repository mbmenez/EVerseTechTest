using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
//using ParameterScannerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;


namespace EVerseTechTestApp
{
    public partial class EVerseTechTestAppMVVM : Window, IDisposable
    {

        public static string FloorLengthString {  get; set; }
        public static string FloorWidthString { get; set; }
        public static EVerseTechTestAppMVVM MainView { get; set; }
        public static ObservableCollection<LevelViewModel> LevelViewModels { get; set; }
        public static Document Document { get; internal set; }
        public static UIDocument UIDocument { get; internal set; }
        public static UIApplication Application { get; internal set; }
        public static ExternalEvent CreateNewFloorEvent { get; set; } = ExternalEvent.Create(new CreateNewFloorEEH());
        public static ExternalEvent ListAllWallsEvent { get; set; } = ExternalEvent.Create(new ListAllWallsEEH());

        public EVerseTechTestAppMVVM(UIApplication uiapp, UIDocument uidoc, Document doc)
        {
            Application = uiapp;
            UIDocument = uidoc;
            Document = doc;
            LevelViewModels = new ObservableCollection<LevelViewModel>();
            EVerseTechTestUIService.PopulateLevelComboBox(doc);
            MainView = this;
            DataContext = this;
            InitializeComponent();
        }
        
        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        public void List_All_Walls_Button_Click(object sender, RoutedEventArgs e)
        {
            ListAllWallsEvent.Raise();
        }
        public void Create_New_Floor_Button_Click(object sender, RoutedEventArgs e)
        {
            CreateNewFloorEvent.Raise();
        }


        private void FloorLengthTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string currentText = FloorLengthTextBox.Text;
            if (currentText.Contains("."))
            {
                TaskDialog.Show("Incorrect format!", "Use ',' instead of '.' for decimal values.");
            }
            else
            {
                FloorLengthString = currentText;
            }
        }
        private void FloorWidthTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string currentText = FloorWidthTextBox.Text;
            FloorWidthString = currentText;
        }
    }
    public class LevelViewModel: INotifyPropertyChanged 
    {
        public static Document Document { get; internal set; }
        public static UIDocument UIDocument { get; internal set; }
        public static UIApplication UIApplication { get; internal set; }
        public string LevelName {  get; set; }
        public ElementId LevelId { get; set; }
        public LevelViewModel(Document doc, Level level)
        {
            LevelName = level.Name;
            LevelId = level.Id;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
    



}