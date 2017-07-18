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
using System.Threading;

public partial class index : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["userConnectionString"].ToString();
    string s_yhh;


    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (Session["userName"] == null)
        {
            Response.Write("<script>alert('请登录');</script>");
            Response.Redirect("login_in.aspx");
        }
        
        //绑定数据源
        Bind();
        

    }

    //绑定数据源
    protected void Bind()
    {
        string date = In_Date.Text;

        SqlConnection con = new SqlConnection(constr);
        con.Open();
        string cmdText = "SELECT bh,yhh,fpmc,fpdz,yjmc,yjdz,sjr,convert(char(10),rq,23) as rqq  FROM [user].[dbo].[temp];";
        SqlDataAdapter sda = new SqlDataAdapter(cmdText, con);
        //声明一个数据集对象  
        DataSet ds = new DataSet();
        //填充数据  
        sda.Fill(ds);
        //绑定数据源  
        GridView1.DataSource = null;
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
        con.Close();

    }

    protected void check_yhh(object sender, EventArgs e)
    {
        
        //这个里面可以写数据库操作判断数据库中是否存在
        SqlConnection conn = new SqlConnection(constr);
        conn.Open();
        string sql = "select * from [user].[dbo].[邮寄] where yhh = @yhh;";

        SqlCommand comm = new SqlCommand(sql, conn);
        comm.Parameters.Add("yhh", In_yhh.Text);
        if (comm.ExecuteNonQuery()==0)
        {
            Response.Write("<script>alert('未找到该用户，请检查后输入');</script>");
        }
        else
        {
            Select();
            Copy();
        }

        conn.Close();         
    }
    //把数据插入到fp表中
    protected void Select()
    {
        //从邮寄表读取数据
        SqlConnection conn = new SqlConnection(constr);
        conn.Open();
        string sql = "select * from [user].[dbo].[邮寄] where yhh=@yhh";
        
        SqlCommand comm = new SqlCommand(sql, conn);
        comm.Parameters.Add("yhh", In_yhh.Text);
        SqlDataReader dr = comm.ExecuteReader();
        int flag = 0;
        while(dr.Read())
        {
            Insert_One(dr);
            flag++;
        }
        if (flag == 0)
            Response.Write("<script>alert('错误代码003');</script>");
        dr.Close();
        conn.Close();

        
        
       
        // insert into fp(yhh,yhch,fpmc,fpdz,yjmc,yjdz,sjr) select yhh,,yhch,fpmc,fpdz,yjmc,yjdz,sjr from [user].[dbo].[邮寄];
    }

    protected void Insert_One(SqlDataReader dr)
    {
        SqlConnection conn = new SqlConnection(constr);
        conn.Open();
        int bh = (int)Session["bh"];
        string sql = "insert into fp(bh,yhh,yhch,fpmc,fpdz,yjmc,yjdz,sjr,yb,rq) values(@bh,@yhh,@yhch,@fpmc,@fpdz,@yjmc,@yjdz,@sjr,@yb,@rq)";
        SqlCommand comm = new SqlCommand(sql, conn);
        comm.Parameters["@bh"].Value = bh.ToString();
        comm.Parameters["@yhh"].Value = dr["yhh"].ToString();
        comm.Parameters["@yhch"].Value = dr["yhch"].ToString();
        comm.Parameters["@fpmc"].Value = dr["yjmc"].ToString();
        comm.Parameters["@fpdz"].Value = dr["fpdz"].ToString();
        comm.Parameters["@yjmc"].Value = dr["yjmc"].ToString();
        comm.Parameters["@yjdz"].Value = dr["yjdz"].ToString();
        comm.Parameters["@sjr"].Value = dr["sjr"].ToString();
        comm.Parameters["@yb"].Value = dr["yb"].ToString();
        comm.Parameters["@rq"].Value = In_Date.Text;
        Session["bh"] = (int)Session["bh"] + 1;
        if(comm.ExecuteNonQuery()==0)
            Response.Write("<script>alert('错误代码001');</script>");
        conn.Close();

    }

    //把数据从fp中存到temp表中
    protected void Copy()
    {
        SqlConnection conn = new SqlConnection(constr);
        conn.Open();
        string sql = "insert into temp(bh, yhh, yhch, fpmc, fpdz, yjmc, yjdz, sjr, yb, rq) select bh, yhh, yhch, fpmc, fpdz, yjmc, yjdz, sjr, yb, rq from [user].[dbo].[fp] where yhh = @yhh and rq=@rq";
        SqlCommand comm = new SqlCommand(sql, conn);
        comm.Parameters.Add("yhh",In_yhh.Text);
        comm.Parameters.Add("rq", In_Date.Text);
        if (comm.ExecuteNonQuery() == 0)
            Response.Write("<script>alert('错误代码002');</script>");
        conn.Close();
        Bind();
    }





    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    //另存为
    protected void BSave_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=GridView1.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.xls";
        StringWriter StringWriter = new System.IO.StringWriter();
        HtmlTextWriter HtmlTextWriter = new HtmlTextWriter(StringWriter);
        GridView1.RenderControl(HtmlTextWriter);
        Response.Write(StringWriter.ToString());
        Response.End();
    }

    //清空表
    protected void BClear_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(constr);
        conn.Open();
        string sql = "TRUNCATE TABLE temp";
        SqlCommand comm = new SqlCommand(sql, conn);
        comm.ExecuteReader();
        conn.Close();
        Bind();
    }

    protected void Insert_Click(object sender, EventArgs e)
    {
        check_yhh(sender, e);
    }
}