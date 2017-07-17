using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class manage : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["userConnectionString"].ToString();
    bool flag = true;//是否允许进行插入操作


    protected void Page_Load(object sender, EventArgs e)
    {

        
        if (Session["userName"] == null)
        {
            Response.Write("<script>alert('请登录');</script>");
            Response.Redirect("login_in.aspx");
        }
        
        SetMax();
    }

    protected void check_yhh(object sender, EventArgs e)
    {
        //这个里面可以写数据库操作判断数据库中是否存在
        SqlConnection conn = new SqlConnection(constr);
        conn.Open();
        string sql = "select count(yhh) from [user].[dbo].[邮寄] where yhh = @yhh;";
        
        SqlCommand comm = new SqlCommand(sql, conn);
        comm.Parameters.Add("yhh", In_yhbh.Text);
        SqlDataReader sdr = comm.ExecuteReader();
        if (!sdr.Read())
        {
            Response.Write("<script>alert('已有该用户，请确认用户号是否正确');</script>");
            In_yhbh.Focus();
            flag = false;
        }
        conn.Close();
        flag = true;
    }

    //重新整理id并获取最大值
    protected void SetMax()
    {
        SqlConnection conn = new SqlConnection(constr);
        conn.Open();
        //整理id
        /*
        string sql1 = "truncate table[user].[dbo].[邮寄]";
        SqlCommand comm1 = new SqlCommand(sql1, conn);
        comm1.ExecuteReader();
        conn.Close();
        */
        conn = new SqlConnection(constr);
        conn.Open();
        string sql2 = "select count(*) from [user].[dbo].[邮寄] ";
        SqlCommand comm2 = new SqlCommand(sql2, conn);
        Session["namx"] = (int)comm2.ExecuteScalar();
        
        conn.Close();
    }

    //检查必要项是否为空
    protected bool check_null()
    {
        if (In_yhbh.Text == "")
        {
            Response.Write("<script>alert('用户编号不能为空，请检查后再提交');</script>");
            In_yhbh.Focus();
            return false;
        }
        if (In_yjmc.Text == "")
        {
            Response.Write("<script>alert('邮寄名称不能为空，请检查后再提交');</script>");
            In_yjmc.Focus();
            return false;
        }
        if(In_yjdz.Text == "")
        {
            Response.Write("<script>alert('邮寄地址不能为空，请检查后再提交');</script>");
            In_yjdz.Focus();
            return false;
        }
        if (In_sjr.Text == "")
        {
            Response.Write("<script>alert('收件人及电话不能为空，请检查后再提交');</script>");
            In_sjr.Focus();
            return false;
        }
        return true;
    }

    protected void SetValues(SqlDataReader dr)
    {
        In_yhbh.Text = dr["yhh"].ToString();
        In_fpdz.Text = dr["fpdz"].ToString();
        In_yhch.Text = dr["yhch"].ToString();
        In_yjdz.Text = dr["yjdz"].ToString();
        In_yjmc.Text = dr["yjmc"].ToString();
        In_sjr.Text = dr["sjr"].ToString();
        In_fphm.Text = dr["fpmc"].ToString();
    }

    //首记录
    protected void BSearch1_Click(object sender, EventArgs e)
    {
        BSearch1.Enabled = false;
        BSearch2.Enabled = false;
        BSearch3.Enabled = true;
        BSearch4.Enabled = true;
 
        SqlConnection conn = new SqlConnection(constr);
        conn.Open();
        Session["n"] = 1;
        string sql = "select * from [user].[dbo].[邮寄] where id = 1;";
        SqlCommand comm = new SqlCommand(sql, conn);
        SqlDataReader dr = comm.ExecuteReader();

        while (dr.Read())
        {
            SetValues(dr);
        }

        dr.Close();
        conn.Close();
    }

    //上一条记录
    protected void BSearch2_Click(object sender, EventArgs e)
    {
        if ((int)Session["n"] == 1)
        {
            Response.Write("<script>alert('已是第一条记录');</script>");
            BSearch1.Enabled = false;
            BSearch2.Enabled = false;         
        }
        else
        {
            Session["n"]= (int)Session["n"] - 1;
            BSearch1.Enabled = true;
            BSearch2.Enabled = true;
            BSearch3.Enabled = true;
            BSearch4.Enabled = true;
        }
        SqlConnection conn = new SqlConnection(constr);
        conn.Open();
           
        string sql = "select * from [user].[dbo].[邮寄] where id = " + (int)Session["n"] + ";";
        SqlCommand comm = new SqlCommand(sql, conn);
        SqlDataReader dr = comm.ExecuteReader();

        while (dr.Read())
        {
            SetValues(dr);
        }

        dr.Close();
        conn.Close();
    }

    //下一条记录
    protected void BSearch3_Click(object sender, EventArgs e)
    {
        if (Session["n"]  == Session["nmax"])
        {
            Response.Write("<script>alert('已是最后一条记录');</script>");
            BSearch3.Enabled = false;
            BSearch4.Enabled = false;

        }
        else
        {
            Session["n"] = (int)Session["n"] + 1;

            BSearch1.Enabled = true;
            BSearch2.Enabled = true;
            BSearch3.Enabled = true;
            BSearch4.Enabled = true;

            SqlConnection conn = new SqlConnection(constr);
            conn.Open();

            //Response.Write("<script>alert('" + (int)Session["n"] + "');</script>");

            string sql = "select * from [user].[dbo].[邮寄] where id = " + (int)Session["n"] + ";";
            SqlCommand comm = new SqlCommand(sql, conn);
            SqlDataReader dr = comm.ExecuteReader();

            while (dr.Read())
            {
                SetValues(dr);
            }

            dr.Close();
            conn.Close();
        }  

    }

    //末记录
    protected void BSearch4_Click(object sender, EventArgs e)
    {
        BSearch1.Enabled = true;
        BSearch2.Enabled = true;
        BSearch3.Enabled = false;
        BSearch4.Enabled = false;
        SqlConnection conn = new SqlConnection(constr);
        conn.Open();
        Session["n"] = (int)Session["nmax"];
        string sql = "select * from [user].[dbo].[邮寄] where id = " + (int)Session["n"] + ";";
        SqlCommand comm = new SqlCommand(sql, conn);
        SqlDataReader dr = comm.ExecuteReader();

        while (dr.Read())
        {
            SetValues(dr);
        }

        dr.Close();
        conn.Close();

    }

    //添加||保存
    protected void BSearch5_Click(object sender, EventArgs e)
    {
        if(BSearch5.Text=="添加")
        {
            BSearch5.Text = "保存";
            BSearch1.Enabled = false;
            BSearch2.Enabled = false;
            BSearch3.Enabled = false;
            BSearch4.Enabled = false;
            BSearch6.Enabled = false;
            BSearch7.Enabled = false;
            In_fpdz.Text = "";
            In_yhbh.Text = "";
            In_yhch.Text = "";
            In_yjdz.Text = "";
            In_yjmc.Text = "";
            In_sjr.Text = "";
            In_fphm.Text = "";
            In_fpdz.ReadOnly = false;
            In_yhbh.ReadOnly = false;
            In_yhch.ReadOnly = false;
            In_yjdz.ReadOnly = false;
            In_yjmc.ReadOnly = false;
            In_sjr.ReadOnly = false;
            In_fphm.ReadOnly = false;

        }
        else if(flag==true && check_null()==true)//确保没有相同的用户以及需要的项目都填写完成
        {

            //将数据插入数据库
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
            //先将该记录删除
            string sql = "delete from [user].[dbo].[邮寄] where yhh = @yhh";           
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.Parameters.Add("yhh", In_yhbh.Text);
            comm.ExecuteReader();
            conn.Close();

            //再将记录插入数据库中

            conn = new SqlConnection(constr);
            conn.Open();
            sql = "insert into [user].[dbo].[邮寄] (yhh,yhch,yjdz,yjmc,sjr,fpmc,fpdz) values(@yhh,@yhch,@yjdz,@yjmc,@sjr,@fpmc,@fpdz)";
            comm = new SqlCommand(sql, conn);
            comm.Parameters.Add("yhh", In_yhbh.Text);
            comm.Parameters.Add("yhch", In_yhch.Text);
            comm.Parameters.Add("yjdz", In_yjdz.Text);
            comm.Parameters.Add("yjmc", In_yjmc.Text);
            comm.Parameters.Add("sjr", In_sjr.Text);
            comm.Parameters.Add("fpmc", In_yjmc.Text);
            comm.Parameters.Add("fpdz", In_fpdz.Text);
              
            comm.ExecuteReader();
            conn.Close();

            BSearch5.Text = "添加";
            BSearch1.Enabled = false;
            BSearch2.Enabled = false;
            BSearch3.Enabled = false;
            BSearch4.Enabled = false;
            BSearch6.Enabled = false;
            BSearch7.Enabled = false;
            In_fpdz.ReadOnly = true;
            In_yhbh.ReadOnly = true;
            In_yhch.ReadOnly = true;
            In_yjdz.ReadOnly = true;
            In_yjmc.ReadOnly = true;
            In_sjr.ReadOnly = true;
            In_fphm.ReadOnly = true;

            //刷新最大值
            SetMax();

        }
        else if (flag == false)
        {
            Response.Write("<script>alert('已有该用户，请确认用户号是否正确');</script>");
        }
    }

    //删除
    protected void BSearch6_Click(object sender, EventArgs e)
    {
        In_yhbh.ReadOnly = true;
        In_fpdz.ReadOnly = false;
        In_yhch.ReadOnly = false;
        In_yjdz.ReadOnly = false;
        In_yjmc.ReadOnly = false;
        In_sjr.ReadOnly = false;
        In_fphm.ReadOnly = false;

        In_fpdz.Focus();

        BSearch1.Enabled = false;
        BSearch2.Enabled = false;
        BSearch3.Enabled = false;
        BSearch4.Enabled = false;
        BSearch6.Enabled = true;
        BSearch7.Enabled = false;
        BSearch8.Enabled = true;
        BSearch5.Text = "保存";
        
    }

    //修改
    protected void BSearch7_Click(object sender, EventArgs e)
    {
        //删除记录
        SqlConnection conn = new SqlConnection(constr);
        conn.Open();
        string sql = "delete from [user].[dbo].[邮寄] where yhh = " + In_yhbh.Text;
        SqlCommand comm = new SqlCommand(sql, conn);
        comm.ExecuteReader();
        conn.Close();

        BSearch1.Enabled = true;
        BSearch2.Enabled = true;
        BSearch3.Enabled = true;
        BSearch4.Enabled = true;
        BSearch5.Text = "添加";
        BSearch6.Enabled = false;
        BSearch7.Enabled = false;
        BSearch8.Enabled = false;
        //向上移动一条记录
        if ((int)Session["n"] != 1)
            Session["n"] = (int)Session["n"] - 1;

        SetMax();

    }

    //取消
    protected void BSearch8_Click(object sender, EventArgs e)
    {
        
        BSearch5.Text = "添加";
        BSearch1.Enabled = true;
        BSearch2.Enabled = true;
        BSearch3.Enabled = true;
        BSearch4.Enabled = true;
        BSearch6.Enabled = false;
        BSearch7.Enabled = false;
        In_fpdz.ReadOnly = true;
        In_yhbh.ReadOnly = true;
        In_yhch.ReadOnly = true;
        In_yjdz.ReadOnly = true;
        In_yjmc.ReadOnly = true;
        In_sjr.ReadOnly = true;
        In_fphm.ReadOnly = true;
    }



    protected void BSearch9_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(constr);
        conn.Open();
        string sql = "select * from [user].[dbo].[邮寄] where yhh = @yhh;";
        SqlCommand comm = new SqlCommand(sql, conn);
        comm.Parameters.Add("yhh", In_yhh.Text);
        In_yhbh.Text = In_yhh.Text;

        SqlDataReader dr = comm.ExecuteReader();
        while (dr.Read())
        {
            SetValues(dr);
        }
        dr.Close();
        conn.Close();

        In_fpdz.ReadOnly = true;
        In_yhbh.ReadOnly = true;
        In_yhch.ReadOnly = true;
        In_yjdz.ReadOnly = true;
        In_yjmc.ReadOnly = true;
        In_sjr.ReadOnly = true;
        In_fphm.ReadOnly = true;
    }
}