using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;//חשוב!
using System.Windows.Forms;//עבור ההודעות!
using System.Data;

namespace StreetGames
{
    public class SQL_CON
    {
        SqlConnection conn;

        public SQL_CON()
        {
            conn = new SqlConnection("Server=YOUR_SERVER;Database=YOUR_DATABASE;Trusted_Connection=True;");//update this!!
        }

        public void execute_non_query(SqlCommand cmd)
        {
            try
            {
                // open a connection object
                this.conn.Open();
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                // NOTE: success popup intentionally removed — only show messages on error.
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DB ERROR", MessageBoxButtons.OK);
            }
            finally
            {
                conn.Close();
            }
        }

        // execute non-query without message boxes (silent)
        public int execute_non_query_no_msg(SqlCommand cmd)
        {
            try
            {
                this.conn.Open();
                cmd.Connection = conn;
                return cmd.ExecuteNonQuery();
            }
            catch
            {
                return -1;
            }
            finally
            {
                conn.Close();
            }
        }

        // execute scalar and return object (e.g. SELECT SCOPE_IDENTITY())
        public object execute_scalar(SqlCommand cmd)
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                // Show the real error (constraint, FK, NULL not allowed, duplicate key, etc.)
                MessageBox.Show(ex.ToString(), "DB ERROR (execute_scalar)", MessageBoxButtons.OK);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }


        public SqlDataReader execute_query(SqlCommand cmd)
        {
            try
            {
                // open a connection object
                conn.Open();
                cmd.Connection = conn;
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "DB ERROR", MessageBoxButtons.OK);
                if (conn.State == ConnectionState.Open) conn.Close();
                return null;

            }
        }

    }

}

