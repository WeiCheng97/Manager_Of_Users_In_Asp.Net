<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login_in.aspx.cs" Inherits="login_in" %>

<!DOCTYPE html>
<html>

<head runat="server">
    <meta charset="utf-8">
    <title>登录</title>
    <meta content="IE=edge,chrome=1" http-equiv="X-UA-Compatible">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="">

    <link rel="stylesheet" type="text/css" href="lib/bootstrap/css/bootstrap.css">

    <link rel="stylesheet" type="text/css" href="stylesheets/theme.css">
    <link rel="stylesheet" href="lib/font-awesome/css/font-awesome.css">

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

  
</head>



<body>

    <div class="row-fluid">
        <div class="row-fluid">
            <div class="dialog">
                <div class="block">
                    <p class="block-heading">Sign In</p>
                    <form id="form1" runat="server">
                        <div class="span12">
                        <label>
                            <br />
                            用户名：</label>                
                        <asp:TextBox ID="TextBox1" runat="server" class="span12" Width="380px"></asp:TextBox>
                        <br />
                        <label>密  码：</label>                
                        <asp:TextBox ID="TextBox2" runat="server" class="span12" Width="380px"></asp:TextBox>
                            <br />
                        <asp:Button ID="Button1" runat="server" Text="登录"  class="btn btn-primary pull-right" OnClick="Button1_Click" />
                        <br/>
                        <div class="clearfix"></div>
                       
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>

    <asp:Label ID="lblMessage" runat="server"></asp:Label>
  
</body>
</html>
