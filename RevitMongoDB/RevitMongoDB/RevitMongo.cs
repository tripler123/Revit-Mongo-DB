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
        

        public Result Execute(ExternalCommandData commandData, 
                              ref string message, 
                              ElementSet elements)
        {
            UIApplication uiApp = commandData.Application;
            UIDocument uiDoc = uiApp.ActiveUIDocument;
            Document doc = uiDoc.Document;

            string projectName = doc.ProjectInformation.UniqueId;
            IMongoCollection<MongoElement> collection = db.GetCollection<MongoElement>(projectName);

            List<Element> lstElement = new List<Element>();

            lstElement = Util.AllElement(doc);

            string idElemento = "";
            string elementName = "";
            string elementLevel = "";

            foreach (Element el in lstElement)
            {
                idElemento = el.Id.ToString();
                elementName = Util.ParameterToString(el.LookupParameter("Elemento"));
                elementLevel = Util.ParameterToString(el.LookupParameter("Nivel del Elemento"));
                MongoElement mongoEl = new MongoElement(elementName, elementLevel, idElemento);
                collection.InsertOne(mongoEl);
            }

            TaskDialog.Show("Mensaje de Finalizacion", "Usted Ingresado la Información con exito");
            

            return Result.Succeeded;
        }
    }
}
