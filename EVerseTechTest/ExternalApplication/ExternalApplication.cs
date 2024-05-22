using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Reflection;
using System.Windows.Media.Imaging;
using System.Diagnostics;
using System.IO;
using EVerseTechTestResources;


namespace EVerseTechTestApp

{
    public class ExternalApplication : IExternalApplication
    {
        //Adds ribbon and button
        public ExternalApplication() { }
        public static string AutodeskUserId { get; set; } = null;
        public static ExternalApplication eVerseTechTestApp = new ExternalApplication();
        internal UIApplication uiApp = null;
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
        public Result OnStartup(UIControlledApplication application)
        {
            AddRibbonButtons(application);

            return Result.Succeeded;
        }
        private void AddRibbonButtons(UIControlledApplication application)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string executingAssemblyPath = assembly.Location;
            Debug.Print(executingAssemblyPath);
            string executingAssemblyName = assembly.GetName().Name;
            Console.WriteLine(executingAssemblyName);
            string tabName = "E-Verse Test";

            try { application.CreateRibbonTab(tabName); } catch (Exception) { }

            RibbonPanel dataPanel = application.CreateRibbonPanel(tabName, "E-Verse Test");

            BitmapImage bitmapImage = ResourceImage.GetIcon("EVerseLogo30x30.png");

            PushButtonData EVerseTechTestPBD = new PushButtonData("E-Verse Test", "E-Verse\nTest", executingAssemblyPath, typeof(EVerseTechTestEC).FullName)
            {
                ToolTip = "Creates rectangular floor instance and lists all walls in document.",
                LargeImage = bitmapImage
            };
            dataPanel.AddItem(EVerseTechTestPBD);
        }

        //Defines UI behavior
        internal EVerseTechTestAppMVVM eVerseTechTestAppMVVM = null;
        internal void ShowEVerseTechTestAppUI()
        {
            if (eVerseTechTestAppMVVM == null || eVerseTechTestAppMVVM.IsLoaded == false)
            {
                UIDocument uidoc = uiApp.ActiveUIDocument;
                Document doc = uidoc.Document;
                eVerseTechTestAppMVVM = new EVerseTechTestAppMVVM(uiApp, uidoc, doc);
                eVerseTechTestAppMVVM.Show();
            }
        }
    }
}
