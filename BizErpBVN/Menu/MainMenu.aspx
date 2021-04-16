<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainMenu.aspx.cs" Inherits="BizErpBVN.Menu.MainMenu" %>

<%@ Register Src="~/Menu/Tab1.ascx" TagPrefix="uc1" TagName="Tab1" %>
<%@ Register Src="~/Menu/Tab2.ascx" TagPrefix="uc1" TagName="Tab2" %>
<%@ Register Src="~/Menu/Tab3.ascx" TagPrefix="uc1" TagName="Tab3" %>
<%@ Register Src="~/Menu/Tab4.ascx" TagPrefix="uc1" TagName="Tab4" %>
<%@ Register Src="~/Menu/Tab5.ascx" TagPrefix="uc1" TagName="Tab5" %>

<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.0.0/css/bootstrap.min.css">
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.0.0/js/bootstrap.min.js"></script>
<script src="//code.jquery.com/jquery-1.11.1.min.js"></script>
<script src="http://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.5.4/bootstrap-select.js"></script>
<script src="//netdna.bootstrapcdn.com/bootstrap/3.0.0/js/bootstrap.min.js"></script>



<!DOCTYPE html>

<head>
    <title></title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <style type="text/css">
        body {
            margin: 0;
            padding-top: unset;
            font-family: Arial, Helvetica, sans-serif;
        }

        .topnav {
            overflow: hidden;
            background: linear-gradient(to bottom, #f4f18b, #f4e416); /* W3C, IE 10+/ Edge, Firefox 16+, Chrome 26+, Opera 12+, Safari 7+ */
        }

            .topnav a {
                float: left;
                color: dimgrey;
                text-align: center;
                padding: 14px 16px;
                text-decoration: none;
                font-size: 20px;
                font-weight: bold;
            }

        body, html {
            height: 100%;
        }

        body {
            margin-top: 20px;
        }

        .fa-fw {
            width: 2em;
        }

        nav.sidebar, .main {
            -webkit-transition: margin 200ms ease-out;
            -moz-transition: margin 200ms ease-out;
            -o-transition: margin 200ms ease-out;
            transition: margin 200ms ease-out;
        }

        .main {
            padding: 10px 10px 0 10px;
        }

        @media (min-width: 765px) {

            .main {
                position: absolute;
                width: calc(100% - 40px);
                margin-left: 40px;
                float: right;
            }

            nav.sidebar:hover + .main {
                margin-left: 200px;
            }

            nav.sidebar.navbar.sidebar > .container .navbar-brand, .navbar > .container-fluid .navbar-brand {
                margin-left: 0px;
            }

            nav.sidebar .navbar-brand, nav.sidebar .navbar-header {
                text-align: center;
                width: 100%;
                margin-left: 0px;
            }

            nav.sidebar a {
                padding-right: 13px;
            }

            nav.sidebar .navbar-nav > li:first-child {
                border-top: 1px #e5e5e5 solid;
            }

            nav.sidebar .navbar-nav > li {
                border-bottom: 1px #e5e5e5 solid;
            }

            nav.sidebar .navbar-nav .open .dropdown-menu {
                position: static;
                float: none;
                width: auto;
                margin-top: 0;
                background-color: transparent;
                border: 0;
                -webkit-box-shadow: none;
                box-shadow: none;
            }

            nav.sidebar .navbar-collapse, nav.sidebar .container-fluid {
                padding: 0 0px 0 0px;
            }

            .navbar-inverse .navbar-nav .open .dropdown-menu > li > a {
                color: #777;
            }

            nav.sidebar {
                width: 200px;
                height: 100%;
                margin-left: -160px;
                float: left;
                margin-bottom: 0px;
            }

                nav.sidebar li {
                    width: 100%;
                }

                nav.sidebar:hover {
                    margin-left: 0px;
                }

            .forAnimate {
                opacity: 0;
            }
        }

        @media (min-width: 1330px) {

            .main {
                width: calc(100% - 200px);
                margin-left: 200px;
            }

            nav.sidebar {
                margin-left: 0px;
                float: left;
            }

                nav.sidebar .forAnimate {
                    opacity: 1;
                }
        }

        nav.sidebar .navbar-nav .open .dropdown-menu > li > a:hover, nav.sidebar .navbar-nav .open .dropdown-menu > li > a:focus {
            color: #CCC;
            background-color: transparent;
        }

        nav:hover .forAnimate {
            opacity: 1;
        }

        section {
            padding-left: 15px;
        }

        .spinner input {
            text-align: right;
        }

        .input-group-btn-vertical {
            position: relative;
            white-space: nowrap;
            width: 2%;
            vertical-align: middle;
            display: table-cell;
        }

            .input-group-btn-vertical > .btn {
                display: block;
                float: none;
                width: 100%;
                max-width: 100%;
                padding: 8px;
                margin-left: -1px;
                position: relative;
                border-radius: 0;
            }

                .input-group-btn-vertical > .btn:first-child {
                    border-top-right-radius: 4px;
                }

                .input-group-btn-vertical > .btn:last-child {
                    margin-top: -2px;
                    border-bottom-right-radius: 4px;
                }

            .input-group-btn-vertical i {
                position: absolute;
                top: 0;
                left: 4px;
            }

        .btn-label {
            position: relative;
            left: -12px;
            display: inline-block;
            padding: 6px 12px 15px 12px;
            background: rgba(0,0,0,0.15);
            border-radius: 3px 0 0 3px;
        }

        .btn-labeled {
            padding-top: 0;
            padding-bottom: 0;
        }

        .btn {
            margin-bottom: 10px;
        }
    </style>

    <script type="text/javascript">


        window.onload = function () {
            $('.selectpicker').selectpicker();
            $('.rm-mustard').click(function () {
                $('.remove-example').find('[value=Mustard]').remove();
                $('.remove-example').selectpicker('refresh');
            });
            $('.rm-ketchup').click(function () {
                $('.remove-example').find('[value=Ketchup]').remove();
                $('.remove-example').selectpicker('refresh');
            });
            $('.rm-relish').click(function () {
                $('.remove-example').find('[value=Relish]').remove();
                $('.remove-example').selectpicker('refresh');
            });
            $('.ex-disable').click(function () {
                $('.disable-example').prop('disabled', true);
                $('.disable-example').selectpicker('refresh');
            });
            $('.ex-enable').click(function () {
                $('.disable-example').prop('disabled', false);
                $('.disable-example').selectpicker('refresh');
            });

            // scrollYou
            $('.scrollMe .dropdown-menu').scrollyou();

            prettyPrint();
        };

        $(function () {

            $('.spinner .btn:first-of-type').on('click', function () {
                var btn = $(this);
                var input = btn.closest('.spinner').find('input');
                if (input.attr('max') == undefined || parseInt(input.val()) < parseInt(input.attr('max'))) {
                    input.val(parseInt(input.val(), 10) + 1);
                } else {
                    btn.next("disabled", true);
                }
            });
            $('.spinner .btn:last-of-type').on('click', function () {
                var btn = $(this);
                var input = btn.closest('.spinner').find('input');
                if (input.attr('min') == undefined || parseInt(input.val()) > parseInt(input.attr('min'))) {
                    input.val(parseInt(input.val(), 10) - 1);
                } else {
                    btn.prev("disabled", true);
                }
            });

        })


    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="topnav">
            <ul class="nav navbar-nav navbar-left">
                <li><a style="padding-bottom: 10px; padding-top: 20px">ERP</a></li>
            </ul>
            <ul class="nav navbar-nav navbar-right">
                <li><a href="../Default.aspx" style="font-size: 14px;">ออกจากระบบ &nbsp;<span style="font-size: 20px;" class="glyphicon glyphicon-log-out" aria-hidden="true"></span></a></li>
            </ul>
        </div>
        <nav class="navbar navbar-default sidebar" role="navigation" style="height: 100%">
            <div class="container-fluid">
                <div class="collapse navbar-collapse" id="bs-sidebar-navbar-collapse-1">
                    <ul class="nav navbar-nav">
                        <li><a class="nav-link" data-toggle="tab" href="#home" style="font-size: 16px; font-weight: bold">ลูกค้า<span style="font-size: 16px; font-weight: bold" class="pull-right hidden-xs showopacity glyphicon glyphicon-user"></span></a></li>
                        <li><a class="nav-link" data-toggle="tab" href="#menu1" style="font-size: 16px; font-weight: bold">สั่งขายรั้ว<span style="font-size: 16px;" class="pull-right hidden-xs showopacity glyphicon glyphicon-tags"></span></a></li>
                        <li><a class="nav-link" data-toggle="tab" href="#menu2" style="font-size: 16px; font-weight: bold">ยืนยันการชำระเงิน<span style="font-size: 16px;" class="pull-right hidden-xs showopacity glyphicon glyphicon-usd"></span></a></li>
                        <li><a class="nav-link" data-toggle="tab" href="#menu3"  style="font-size: 16px; font-weight: bold">อนุมัติการสั่งขายรั้ว <span style="font-size: 16px;" class="pull-right hidden-xs showopacity glyphicon glyphicon-check"></span></a></li>
                        <li><a class="nav-link" data-toggle="tab" href="#menu4" style="font-size: 16px; font-weight: bold; display:none">บันทึกการขนส่ง <span style="font-size: 16px;" class="pull-right hidden-xs showopacity glyphicon glyphicon-level-up"></span></a></li>
                    </ul>
                </div>
            </div>
        </nav>
        <div class="tab-content">
            <div id="home" class="container tab-pane active">
                <uc1:Tab1 runat="server" ID="Tab1" />
            </div>
            <div id="menu1" class="container tab-pane fade">
                <br>
                <uc1:Tab2 runat="server" ID="Tab2" />
            </div>
            <div id="menu2" class="container tab-pane fade">
                <br>
              <uc1:Tab3 runat="server" id="Tab3" />
            </div>
            <div id="menu3" class="container tab-pane fade">
                <br>
            <uc1:Tab4 runat="server" ID="Tab4" />
            </div>
            <div id="menu4" class="container tab-pane fade">
                <br>
                <uc1:Tab5 runat="server" ID="Tab5" />
            </div>
            
        </div>
    </form>
</body>
