using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Dev_TicariOtomasyon
{
    internal class SqlBaglanti
    {
        public SqlConnection Connection()
        {
            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-RHMNBG1\SQLEXPRESS;Initial Catalog=DboTicariOtomasyon;Integrated Security=True");
            conn.Open();
            return conn;
        }
    }
}
