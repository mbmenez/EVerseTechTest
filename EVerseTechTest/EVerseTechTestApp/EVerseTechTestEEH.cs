using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;


namespace EVerseTechTestApp
{
    public class Action1EEH : IExternalEventHandler
    {
        public void Execute(UIApplication app) 
        { 

        }
        public string GetName() 
        {
            return this.GetType().Name;
        }

    }
}
