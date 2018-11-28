using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
//using X = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Reflection;
using WinForms = System.Windows.Forms; // Necesarios para trabajar con Windows Form
//using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Forms;

namespace RevitMongoDB
{
    public static class Util
    {
        /// <summary>
        /// transform one parameter to string
        /// </summary>
        /// <param name="param">Parameter to convert</param>
        /// <returns></returns>
        public static string ParameterToString(Parameter param)
        {
            string val = "none";

            if (param == null)
            {
                return val;
            }

            switch (param.StorageType)
            {
                case StorageType.Double:
                    double dval = param.AsDouble();
                    val = dval.ToString();
                    break;

                case StorageType.Integer:
                    int iVal = param.AsInteger();
                    val = iVal.ToString();
                    break;

                case StorageType.String:
                    string sVal = param.AsString();
                    val = sVal;
                    break;

                case StorageType.ElementId:
                    ElementId idVal = param.AsElementId();
                    val = idVal.IntegerValue.ToString();
                    break;

                case StorageType.None:
                    break;

                default:
                    break;
            }
            return val;
        }

        public static bool IsPhysicalElement(this Element e)
        {
            if (e.Category == null) return false;
            if (e.ViewSpecific) return false;
            // exclude specific unwanted categories
            if (((BuiltInCategory)e.Category.Id.IntegerValue) == BuiltInCategory.OST_HVAC_Zones) return false;
            if (((BuiltInCategory)e.Category.Id.IntegerValue) == BuiltInCategory.OST_SharedBasePoint) return false;
            if (((BuiltInCategory)e.Category.Id.IntegerValue) == BuiltInCategory.OST_ProjectBasePoint) return false;
            if (((BuiltInCategory)e.Category.Id.IntegerValue) == BuiltInCategory.OST_VolumeOfInterest) return false;
            if (((BuiltInCategory)e.Category.Id.IntegerValue) == BuiltInCategory.OST_SectionBox) return false;
            if (((BuiltInCategory)e.Category.Id.IntegerValue) == BuiltInCategory.OST_Materials) return false;
            if (((BuiltInCategory)e.Category.Id.IntegerValue) == BuiltInCategory.OST_Lines) return false;
            return e.Category.CategoryType == CategoryType.Model && e.Category.CanAddSubcategory;
        }

        public static List<Element> AllElement(Document doc)
        {
            List<Element> AllElem = new FilteredElementCollector(doc)
                    .WhereElementIsNotElementType()
                    .Where(e => e.IsPhysicalElement())

                    .ToList<Element>();

            return AllElem;
        }
    }
}