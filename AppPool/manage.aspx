<%@ Page Language="C#" AutoEventWireup="true" CodeFile="manage.aspx.cs" Inherits="manage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>邮寄档案维护</title>
    <meta content="IE=edge,chrome=1" http-equiv="X-UA-Compatible" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="description" content="" />
    <meta name="author" content="" />

    <link rel="stylesheet" type="text/css" href="lib/bootstrap/css/bootstrap.css" />

    <link rel="stylesheet" type="text/css" href="stylesheets/theme.css" />
    <link rel="stylesheet" href="lib/font-awesome/css/font-awesome.css" />

    <script src="lib/jquery-1.7.2.min.js" type="text/javascript"></script>

    <!-- Demo page code -->

    <style type="text/css">
        #line-chart {
            height: 300px;
            width: 800px;
            margin: 0px auto;
            margin-top: 1em;
        }

        .brand {
            font-family: georgia, serif;
        }

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
                <li><a href="history.aspx">查询历史清单</a></li>
                <li><a href="manage.aspx">邮寄档案维护</a></li>
                <li><a href="all.aspx">档案库浏览</a></li>
                <li><a href="logout.aspx">登出</a></li>





            </ul>


        </div>
        <div class="content">


            <div id="myTabContent" class="tab-content">
                <br />
                输入需要查询的用户号：<asp:TextBox ID="In_yhh" runat="server" CssClass="input-small"></asp:TextBox>
                    <asp:Button ID="BSearch9" runat="server"  Text="查询" CssClass="btn btn-primary" OnClick="BSearch9_Click" style="height: 30px"  />
                <div class="well">
                    <label>用户号：</label><asp:TextBox ID="In_yhbh" runat="server" CssClass="input-small" ontextchanged="check_yhh" AutoPostBack="true"></asp:TextBox>
                    <label>册号：</label><asp:TextBox ID="In_yhch" runat="server" CssClass="input-small">000000</asp:TextBox>
                    <label>发票户名：</label><asp:TextBox ID="In_fphm" runat="server" CssClass="input-xlarge"></asp:TextBox>
                    <label>发票地址：</label><asp:TextBox ID="In_fpdz" runat="server" CssClass="input-xlarge"></asp:TextBox>
                    <label>邮寄名称：</label><asp:TextBox ID="In_yjmc" runat="server" CssClass="input-xlarge"></asp:TextBox>
                    <label>邮寄地址：</label><asp:TextBox ID="In_yjdz" runat="server" CssClass="input-xlarge"></asp:TextBox>
                    <label>收件人及电话：</label><asp:TextBox ID="In_sjr" runat="server" CssClass="input-large"></asp:TextBox>
                    <br />
                    <asp:Button ID="BSearch1" runat="server"  Text="首记录" CssClass="btn btn-primary" OnClick="BSearch1_Click" />
                    <asp:Button ID="BSearch2" runat="server"  Text="上一条" CssClass="btn btn-primary" Enabled="False" OnClick="BSearch2_Click" />
                    <asp:Button ID="BSearch3" runat="server"  Text="下一条" CssClass="btn btn-primary" OnClick="BSearch3_Click" />
                    <asp:Button ID="BSearch4" runat="server"  Text="末记录" CssClass="btn btn-primary" OnClick="BSearch4_Click" />
                    <asp:Button ID="BSearch5" runat="server"  Text="添加" CssClass="btn btn-primary" OnClick="BSearch5_Click" />
                    <asp:Button ID="BSearch6" runat="server"  Text="修改" CssClass="btn btn-primary" OnClick="BSearch6_Click" />
                    <asp:Button ID="BSearch7" runat="server"  Text="删除" CssClass="btn btn-primary" OnClientClick="return confirm('您确认要删除该条记录吗？')"  OnClick="BSearch7_Click" />
                    <asp:Button ID="BSearch8" runat="server"  Text="取消" CssClass="btn btn-primary" OnClick="BSearch8_Click" />
                    <br />
                    
                </div>
            </div>

        </div>

    </form>

</body>
</html>
