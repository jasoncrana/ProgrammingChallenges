using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.ComponentModel;

namespace ToDoList.Model
{
  public class Todo
  {
    public string item { get; set; }
    public int id { get; set; }
    public bool done { get; set; }
    public int diff { get; set; }

    private string tConn = ConfigurationManager.ConnectionStrings["tConn"].ToString();

    public Todo()
    {
      item = "";
      id = 0;
      done = false;
      diff = 0;
    }

    public void newItem()
    {
      using (SqlConnection con = new SqlConnection(tConn))
      {
        string q = $"INSERT INTO Todo VALUES ('{item}',0,'{diff}') ";
        SqlCommand c = new SqlCommand(q, con);
        con.Open();
        c.ExecuteNonQuery();
      }
    }

    public bool readItem()
    {
      using (SqlConnection con = new SqlConnection(tConn))
      {
        string q = $"SELECT * FROM Todo WHERE id={id}";
        SqlCommand c = new SqlCommand(q, con);
        con.Open();
        SqlDataReader rows = c.ExecuteReader();
        if (rows.HasRows) while (rows.Read())
          {
            item = rows["item"].ToString();
            id = Convert.ToInt32(rows["id"]);
            done = (bool)rows["done"];
            diff = Convert.ToInt32(rows["difficulty"]);
            return true;
          }
      }
      return false;
    }
    public void updateItem()
    {
      using (SqlConnection con = new SqlConnection(tConn))
      {
        string q = $"UPDATE Todo SET item={item},difficulty={diff} WHERE id={id}";
        SqlCommand c = new SqlCommand(q, con);
        con.Open();
        c.ExecuteNonQuery();
      }
    }

    public void completeItem()
    {
      using (SqlConnection con = new SqlConnection(tConn))
      {
        string q = $"Update Todo SET done='True' WHERE id={id}";
        SqlCommand c = new SqlCommand(q, con);
        con.Open();
        c.ExecuteNonQuery();
      }
    }

    public override string ToString()
    {
      string t = "<tr><td>";
      t += "<input type='checkbox' ";
      t += (done) ? "checked " : "";
      t += $"id='cb{id}' name='cb{id}' /></td>" +
        $"<td class='diff{diff}'>{item}</td></tr>\n";
      return t;
    }
  }
}