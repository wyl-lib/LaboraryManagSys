using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace initView.Properties
{
    class DBTool
    {
        static string MySqlCon = "Data Source=LAPTOP-U65JC2DT\\EXAMPLENAME;Initial Catalog = Laborary; User ID=sa; Password=123123";
        static SqlConnection con = new SqlConnection(@MySqlCon);
        /**
         * job:用于查询
         * param:sqlStr
         * return: 返回一个DataTable的对象
         * 
        */
        public static DataSet ExecuteQuery(string sqlStr)
        {
            //设置连接到数据库的SqlConnection
            DataSet ds = new DataSet();
            try
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(sqlStr, con);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                closeServer();
            }
            return ds;
            /*
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.Text;
                        //Console.WriteLine("CommandType.Text: " + CommandType.Text);
                        cmd.CommandText = sqlStr;
                        DataTable dt = new DataTable();
                        SqlDataAdapter msda;
                        msda = new SqlDataAdapter(cmd);
                        msda.Fill(dt);
                        //Console.WriteLine("msda.Fill(dt): " + msda.Fill(dt));
                        con.Close();
                        return dt;
            */
        }

        /*
         * job：用于增删改
         * param：sqlStr
         * return: 返回受影响的行数int类型
         */
        public static int ExecuteUpdate(string sqlStr)
        {
            SqlConnection con = new SqlConnection(@MySqlCon);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sqlStr;
            /* string firstString = sqlStr.Substring(0, 1);
            if (firstString.Equals("i")|| firstString.Equals("I"))
            {
                return 1;
            }*/
            int iud = cmd.ExecuteNonQuery();
            //执行T-SQL并返回受影响的行数
            Console.WriteLine("受影响的行数为: " + iud);
            closeServer();
            return iud;
        }

        /*
         * job：调用数据库数据填充dataGridView.ItemsSource
         * param：sql,tableName
         * return: 
         */
        public static DataView queryInfo(String sql, String tableName)
        {
            //创建SqlDataAdapter实例da，并指定SQL查询string和SqlConnection
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            closeServer();

            DataSet ds = new DataSet();
            //从数据库中读取数据，并填充ds
            da.Fill(ds, tableName);
            //创建DataView实例dv，并指定其DataTable
            DataView dv = new DataView(ds.Tables[tableName]);
            return dv;
        }

        /*调用数据库数据填充dataGridView.ItemsSource
        public static DataView dataViewInfo(String sql, String tableName)
        {
            //创建SqlDataAdapter实例da，并指定SQL查询string和SqlConnection
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            closeServer();

            DataSet ds = new DataSet();
            //从数据库中读取数据，并填充ds
            da.Fill(ds, tableName);
            //创建DataView实例dv，并指定其DataTable
            DataView dv = new DataView(ds.Tables[tableName]);
            return dv;
        }

         private void DataGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string sql = "select * from kc_billStatistics where uID='111'";
            DataView dv = DBTool.dataViewInfo(sql, "kc_billStatistics");
            dataGridView.ItemsSource = dv;  //设置DataGrid的ItemsSource属性
        }

        */

        public static void closeServer()
        {
            if (con != null)
            {
                con.Close();
            }
        }

    }

}

