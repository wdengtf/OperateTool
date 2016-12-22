using Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace YYT.BLL.Common
{    /// <summary>
    /// 通用分页类
    /// </summary>
    public class CommonPage
    {
        private SqlBO sqlBo = new SqlBO();
        public CommonPage()
        {

        }

        public int PageSize = 10;
        public string Table = "";
        public string columns = "";
        public string where = " 1=1";
        public string Order = " id desc";
        private int PageCount = 0;

        /// <summary>
        /// 返回DataTable和总记录数
        /// </summary>
        /// <param name="CurrentPage"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public DataTable getDataByPage(int CurrentPage, out int recordCount)
        {
            DataTable ds = new DataTable();
            if (where == "") where = " 1=1";
            if (CurrentPage < 1) CurrentPage = 1;
            //Table 需要替换掉left join， left join导致count很慢
            string countTable = Table;
            if (Table.ToLower().IndexOf("left join") > 0)
            {
                countTable = Regex.Split(Table.ToLower(), "left join", RegexOptions.IgnoreCase)[0];
            }

            recordCount = int.Parse(sqlBo.GetdataBySql("select count(*) from " + countTable + " where " + where).Rows[0][0].ToString());
            string sql = "select top " + PageSize + "  * from ";
            sql += " ( select top " + CurrentPage * PageSize + "  ROW_NUMBER() OVER(ORDER BY " + Order + ") as RowNum ," + columns + " from " + Table + " where " + where + " ) as tempTable";
            sql += " where RowNum Between " + ((int)(CurrentPage - 1) * PageSize + 1) + " and " + CurrentPage * PageSize + " Order by RowNum asc";


            ds = sqlBo.GetdataBySql(sql);
            return ds;
        }
        /// <summary>
        /// 分页sql语句
        /// </summary>
        /// <param name="fldName">排序字段名称</param>
        /// <param name="PageIndex">当期页码</param>
        /// <param name="OrderType">排序类型非 0 值则降序</param>
        /// <returns></returns>
        public string GetString(int CurrentPage)
        {
            string sql = "select top " + PageSize + "  * from ";
            sql += " ( select top " + CurrentPage * PageSize + "  ROW_NUMBER() OVER(ORDER BY " + Order + ") as RowNum ," + columns + " from " + Table + " where " + where + " ) as tempTable";
            sql += " where RowNum Between " + ((int)(CurrentPage - 1) * PageSize + 1) + " and " + CurrentPage * PageSize + " Order by RowNum asc";
            return sql;
        }

        /// <summary>
        /// getJsonByPage
        /// </summary>
        /// <param name="CurrentPage"></param>
        /// <param name="PageCount"></param>
        /// <returns></returns>
        public string getJsonByPage(int CurrentPage)
        {
            DataTable ds = new DataTable();
            if (CurrentPage < 1) CurrentPage = 1;
            if (where == "") where = " 1=1";
            int recordCount = int.Parse(sqlBo.GetdataBySql("select count(*) from " + Table + " where " + where).Rows[0][0].ToString());
            if (recordCount % PageSize == 0)
            {
                PageCount = recordCount / PageSize;
            }
            else
            {
                PageCount = (int)(recordCount / PageSize) + 1;
            }
            string sql = "select top " + PageSize + "  * from ";
            sql += " ( select top " + CurrentPage * PageSize + "  ROW_NUMBER() OVER(ORDER BY " + Order + ") as RowNum ," + columns + " from " + Table + " where " + where + " ) as tempTable";
            sql += " where RowNum Between " + ((int)(CurrentPage - 1) * PageSize + 1) + " and " + CurrentPage * PageSize + " Order by RowNum asc";
            ds = sqlBo.GetdataBySql(sql);
            string json = "";
            string dtjson = Utility.ToJson(ds);
            json = "{\"page\":\"" + CurrentPage + "\",\"total\":\"" + PageCount + "\",\"records\":\"" + recordCount + "\",\"rows\":" + dtjson + "}";
            return json;
        }

        /// <summary>
        /// CTE分页
        /// </summary>
        /// <param name="CurrentPage"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public string getJsonByCTEPage(int CurrentPage, string sql)
        {
            DataTable ds = new DataTable();
            if (CurrentPage < 1) CurrentPage = 1;
            if (where == "") where = " 1=1";
            int recordCount = int.Parse(sqlBo.GetdataBySql("select count(*) from " + Table + " where " + where).Rows[0][0].ToString());
            if (recordCount % PageSize == 0)
            {
                PageCount = recordCount / PageSize;
            }
            else
            {
                PageCount = (int)(recordCount / PageSize) + 1;
            }
            ds = sqlBo.GetdataBySql(sql);
            string json = "";
            string dtjson = Utility.ToJson(ds);
            json = "{\"page\":\"" + CurrentPage + "\",\"total\":\"" + PageCount + "\",\"records\":\"" + recordCount + "\",\"rows\":" + dtjson + "}";
            return json;
        }


        #region 分页sql语句
        /// <summary>
        /// 分页sql语句
        /// </summary>
        /// <param name="fldName">排序字段名称</param>
        /// <param name="PageIndex">当期页码</param>
        /// <param name="OrderType">排序类型非 0 值则降序</param>
        /// <returns></returns>
        public string GetString(string fldName, int PageIndex, int OrderType)
        {
            string strSQL = "";
            string strTmp = "";
            string strOrder = "";
            if (OrderType != 0)
            {
                strTmp = "<(select min";
                strOrder = " order by " + fldName + " desc";
            }
            else
            {
                strTmp = ">(select max";
                strOrder = " order by " + fldName + " asc";
            }

            if (PageIndex == 1)
            {
                strSQL = "select top " + PageSize + " " + columns + " from " + Table;
                if (!String.IsNullOrEmpty(where)) strSQL += " where " + where;

                strSQL += strOrder;
            }
            else
            {
                strSQL = "select top " + PageSize + " " + columns + " from " + Table + " where " + fldName + strTmp + "(" + fldName + ")" + " from (select top " + (PageIndex - 1) * PageSize + " " + fldName + " from " + Table + strOrder + ") as tblTmp)" + strOrder;
                if (where != "")
                    strSQL = "select top " + PageSize + " " + columns + " from " + Table + " where " + fldName + strTmp + "(" + fldName + ")" + " from (select top " + (PageIndex - 1) * PageSize + " " + fldName + " from " + Table + " where " + where + strOrder + ") as tblTmp) and " + where + strOrder;
            }
            return strSQL;
        }
        #endregion
    }
}
