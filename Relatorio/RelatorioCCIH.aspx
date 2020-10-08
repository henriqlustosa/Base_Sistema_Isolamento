<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="RelatorioCCIH.aspx.cs" Inherits="CCIH_RelatorioCCIH" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../js/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="../js/maskedinput-1.2.2.js" type="text/javascript"></script>

    <script type="text/javascript">
        




    </script>

    <style type="text/css">
        .style7
        {
            width: 155px;
        }
        .style8
        {
            width: 108px;
        }
        .style9
        {
            width: 127px;
        }
        .style10
        {
            width: 83px;
        }
        .style12
        {
            height: 59px;
        }
        .style13
        {
            width: 108px;
            height: 59px;
        }
        .style14
        {
            width: 83px;
            height: 59px;
        }
        .style15
        {
            width: 127px;
            height: 59px;
        }
        .style16
        {
            width: 155px;
            height: 59px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1 align="center" style="width: 877px">
        Relatório de Pacientes Completo
    </h1>
    <table align="center">
        <tr>
            <td class="style8">
                MÊS INICIAL:
            </td>
            <td class="style10">
                <asp:DropDownList ID="ddlMesInicial" runat="server">
                    <asp:ListItem Value="1">Janeiro</asp:ListItem>
                    <asp:ListItem Value="2">Fevereiro</asp:ListItem>
                    <asp:ListItem Value="3">Março</asp:ListItem>
                    <asp:ListItem Value="4">Abril</asp:ListItem>
                    <asp:ListItem Value="5">Maio</asp:ListItem>
                    <asp:ListItem Value="6">Junho</asp:ListItem>
                    <asp:ListItem Value="7">Julho</asp:ListItem>
                    <asp:ListItem Value="8">Agosto</asp:ListItem>
                    <asp:ListItem Value="9">Setembro</asp:ListItem>
                    <asp:ListItem Value="10">Outubro</asp:ListItem>
                    <asp:ListItem Value="11">Novembro</asp:ListItem>
                    <asp:ListItem Value="12">Dezembro</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style9">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ANO INICIAL:
            </td>
            <td class="style7">
                <asp:DropDownList ID="ddlAnoInicial" runat="server" DataSourceID="SqlDataSource3"
                    DataTextField="Ano" DataValueField="Ano">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringIsolamento %>"
                    ProviderName="<%$ ConnectionStrings:ConnectionStringIsolamento.ProviderName %>"
                    SelectCommand="SELECT DISTINCT YEAR([dt_resultado]) AS Ano FROM [Isolamento].[dbo].[Exame] ORDER BY Ano DESC">
                </asp:SqlDataSource>
            </td>
        </tr>
        <td class="style13">
            MÊS FINAL:
        </td>
        <td class="style14">
            <asp:DropDownList ID="ddlMesFinal" runat="server">
                <asp:ListItem Value="1">Janeiro</asp:ListItem>
                <asp:ListItem Value="2">Fevereiro</asp:ListItem>
                <asp:ListItem Value="3">Março</asp:ListItem>
                <asp:ListItem Value="4">Abril</asp:ListItem>
                <asp:ListItem Value="5">Maio</asp:ListItem>
                <asp:ListItem Value="6">Junho</asp:ListItem>
                <asp:ListItem Value="7">Julho</asp:ListItem>
                <asp:ListItem Value="8">Agosto</asp:ListItem>
                <asp:ListItem Value="9">Setembro</asp:ListItem>
                <asp:ListItem Value="10">Outubro</asp:ListItem>
                <asp:ListItem Value="11">Novembro</asp:ListItem>
                <asp:ListItem Value="12">Dezembro</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td class="style15">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ANO FINAL:
        </td>
        <td class="style16">
            <asp:DropDownList ID="ddlAnoFinal" runat="server" DataSourceID="SqlDataSource3" DataTextField="Ano"
                DataValueField="Ano">
            </asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringIsolamento %>"
                ProviderName="<%$ ConnectionStrings:ConnectionStringIsolamento.ProviderName %>"
                SelectCommand="SELECT DISTINCT YEAR([dt_resultado]) AS Ano FROM [Isolamento].[dbo].[Exame] ORDER BY Ano DESC">
            </asp:SqlDataSource>
        </td>
        <tr>
            <td align="center" colspan="4">
                <asp:Button ID="btnExportar" runat="server" Text="Exportar para Excel" OnClick="btnExportar_Click" />
            </td>
        </tr>
    </table>
    <asp:GridView ID="GridInternado" runat="server" CellPadding="4" ForeColor="#333333"
        GridLines="Both">
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#FF66CDAA" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#999999" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    </asp:GridView>
</asp:Content>
