<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
CodeFile="GraficoRelatorio_2.aspx.cs" Inherits="Relatorio_GraficoRelatorio_2" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../js/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="../js/maskedinput-1.2.2.js" type="text/javascript"></script>

    <script type="text/javascript">
        




    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<h1 align="center" style="width: 877px">
       Gráfico dos Pacientes
      
    </h1>
      <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
    <asp:TabContainer ID="tcPagina" runat="server" ActiveTabIndex="1">
        <asp:TabPanel runat="server" HeaderText="Microrganismos" ID="tbMic">
         <ContentTemplate>
          <table align="center">
    <tr>
    
         <td >
                MICRORGANISMOS:
            </td>
            <td align="center" colspan="2">
                <asp:DropDownList ID="ddlmicrorganismos" runat="server" DataSourceID="SqlDataSource" DataTextField="Descricao"
                    DataValueField="Descricao">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringIsolamento %>"
                    ProviderName="<%$ ConnectionStrings:ConnectionStringIsolamento.ProviderName %>"
                    SelectCommand="SELECT [descricao] AS Descricao FROM [Isolamento].[dbo].[tipos_microorganismos] ">
                </asp:SqlDataSource>
            </td>
            
           
            
        </tr>
         <tr>
            <td >
                MÊS INICIAL:
            </td>
            <td >
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
            <td >
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ANO INICIAL:
            </td>
            <td >
                <asp:DropDownList ID="ddlAnoInicial" runat="server" DataSourceID="SqlDataSource2"
                    DataTextField="Ano" DataValueField="Ano">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringIsolamento %>"
                    ProviderName="<%$ ConnectionStrings:ConnectionStringIsolamento.ProviderName %>"
                    SelectCommand="SELECT DISTINCT YEAR([dt_resultado]) AS Ano FROM [Isolamento].[dbo].[Exame] ORDER BY Ano DESC">
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td >
                MÊS FINAL:
            </td>
            <td >
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
            <td >
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ANO FINAL:
            </td>
            <td >
                <asp:DropDownList ID="ddlAnoFinal" runat="server" DataSourceID="SqlDataSource3" DataTextField="Ano"
                    DataValueField="Ano">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringIsolamento %>"
                    ProviderName="<%$ ConnectionStrings:ConnectionStringIsolamento.ProviderName %>"
                    SelectCommand="SELECT DISTINCT YEAR([dt_resultado]) AS Ano FROM [Isolamento].[dbo].[Exame] ORDER BY Ano DESC">
                </asp:SqlDataSource>
            </td>
              <td align="center" colspan="4">
                <asp:Button ID="Button2" runat="server" Text="Carregar Gráficos" OnClick="btnCarregar2_Click" />
            </td>
        </tr>
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
                <asp:Title Name="Title1" Text="Grupos de Sítios por Microrganismo" Font="Microsoft Sans Serif, 15pt">
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
        
        </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="tcGrupoSitio" runat="server" HeaderText="Grupos de Sítios">
         <ContentTemplate>
         <table align="center">
     <tr>
         <td >
             GRUPOS DE SÍTIOS:
            </td>
            <td align="center" colspan="2">
                <asp:DropDownList ID="ddlGrupos" runat="server" DataSourceID="SqlDataSource4" DataTextField="Descricao"
                    DataValueField="Descricao">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringIsolamento %>"
                    ProviderName="<%$ ConnectionStrings:ConnectionStringIsolamento.ProviderName %>"
                    SelectCommand="SELECT [descricao] AS Descricao FROM [Isolamento].[dbo].[Grupo_Materiais] ">
                </asp:SqlDataSource>
            </td>
            
             
            
        </tr>
        <tr>
            <td >
                MÊS INICIAL:
            </td>
            <td >
                <asp:DropDownList ID="ddlMesInicial2" runat="server">
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
            <td >
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ANO INICIAL:
            </td>
            <td >
                <asp:DropDownList ID="ddlAnoInicial2" runat="server" DataSourceID="SqlDataSource5"
                    DataTextField="Ano" DataValueField="Ano">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringIsolamento %>"
                    ProviderName="<%$ ConnectionStrings:ConnectionStringIsolamento.ProviderName %>"
                    SelectCommand="SELECT DISTINCT YEAR([dt_resultado]) AS Ano FROM [Isolamento].[dbo].[Exame] ORDER BY Ano DESC">
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td >
                MÊS FINAL:
            </td>
            <td >
                <asp:DropDownList ID="ddlMesFinal2" runat="server">
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
            <td >
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ANO FINAL:
            </td>
            <td >
                <asp:DropDownList ID="ddlAnoFinal2" runat="server" DataSourceID="SqlDataSource6" DataTextField="Ano"
                    DataValueField="Ano">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringIsolamento %>"
                    ProviderName="<%$ ConnectionStrings:ConnectionStringIsolamento.ProviderName %>"
                    SelectCommand="SELECT DISTINCT YEAR([dt_resultado]) AS Ano FROM [Isolamento].[dbo].[Exame] ORDER BY Ano DESC">
                </asp:SqlDataSource>
            </td>
            <td align="center" colspan="4">
                <asp:Button ID="btnExportar" runat="server" Text="Carregar Gráficos" OnClick="btnCarregar_Click" />
            </td>
        </tr>
       
          
    
        
    </table>
     <table align ="center">
        <asp:Chart ID="ChartMicrorganismo"  ImageStorageMode="UseImageLocation" ImageLocation="~/chart_images/#SEQ(100,10)"  runat="server" Width="738px" Visible="False" Height="318px">
            <Legends>
                <asp:Legend Name="Vivo" IsTextAutoFit="False">
                </asp:Legend>
                <asp:Legend Name="Morto" Font="Microsoft Sans Serif, 8pt, style=Bold" IsTextAutoFit="False">
                </asp:Legend>
            </Legends>
            <Titles>
                <asp:Title Name="Title1" Text="Microrganismo por Grupo de Sítio" Font="Microsoft Sans Serif, 15pt">
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
     </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
    
  
       
    
</asp:Content>

