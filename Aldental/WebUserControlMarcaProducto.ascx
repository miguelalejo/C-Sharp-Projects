﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlMarcaProducto.ascx.cs" Inherits="Aldental.WebUserControlMarcaProducto" %>
<%@ Register assembly="DevExpress.Web.v9.1, Version=9.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxNavBar" tagprefix="dxnb" %>
<%@ Register assembly="DevExpress.Web.v9.1, Version=9.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxMenu" tagprefix="dxm" %>

<style type="text/css">

    .style28
    {
        width: 574px;
        height: 52px;
    }
    
    * {
	margin: 0px 0px 1px 0px;
	padding: 0px;
	text-align: center;
}

    .style82
    {
        height: 26px;
    }
    .style80
    {
        width: 10px;
        height: 117px;
    }
    .style81
    {
        height: 117px;
    }
    .style61
    {
        width: 5px;
        height: 52px;
    }
    .style27
    {
        width: 186px;
        height: 52px;
    }
    .style29
    {
        width: 54px;
        height: 52px;
    }
    .style30
    {
        width: 116px;
        height: 52px;
    }
    .style62
    {
        width: 110px;
        height: 52px;
    }
    .style57
    {
        width: 100px;
        height: 52px;
    }
    .style31
    {
        height: 52px;
    }
    
    .style73
    {
        width: 10px;
        height: 114px;
    }
    .style32
    {
        height: 114px;
    }
    .style84
    {
        width: 28px;
        height: 13px;
    }
    .style85
    {
        width: 93px;
        height: 13px;
    }
    .style92
    {
        width: 101px;
        font-size: small;
        font-weight: bold;
        height: 13px;
        text-align: right;
    }
    .style87
    {
        width: 98px;
        height: 13px;
    }
    .style76
    {
        width: 10px;
        height: 84px;
    }
    .style77
    {
        height: 84px;
    }
    .style42
    {
        height: 93px;
        width: 10px;
    }
    .style40
    {
        height: 93px;
        width: 96px;
    }
    .dxnbControl_BlackGlass 
{
	font: 9pt Tahoma;
	color: black;
	background-color: white;
	border-right: solid 1px Black;
	border-bottom: solid 1px Black;
}
.dxnbGroupHeader_BlackGlass, .dxnbGroupHeaderCollapsed_BlackGlass
{
	text-align: left;
}
.dxnbGroupHeader_BlackGlass
{
	font: 9pt Tahoma;
	color: White;
	background: #444444 url('App_Themes/BlackGlass/Web/nbItemBack.gif') repeat-x left top;
	border-left: solid 1px Black;
	border-top: solid 1px Black;
	padding: 4px 6px 5px 10px;
}
.dxnbGroupHeader_BlackGlass table.dxnb
{
	font: 9pt Tahoma;
	color: black;
}
.dxnbGroupHeader_BlackGlass td.dxnb
{
	color: White;
	white-space: nowrap;
}

.dxnbGroupContent_BlackGlass
{
	font: 9pt Tahoma;
	color: #4F4F51;
	background-color: #a4bed1;
	border-left: solid 1px Black;
	border-top: solid 1px Black;
}
.dxnbItem_BlackGlass, .dxnbItemHover_BlackGlass, .dxnbItemSelected_BlackGlass,
.dxnbBulletItem_BlackGlass, .dxnbBulletItemHover_BlackGlass, .dxnbBulletItemSelected_BlackGlass
{
    text-align: left;
}
.dxnbItem_BlackGlass
{
	font: 9pt Tahoma;
	padding-top: 6px;
	padding-right: 5px;
	padding-bottom: 7px;
	padding-left: 10px;
}
.dxnbItem_BlackGlass, .dxnbLargeItem_BlackGlass, .dxnbBulletItem_BlackGlass
{
	color: #4F4F51;
	background-color: #E8EDF1;
}
.dxnbControl_BlackGlass a
{
	color: Black;
}
    .style46
    {
        height: 93px;
    }
    .style44
    {
        height: 93px;
        width: 86px;
    }
    .style59
    {
        font-size: small;
    }
    .style65
    {
        width: 10px;
        height: 14px;
    }
    .style66
    {
        width: 96px;
        height: 14px;
    }
    .style67
    {
        font-size: x-small;
        font-weight: bold;
        width: 83px;
        height: 14px;
    }
    .style68
    {
        font-size: x-small;
        width: 91px;
        font-weight: bold;
        height: 14px;
    }
    .style69
    {
        font-size: x-small;
        font-weight: bold;
        width: 100px;
        height: 14px;
    }
    .style70
    {
        font-size: x-small;
        font-weight: bold;
        width: 88px;
        height: 14px;
    }
    .style71
    {
        font-size: x-small;
        width: 86px;
        font-weight: bold;
        height: 14px;
    }
    .style72
    {
        height: 14px;
    }

    .style43
    {
        width: 10px;
    }
    .style41
    {
        width: 96px;
    }
    
    .style33
    {
        font-size: x-small;
        font-weight: bold;
    }
    .style55
    {
        font-size: x-small;
        width: 91px;
        font-weight: bold;
    }
    .style45
    {
        font-size: x-small;
        width: 86px;
        font-weight: bold;
    }
    
body {
	padding: 80px 0px 0px;
	background:  #003366 repeat-x;
	color: #695d47;
	font-family: "Times New Roman", Times, serif;
	font-size: small;
	text-align: center;
        font-weight: 700;
    }

    .style93
    {
        height: 117px;
        width: 41px;
    }
    .style94
    {
        height: 114px;
        width: 41px;
    }
    .style95
    {
        height: 84px;
        width: 41px;
    }

    .style96
    {
        font-size: large;
        color: #FFFFFF;
    }

    </style>
    </style>

<script type="text/javascript" language="javascript">
    
    //esta funcion solo deja ingresar numeros enteros
   
function validar(e) 
{
    tecla = (document.all) ? e.keyCode : e.which;
    if (tecla==8) return true; //Tecla de retroceso (para poder borrar)
    if (tecla>47 && tecla <58)
            	{
                	return String.fromCharCode(tecla);
            	}
            	return false;
} 

</script>
    <table class="style28" style="background-color: #FFFFFF" 
    bgcolor="AliceBlue">
        <tr>
            
        </tr>
        <tr>
            
        </tr>
        <tr>
            <td style="height: 21px; background-color: #003366;" bgcolor="#003366" 
                colspan="4" class="style96">
                MARCA PRODUCTOS</td>
        </tr>
        <tr>
            <td align="center" colspan="4">
            </td>
        </tr>
        <tr>
            <td align="center" class="style80">
            </td>
            <td align="center" class="style81">
                <table style="width: 100%; height: 129px;">
                    <tr>
                        <td class="style61">
                            &nbsp;</td>
                        <td class="style27">
                        </td>
                        <td class="style28">
                <asp:Image ID="Image1" runat="server" Height="124px" 
                    ImageUrl="~/images/con2.jpeg" Width="175px" />
                        </td>
                        <td class="style29">
                        </td>
                        <td class="style30">
                            <asp:Image ID="Image2" 
                    runat="server" Height="119px" 
                    ImageUrl="~/images/con3.jpeg" Width="163px" />
                        </td>
                        <td class="style62">
                        </td>
                        <td class="style57">
                            &nbsp;</td>
                        <td class="style31">
                        </td>
                    </tr>
                    </table>
            </td>
            <td align="center" class="style81">
                &nbsp;</td>
            <td align="center" class="style93">
            </td>
        </tr>
        <tr>
            <td align="center" class="style73">
            </td>
            <td align="center" class="style32">
                <table style="width:101%; height: 140px;">
                    <tr>
                        <td class="style84">
                            </td>
                        <td class="style85">
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                            </td>
                        <td class="style92">
                            Código: </td>
                        <td class="style87">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="TextBoxCodMarca" runat="server" 
    Height="20px" onkeypress="return validar(event)"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="style87">
                        </td>
                    </tr>
                    <tr>
                        <td class="style84">
                            </td>
                        <td class="style85">
                            <dxnb:ASPxNavBar ID="ASPxNavBar4" runat="server" 
                                CssFilePath="~/App_Themes/BlackGlass/{0}/styles.css" CssPostfix="BlackGlass" 
                                GroupSpacing="0px" Height="16px" ImageFolder="~/App_Themes/BlackGlass/{0}/" 
                                Width="69px" onitemclick="ASPxNavBar4_ItemClick">
                                <groups>
                                    <dxnb:NavBarGroup Text="REPORTE">
                                        <itemimage height="20px" url="~/images/con3.jpeg" width="20px" />
                                        <Items>
                                            <dxnb:NavBarItem NavigateUrl="~/WebForm_BuscarMarcaProducto.aspx" 
                                                Text="Marca Productos">
                                            </dxnb:NavBarItem>
                                        </Items>
                                    </dxnb:NavBarGroup>
                                </groups>
                                <GroupContentStyle ItemSpacing="1px">
                                </GroupContentStyle>
                                <CollapseImage Height="17px" Url="~/App_Themes/BlackGlass/Web/nbCollapse.gif" 
                                    Width="18px" />
                                <ExpandImage Height="17px" Url="~/App_Themes/BlackGlass/Web/nbExpand.gif" 
                                    Width="18px" />
                            </dxnb:ASPxNavBar>
                            </td>
                        <td class="style92">
                            Nombre:</td>
                        <td class="style87">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="TextBoxNomMarca" runat="server" Height="20px"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="style87">
                        </td>
                    </tr>
                    </table>
            </td>
            <td align="center" class="style32">
                &nbsp;</td>
            <td align="center" class="style94">
            </td>
        </tr>
        <tr>
            <td align="center" class="style76">
                </td>
            <td align="center" class="style77">
                <table style="width: 99%; height: 115px;">
                <tr>
                    <td style="text-align: center; " class="style42">
                        </td>
                    <td style="text-align: center; " class="style40">
                            &nbsp;</td>
                    <td style="text-align: center; " class="style46" colspan="4">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <dxm:ASPxMenu ID="ASPxMenuOperaciones1" runat="server" 
    AccessibilityCompliant="True" AutoSeparators="RootOnly" 
    CssFilePath="~/App_Themes/BlackGlass/{0}/styles.css" CssPostfix="BlackGlass" 
    ImageFolder="~/App_Themes/BlackGlass/{0}/" ItemSpacing="0px" 
    onitemclick="ASPxMenuOperaciones1_ItemClick" SeparatorColor="#16171B" 
    SeparatorHeight="100%" SeparatorWidth="1px" ShowPopOutImages="True">
                                    <rootitemsubmenuoffset firstitemx="-1" lastitemx="-1" 
        x="-1" />
                                    <Items>
                                        <dxm:MenuItem Text="Nuevo" Name="Nuevo">
                                            <image alternatetext="Nuevo" height="20px" url="~/images/add-32.png" 
                width="20px" />
                                        </dxm:MenuItem>
                                        <dxm:MenuItem Text="Guardar" ClientEnabled="False" Name="Guardar">
                                            <image height="20px" url="~/images/Saveas.JPG" width="20px" />
                                        </dxm:MenuItem>
                                        <dxm:MenuItem Text="Modificar" ClientEnabled="False" Name="Modificar">
                                            <image height="20px" url="~/images/modificar.bmp" width="20px" />
                                        </dxm:MenuItem>
                                        <dxm:MenuItem Text="Eliminar" ClientEnabled="False" Name="Eliminar">
                                            <image height="20px" url="~/images/delete.ico" width="20px" />
                                        </dxm:MenuItem>
                                        <dxm:MenuItem Text="Buscar" Name="Buscar">
                                            <image height="20px" url="~/images/buscar-32.png" width="20px" />
                                        </dxm:MenuItem>
                                    </Items>
                                    <submenustyle gutterwidth="0px"></submenustyle>
                                    <submenuitemstyle wrap="False">
                                    </submenuitemstyle>
                                    <border bordercolor="#16171B" borderstyle="Solid" 
        borderwidth="1px"></border>
                                    <horizontalpopoutimage height="7px" 
        url="~/App_Themes/BlackGlass/Web/mHorizontalPopOut.png" width="7px" />
                                </dxm:ASPxMenu>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td style="text-align: center; " class="style44">
                        <br />
                    </td>
                    <td class="style46">
                        <b>CODIGO</b><br class="style59" />
                        <asp:TextBox ID="TextBoxBuscar" runat="server" Width="111px" onkeypress="return validar(event)"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; background-color: #FFFFFF;" class="style65">
                        </td>
                    <td style="text-align: center" class="style66">
                        </td>
                    <td style="text-align: center; " class="style67">
                        &nbsp;</td>
                    <td style="text-align: center; " class="style68">
                        &nbsp;</td>
                    <td style="text-align: center" class="style69">
                        &nbsp;</td>
                    <td style="text-align: center; " class="style70">
                        &nbsp;</td>
                    <td style="text-align: center; " class="style71">
                        &nbsp;</td>
                    <td class="style72">
                        </td>
                </tr>
                <tr>
                    <td style="text-align: center; background-color: #FFFFFF;" class="style43">
                        &nbsp;</td>
                    <td style="text-align: center" class="style41">
                        &nbsp;</td>
                    <td style="text-align: center; width: 83px;" class="style33">
                        &nbsp;</td>
                    <td style="text-align: center; " class="style55">
                        &nbsp;</td>
                    <td style="width: 100px; text-align: center" class="style33">
                        &nbsp;</td>
                    <td style="text-align: center; width: 88px;" class="style33">
                        &nbsp;</td>
                    <td style="text-align: center; " class="style45">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                </table>
            </td>
            <td align="center" class="style77">
                &nbsp;</td>
            <td align="center" class="style95">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
        </tr>
        </table>
 
