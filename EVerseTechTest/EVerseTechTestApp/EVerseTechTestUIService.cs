using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Autodesk.Revit.Attributes;

namespace EVerseTechTestApp
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class EVerseTechTestUIService
    {
        public static Document Document { get; internal set; }
        public static UIDocument UIDocument { get; internal set; }
        public static UIApplication UIApplication { get; internal set; }
        public static void PopulateLevelComboBox(Document doc) 
        {
            try
            {
                Document = doc;

                List<Level> Levels = new FilteredElementCollector(doc)
                    .OfClass(typeof(Level))
                    .Cast<Level>()
                    .OrderBy(lvl => lvl.Elevation)
                    .ToList();

                foreach (Level lvl in Levels) 
                { 
                    LevelViewModel levelViewModel = new LevelViewModel(doc, lvl);
                    levelViewModel.LevelName = lvl.Name.ToString(); 
                    levelViewModel.LevelId = lvl.Id;
                    EVerseTechTestAppMVVM.LevelViewModels.Add(levelViewModel);
                }
               
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
