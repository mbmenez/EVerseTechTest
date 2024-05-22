using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using EVerseTechTestApp;
using static EVerseTechTestApp.ExternalApplication;

namespace EVerseTechTestApp
{
    [Regeneration(RegenerationOption.Manual)]
    [Transaction(TransactionMode.Manual)]
    internal class EVerseTechTestEC : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                UIApplication uiapp = commandData.Application;
                eVerseTechTestApp.uiApp = uiapp;
                eVerseTechTestApp.ShowEVerseTechTestAppUI();
                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                message = ex.Message + "\n\n" + ex.StackTrace;
                return Result.Failed;
            }
        }
    }
}
