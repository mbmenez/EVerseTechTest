using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;


namespace EVerseTechTestApp
{
    public class CreateNewFloorEEH : IExternalEventHandler
    {
        public void Execute(UIApplication app)
        {
            UIDocument uidoc = app.ActiveUIDocument;
            Document doc = uidoc.Document;

            //Gathers interface input fields information
            string inputLengthString = EVerseTechTestAppMVVM.FloorLengthString;
            string inputWidthString = EVerseTechTestAppMVVM.FloorWidthString;
            LevelViewModel selectedLevel = EVerseTechTestAppMVVM.MainView.FloorLevelComboBox.SelectedItem as LevelViewModel;
            try
            {
                //Parses interface input fields information
                double floorLength = double.Parse(inputLengthString);
                double floorWidth = double.Parse(inputWidthString);
                ElementId levelId = selectedLevel.LevelId;

                //Creates rectangular floor profile
                IList<CurveLoop> profile = new List<CurveLoop>();
                XYZ endpoint1 = new XYZ(0, 0, 0);
                XYZ endpoint2 = new XYZ(0, floorLength, 0);
                XYZ endpoint3 = new XYZ(floorWidth, floorLength, 0);
                XYZ endpoint4 = new XYZ(floorWidth, 0, 0);
                Curve l1 = Line.CreateBound(endpoint1, endpoint2);
                Curve l2 = Line.CreateBound(endpoint2, endpoint3);
                Curve l3 = Line.CreateBound(endpoint3, endpoint4);
                Curve l4 = Line.CreateBound(endpoint4, endpoint1);
                IList<Curve> curves = new List<Curve>()
                {
                    l1,
                    l2,
                    l3,
                    l4
                };
                profile.Add(CurveLoop.Create(curves));

                //Fetches floor type Id
                ElementId floorTypeId = new FilteredElementCollector(doc).WhereElementIsElementType().OfCategory(BuiltInCategory.OST_Floors).Cast<FloorType>().First().Id;

                //Starts transaction to create floor
                Transaction tx = new Transaction(doc, "Creating floor instance");
                tx.Start();

                //Creates floor instance
                Floor createdFloorInstance = Floor.Create(doc, profile, floorTypeId, levelId);
                tx.Commit();
                List<ElementId> createdFloorInstanceId = new List<ElementId>()
                {
                    createdFloorInstance.Id
                };

                //Selects created floor instance and closes main view
                uidoc.Selection.SetElementIds(createdFloorInstanceId);
                TaskDialog.Show("Success!", "New floor instance is selected. Click 'Close' then type in 'HI' (Hide/Isolate command) to isolate instance in a view where element is visible.");
                EVerseTechTestAppMVVM.MainView.Close();
            }
            catch (Exception)
            {
                TaskDialog.Show("Failure!", "Make sure you fill in all required fields and type floor length and width in a '0' or '0,00' string format."); ;
            }

        }
        public string GetName()
        {
            return this.GetType().Name;
        }

    }
    public class ListAllWallsEEH : IExternalEventHandler
    {
        public void Execute(UIApplication app)
        {
            UIDocument uidoc = app.ActiveUIDocument;
            Document doc = uidoc.Document;
            //Fetches all instances of walls placed in the document
            List<Wall> instancedWalls = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_Walls)
                .OfClass(typeof(Wall))
                .Cast<Wall>()
                .ToList();

            //Gathers all wall element types of instanced walls
            List<string> allWallTypeNames = new List<string>();
            foreach (Wall instancedWall in instancedWalls)
            {
                string instancedWallTypeName = doc.GetElement(instancedWall.GetTypeId()).Name;
                allWallTypeNames.Add(instancedWallTypeName);
            }

            //Gets unique wall type names between all instanced wall type names to create a dictionary of wall types and the amount of times they're instanced
            List<string> uniqueWallTypeNames = allWallTypeNames.Distinct().ToList();
            Dictionary<string, int> wallTypesAndOccurrenceAmounts = new Dictionary<string, int>();
            foreach (string uniqueWallTypeName in uniqueWallTypeNames)
            {
                int occurrenceAmount = allWallTypeNames.Where(i => i == uniqueWallTypeName).Count();
                wallTypesAndOccurrenceAmounts.Add(uniqueWallTypeName, occurrenceAmount);
            }

            //Builds message to display through TaskDialog
            string message = $"There are {instancedWalls.Count} Wall Instances currenty placed in the document of {uniqueWallTypeNames.Count} different Wall Types.\n";
            if (wallTypesAndOccurrenceAmounts.Any())
            {
                message += "This is the number of existing instances for each Wall Type found: \n\n";
                foreach (var item in wallTypesAndOccurrenceAmounts)
                {
                    message += $"{item.Key}: {item.Value}\n";
                }
            }
            TaskDialog.Show("Success!", message);
            EVerseTechTestAppMVVM.MainView.Close();
        }
        public string GetName()
        {
            return this.GetType().Name;
        }
    }
}
