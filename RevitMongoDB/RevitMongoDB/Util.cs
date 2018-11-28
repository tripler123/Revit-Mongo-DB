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

    }
}