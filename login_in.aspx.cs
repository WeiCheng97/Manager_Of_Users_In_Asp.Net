using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class login_in : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        

    }
    public void logini()
    {
        
        string str = "server=127.0.0.1;database=user;uid=sa;pwd=970527j;Trusted_Connection=no";
        SqlConnection conn = new SqlConnection(str);
        conn.Open();
        string sql = "select * from userInfo where userName = @userName and password = @password";
        SqlCommand comm = new SqlCommand(sql, conn);
        comm.Parameters.Add("userName", TextBox1.Text);
        comm.Parameters.Add("password", TextBox2.Text);
        SqlDataReader sdr = comm.ExecuteReader();
        if (sdr.Read())
        {
            lblMessage.Text = "登录成功";//调试语句，正式使用时删除
            Session["userName"] = TextBox1.Text;
            Session["password"] = TextBox2.Text;
            Response.Write("<script>alert('登录成功');location.href='index.aspx';</script>");

            //创建session
            Session["n"] = 1;
            Session["namx"] = 1;
            Session["bh"] = 1;
        }
        else
        {
            lblMessage.Text = "用户名或密码错误！";
            //Response.Redirect("login.aspx");  
        }
        conn.Close();
        
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //queryUserInfo();  
        logini();
    }
}