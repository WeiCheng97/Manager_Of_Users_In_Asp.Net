<%@ Page Language="C#" AutoEventWireup="true" CodeFile="history.aspx.cs" Inherits="history" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="utf-8"/>
    <title>查询历史清单</title>
    <meta content="IE=edge,chrome=1" http-equiv="X-UA-Compatible"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <meta name="description" content=""/>
    <meta name="author" content=""/>

    <link rel="stylesheet" type="text/css" href="lib/bootstrap/css/bootstrap.css"/>
    
    <link rel="stylesheet" type="text/css" href="stylesheets/theme.css"/>
    <link rel="stylesheet" href="lib/font-awesome/css/font-awesome.css"/>

    <script src="lib/jquery-1.7.2.min.js" type="text/javascript"></script>
    <!-- 日期选择 -->
    <script language="javascript" type="text/javascript" src="datepicker/WdatePicker.js"></script>
    <!-- Demo page code -->

    <style type="text/css">
        #line-chart {
            height:300px;
            width:800px;
            margin: 0px auto;
            margin-top: 1em;
        }
        .brand { font-family: georgia, serif; }
        .brand .first {
            color: #ccc;
            font-style: italic;
        }
        .brand .second {
            color: #fff;
            font-weight: bold;
        }
    </style>

    <!-- Le HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
    <![endif]-->
</head>
<body>
    <form id="form1" runat="server">
    <div class="navbar">
        <div class="navbar-inner">
                <a class="brand" href="index.html"><span class="first">发票打印系统</span></a>
        </div>
    </div>
    <div class="sidebar-nav">
        <a href="#dashboard-menu" class="nav-header" data-toggle="collapse"><i class="icon-dashboard"></i>功能选择</a>
        <ul id="dashboard-menu" class="nav nav-list collapse in">
            <li><a href="index.aspx">生成当月清单</a></li>
            <li ><a href="history.aspx">查询历史清单</a></li>
            <li ><a href="manage.aspx">邮寄档案维护</a></li>
            <li ><a href="all.aspx">档案库浏览</a></li>  
            <li ><a href="logout.aspx">登出</a></li>
        </ul>

    </div>
    <div class="content">
        <div class="alert alert-info">
        <strong>请输入日期区间</strong> 
        </div>
        <asp:TextBox ID="In_Date1" runat="server" CssClass="input-xlarge" Width="100px" OnClick="WdatePicker()"></asp:TextBox>
        ——<asp:TextBox ID="In_Date2" runat="server" CssClass="input-xlarge" Width="100px" OnClick="WdatePicker()"></asp:TextBox>
        <asp:Button ID="BSearch" runat="server"  Text="检索" CssClass="btn btn-primary" OnClientClick="Search"/>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
            <Columns> 
                <asp:BoundField DataField="yhh" HeaderText="用户代号" SortExpression="yhh" />
                <asp:BoundField DataField="fpmc" HeaderText="发票名称" SortExpression="fpmc" />
                <asp:BoundField DataField="fpdz" HeaderText="发票地址" SortExpression="fpdz" />
                <asp:BoundField DataField="yjmc" HeaderText="邮寄名称" SortExpression="yjmc" />
                <asp:BoundField DataField="yjdz" HeaderText="邮寄地址" SortExpression="yjdz" />
                <asp:BoundField DataField="sjr" HeaderText="收件人" SortExpression="sjr" />
                <asp:BoundField DataField="rqq" HeaderText="生成日期" SortExpression="rq" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:userConnectionString %>" SelectCommand="SELECT [bh], [yhh], [fpdz], convert(char(10),rq,23) as rqq, [yjmc], [yjdz], [fpmc], [sjr] FROM [fp] WHERE (([rq] &gt;= @rq) AND ([rq] &lt;= @rq2))">
            <SelectParameters>
                <asp:ControlParameter ControlID="In_Date1" DbType="Date" Name="rq" PropertyName="Text" />
                <asp:ControlParameter ControlID="In_Date2" DbType="Date" Name="rq2" PropertyName="Text" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>


    </form>


</body>
</html>
