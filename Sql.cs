using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hastane_Sistemi
{
    class Sql
    {
        public SqlConnection bgl()
        {
            SqlConnection bgl = new SqlConnection("Data Source=EMRE_SEFEROGLU\\SQLEXPRESS;Initial Catalog=HastaneSistemi;Integrated Security=True;Encrypt=False");
            bgl.Open();
            return bgl;
        }

    }

}
