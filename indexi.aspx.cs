using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;
using System.Text;

public partial class index : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void BSearch1_Click(object sender, EventArgs e)
    {
        string date = In_Date.Text;
        string constr = ConfigurationManager.ConnectionStrings["userConnectionString"].ToString();
        SqlConnection con = new SqlConnection(constr);
        con.Open();
        string cmdText = "SELECT yhh,fpmc,fpdz,yjmc,yjdz,sjr,convert(char(10),rq,23) as rqq  FROM [user].[dbo].[fp] where rq= '" + date+ "'";
        SqlDataAdapter sda = new SqlDataAdapter(cmdText, con);
        //声明一个数据集对象  
        DataSet ds = new DataSet();
        //填充数据  
        sda.Fill(ds);
        //绑定数据源  
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
        con.Close();
    }

    protected void BSearch2_Click(object sender, EventArgs e)
    {
        string yhh = In_Num.Text;
        string constr = ConfigurationManager.ConnectionStrings["userConnectionString"].ToString();
        SqlConnection con = new SqlConnection(constr);
        con.Open();
        string cmdText = "SELECT yhh,fpmc,fpdz,yjmc,yjdz,sjr,convert(char(10),rq,23) as rqq  FROM [user].[dbo].[fp] where yhh= '" + yhh + "'  ORDER BY rqq";
        SqlDataAdapter sda = new SqlDataAdapter(cmdText, con);
        //声明一个数据集对象  
        DataSet ds = new DataSet();
        //填充数据  
        sda.Fill(ds);
        //绑定数据源  
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
        con.Close();
    }


   
}