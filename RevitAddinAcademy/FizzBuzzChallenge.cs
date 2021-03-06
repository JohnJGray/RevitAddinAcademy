#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

#endregion

namespace RevitAddinAcademy
{
    [Transaction(TransactionMode.Manual)]
    public class FizzBuzzChallenge : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            string filename = doc.PathName;

            double offset = 0.05;
            double offsetCalc = offset * doc.ActiveView.Scale;

            XYZ curPoint = new XYZ(0, 0, 0);
            XYZ offsetPoint = new XYZ(0,offsetCalc, 0);
            
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfClass(typeof(TextNoteType));

            Transaction t = new Transaction(doc, "FizzBuzz Challenge");
            t.Start();

            int range = 100;
            for (int i = 1; i <= range; i++)
            {
                string fizzbuzzString = "";

                if (i % 3 == 0 && i % 5 == 0)
                {
                    fizzbuzzString = "FizzBuzz";
                }

                else if (i % 3 == 0)
                {
                    fizzbuzzString = "Fizz";
                }

                else if (i % 5 == 0)
                {
                    fizzbuzzString = "Buzz";
                }

                else
                {
                    fizzbuzzString = i.ToString();
                }

                TextNote.Create(doc, doc.ActiveView.Id, curPoint, fizzbuzzString, collector.FirstElementId());
                curPoint = curPoint.Subtract(offsetPoint);
            }

            t.Commit();
            t.Dispose();

            return Result.Succeeded;
        }

    }
}
