using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing;

namespace Microsoft.Dynamics.Framework.Reports.SharedLibrary
{
    public static class ReportHelper
    {
        public static bool IsOracle()
        {
            AxaptaWrapper Ax = SessionManager.GetSession();
            AxaptaObjectWrapper DBSystem = Ax.CreateAxaptaObject("SysSqlSystem");

            return (int)DBSystem.Call("databaseId") == 3;
        }

        public static DataTable GetDefaultCompany(string CompanyName)
        {
            DataTable dTable = new DataTable();
            dTable.Columns.Add("CompanyKey");
            dTable.Columns.Add("CompanyValue");
            if (!string.IsNullOrEmpty(CompanyName))
            {
                if (IsOracle())
                {
                    CompanyName = CompanyName.ToLower();
                }

                dTable.Rows.Add(CompanyName, String.Format("[Company].[Company accounts].&[{0}]", CompanyName));
            }

            return dTable;
        }

        public static DataTable GetDefaultEmployee()
        {
            AxaptaWrapper Ax = SessionManager.GetSession();
            string EmpId = (string)Ax.CallStaticClassMethod("smmUtility", "getCurrentContact");
            string EmpName = (string)Ax.CallStaticRecordMethod("EmplTable", "emplId2Name", EmpId);

            DataTable dTable = new DataTable();
            dTable.Columns.Add("EmployeeId");
            dTable.Columns.Add("EmployeeName");

            if (!string.IsNullOrEmpty(EmpId))
            {
                dTable.Rows.Add(EmpId, EmpName);
            }

            return dTable;
        }

        public static DataTable GetDefaultDateRange()
        {
            DataTable dTable = new DataTable();
            dTable.Columns.Add("DateFrom", System.Type.GetType("System.DateTime"));
            dTable.Columns.Add("DateTo", System.Type.GetType("System.DateTime"));

            dTable.Rows.Add(DateTime.Today.AddYears(-1), DateTime.Today);

            return dTable;
        }

        public static DataTable GetAllDateRange()
        {
            DataTable dTable = new DataTable();
            dTable.Columns.Add("DateFrom", System.Type.GetType("System.DateTime"));
            dTable.Columns.Add("DateTo", System.Type.GetType("System.DateTime"));

            dTable.Rows.Add(Convert.ToDateTime("1900-01-01"), DateTime.Today);

            return dTable;
        }

        ///<summary>
        /// Method to get a color hex code based on the total grouping
        ///</summary>
        /// <param name="index">
        /// series/data/category number
        /// </param>
        /// <param name="count">
        /// total number of series/data/category
        /// </param>
        public static string GetCustomColor(int index, int count)
        {
            if (count <= 6)
            {
                return GetPredefinedColor(index - 1, 0);
            }

            int R1 = 51, R2 = 225;
            int G1 = 66, G2 = 231;
            int B1 = 09, B2 = 205;
            decimal RS1 = 0;
            decimal GS1 = 0;
            decimal BS1 = 0;

            if (count > 1)
            {
                RS1 = (R2 - R1) / (count - 1);
                GS1 = (G2 - G1) / (count - 1);
                BS1 = (B2 - B1) / (count - 1);
            }

            int R = R1 + (int)Math.Round(RS1 * (index - 1), 0);
            int G = G1 + (int)Math.Round(GS1 * (index - 1), 0);
            int B = B1 + (int)Math.Round(BS1 * (index - 1), 0);

            return ColorTranslator.ToHtml(Color.FromArgb(R, G, B));
        }

        ///<summary>
        /// Method to get a color hex code from a predefined palette
        ///</summary>
        /// <param name="index">
        /// Color number to use. Valid values are 0-5
        /// </param>
        /// <param name="position">
        /// 0 - lower (darker color)
        /// 1 - upper (lighter color)
        /// </param>
        public static string GetPredefinedColor(int index, int position)
        {
            string[,] HexColorCode = new string[6, 2] {
                {"#79A113", "#BCD089"},
                {"#B0CB69", "#D8E5B4"},
                {"#526C0E", "#A9B687"},
                {"#C2CF9B", "#E1E7CD"},
                {"#9EC432", "#CFE299"},
                {"#334209", "#99A184"}
                };

            return HexColorCode[index, position];
        }

        /// <summary>
        /// This method returns the result of a query object by passing the needed
        /// parameters.
        /// </summary>
        /// <param name="queryName">The name of the AX Query object</param>
        /// <param name="rangeNames">An array containing the range field names</param>
        /// <param name="rangeValues">An array containing the range filter values</param>
        /// <returns>A data table containing the result from execution of the AX Query</returns>
        public static DataTable GetParameterDataTable(string queryName,
            object[] rangeNames, object[] rangeValues)
        {
            if (queryName == null)
                throw new ArgumentNullException("queryName");
            if (rangeNames == null)
                throw new ArgumentNullException("rangeNames");
            if (rangeValues == null)
                throw new ArgumentNullException("rangeValues");

            IDictionary<string, object> ranges = new Dictionary<string, object>();

            // we need to have the same number of elements in both arrays
            if (rangeNames.Length != rangeValues.Length)
            {
                throw new ArgumentNullException("rangeValues");
            }

            // handle range parameters
            for (int index = 0; index < rangeNames.Length; ++index)
            {
                if (rangeValues[index] != null)
                {
                    ranges.Add(rangeNames[index].ToString(), rangeValues[index]);
                }
            }

            // execute query
            DataTable resultTable = AxQuery.ExecuteQuery((String.Format("Select * from {0}", queryName))
                , ranges);

            return resultTable;
        }

        /// <summary>
        /// Formats a DateTime value in the short date format of the culture to be used when 
        /// using the return value as an AX Query range value using the AxQuery.ExecuteQuery() API.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>The date as a string.</returns>
        public static string ToShortDateStringForAxQueryRange(this DateTime date)
        {
            return date.ToString(AxQuery.Culture.DateTimeFormat.ShortDatePattern, AxQuery.Culture);
        }
    }
}
