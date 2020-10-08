<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="GraficoRelatorio.aspx.cs" Inherits="Relatorio_GraficoRelatorio" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
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
        .style17
        {
            width: 554px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1 align="center" style="width: 877px">
        Gráfico dos Pacientes
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
        <tr>
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
        </tr>
        <tr>
            <td align="center" colspan="4">
                <asp:Button ID="btnExportar" runat="server" Text="Carregar Gráficos" OnClick="btnCarregar_Click" />
            </td>
        </tr>
    </table>

    <table>
        <asp:Chart ID="ChartTaxaDeObito" ImageStorageMode="UseImageLocation" ImageLocation="~/chart_images/#SEQ(100,10)" runat="server" Visible="False" Width="452px" Height="232px"
            Style="margin-right: 164px">
            <Legends>
                <asp:Legend Name="Legend1">
                </asp:Legend>
            </Legends>
            <Titles>
                <asp:Title Font="Microsoft Sans Serif, 15pt" Name="Title1" Text="Taxa de Óbito">
                </asp:Title>
            </Titles>
            <Series>
                <asp:Series Name="Series1" ChartArea="ChartArea1" ChartType="Pie" Legend="Legend1">
                </asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1">
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
    </table>
    <table>
        <asp:Chart ID="ChartSexo" ImageStorageMode="UseImageLocation" ImageLocation="~/chart_images/#SEQ(100,10)" runat="server" Width="452px" Visible="False" 
            Height="232px">
            <Legends>
                <asp:Legend Name="Legend1">
                </asp:Legend>
            </Legends>
            <Titles>
                <asp:Title Font="Microsoft Sans Serif, 15pt" Name="Title1" Text="Taxa Relacionada ao Sexo">
                </asp:Title>
            </Titles>
            <Series>
                <asp:Series Name="Series1" ChartType="Pie" Legend="Legend1" ChartArea="ChartArea1">
                </asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1">
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
    </table>
    <table>
        <asp:Chart ID="ChartSexoObito" ImageStorageMode="UseImageLocation" ImageLocation="~/chart_images/#SEQ(100,10)" runat="server" Visible="False" Width="564px">
            <Legends>
                <asp:Legend Name="Legend1">
                </asp:Legend>
            </Legends>
            <Titles>
                <asp:Title Font="Microsoft Sans Serif, 15pt" Name="Title1" 
                    Text="Relatório de MDR por Sexo e Óbito">
                </asp:Title>
            </Titles>
            <Series>
                <asp:Series Name="Vivo" Legend="Legend1">
                </asp:Series>
                <asp:Series ChartArea="ChartArea1" Legend="Legend1" Name="Morto">
                </asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1">
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
    </table>
    <table>
        <asp:Chart ID="ChartIdade" ImageStorageMode="UseImageLocation" ImageLocation="~/chart_images/#SEQ(100,10)" runat="server" Style="margin-right: 0px" Visible="False"
            Width="747px">
            <Titles>
                <asp:Title Font="Microsoft Sans Serif, 15pt" Name="Title1" Text="Distribuição de MDR por Idade">
                </asp:Title>
            </Titles>
            <Series>
                <asp:Series Name="Idade" IsValueShownAsLabel="True">
                </asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1" IsSameFontSizeForAllAxes="True">
                    <AxisX Interval="1">
                    </AxisX>
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
    </table>
    <table>
        <asp:Chart ID="ChartIdadeObito" ImageStorageMode="UseImageLocation" ImageLocation="~/chart_images/#SEQ(100,10)"  runat="server" Width="742px" Visible="False">
            <Legends>
                <asp:Legend Name="Legend1">
                </asp:Legend>
            </Legends>
            <Titles>
                <asp:Title Font="Microsoft Sans Serif, 15pt" Name="Title1" Text="Relatório de Distribuição de MDR por Idade e por Óbito">
                </asp:Title>
            </Titles>
            <Series>
                <asp:Series Name="Vivo" Legend="Legend1">
                </asp:Series>
                <asp:Series ChartArea="ChartArea1" Legend="Legend1" Name="Morto">
                </asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1">
                    <AxisX Interval="1">
                    </AxisX>
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
    </table>
    <table>
        <asp:Chart ID="ChartGrupoMateriais"  ImageStorageMode="UseImageLocation" ImageLocation="~/chart_images/#SEQ(100,10)" runat="server" Width="738px" 
            Visible="False" Height="318px">
            <Legends>
                <asp:Legend Name="Vivo" IsTextAutoFit="False">
                </asp:Legend>
                <asp:Legend Name="Morto" Font="Microsoft Sans Serif, 8pt, style=Bold" IsTextAutoFit="False">
                </asp:Legend>
            </Legends>
            <Titles>
                <asp:Title Name="Title1" Text="Grupos de Sítios" Font="Microsoft Sans Serif, 15pt">
                </asp:Title>
            </Titles>
            <Series>
                <asp:Series Name="Vivo" ChartType="Bar" Legend="Vivo">
                </asp:Series>
                <asp:Series ChartArea="ChartArea1" ChartType="Bar" Legend="Vivo" Name="Morto">
                </asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1">
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
    </table>
    <table align ="center">
        <asp:Chart ID="ChartMicrorganismo"  ImageStorageMode="UseImageLocation" ImageLocation="~/chart_images/#SEQ(100,10)"  runat="server" Width="1000px" Visible="False" Height="500px">
            <Legends>
                <asp:Legend Name="Vivo" IsTextAutoFit="False">
                </asp:Legend>
                <asp:Legend Name="Morto" Font="Microsoft Sans Serif, 8pt, style=Bold" IsTextAutoFit="False">
                </asp:Legend>
            </Legends>
            <Titles>
                <asp:Title Name="Title1" Text="Microrganismo" Font="Microsoft Sans Serif, 15pt">
                </asp:Title>
            </Titles>
            <Series>
                <asp:Series Name="Vivo" ChartType="Bar" Legend="Vivo">
                </asp:Series>
                <asp:Series ChartArea="ChartArea1" ChartType="Bar" Legend="Vivo" Name="Morto">
                </asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1">
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
    </table>
</asp:Content>
