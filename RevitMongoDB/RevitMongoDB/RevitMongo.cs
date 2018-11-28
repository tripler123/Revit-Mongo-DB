using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//NameSpaces
using Autodesk.Revit.DB;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.Attributes;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using MongoDB.Driver;

namespace RevitMongoDB
{
    [Transaction(TransactionMode.ReadOnly)]
    public class RevitMongo : IExternalCommand
    {
        static MongoClient client = new MongoClient();
        static IMongoDatabase db = client.GetDatabase("RevitDB");
        static IMongoCollection<MongoElement> collection = db.GetCollection<MongoElement>("mongoElements");

        public Result Execute(ExternalCommandData commandData, 
                              ref string message, 
                              ElementSet elements)
        {
            UIApplication uiApp = commandData.Application;
            UIDocument uiDoc = uiApp.ActiveUIDocument;
            Document doc = uiDoc.Document;

            IList<Reference> pickedObjs = uiDoc.Selection.PickObjects(ObjectType.Element, "Selecciones los elementos");
            List<ElementId> ids = (from Reference r in pickedObjs select r.ElementId).ToList();
            List<Element> lstElement = new List<Element>();

            

            Element e = doc.GetElement(ids[0]);

            string elementName = "";
            string elementLevel = "";

            elementName =  Util.ParameterToString(e.LookupParameter("Elemento"));
            elementLevel = Util.ParameterToString(e.LookupParameter("Nivel del Elemento"));
            MongoElement mongoEl = new MongoElement(elementName, elementLevel);
            collection.InsertOne(mongoEl);
            TaskDialog.Show("Mensaje de Finalizacion", "Usted Ingresado la Información con exito");
            

            return Result.Succeeded;
        }
    }
}
