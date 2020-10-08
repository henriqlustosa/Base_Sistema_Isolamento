<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Graficos_3.aspx.cs" Inherits="Relatorio_Graficos_3" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script src="../js/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="../js/maskedinput-1.2.2.js" type="text/javascript"></script>

    <script type="text/javascript">
        




    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<h1 align="center" style="width: 877px">
        Gráfico de Exames Positivados
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
    </h1>
    <asp:TabContainer ID="graphMic" runat="server" ActiveTabIndex="2">
        <asp:TabPanel runat="server" HeaderText="Microrganismos" ID="TabPanel1">
        <ContentTemplate>
          <table align ="center">
        <tr>
            
            <td>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; MICRORGANISMOS:
            </td>
            <td>
                <asp:DropDownList ID="ddlmicrorganismos2" runat="server" DataSourceID="SqlDataSource6" DataTextField="Descricao"
                    DataValueField="Descricao">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringIsolamento %>"
                    ProviderName="<%$ ConnectionStrings:ConnectionStringIsolamento.ProviderName %>"
                    SelectCommand="SELECT [descricao] AS Descricao FROM [Isolamento].[dbo].[tipos_microorganismos]  ">
                </asp:SqlDataSource>
            </td>
              <td >
               ANO:
            </td>
            <td >
                <asp:DropDownList ID="ddlAno2" runat="server" DataSourceID="SqlDataSource5" DataTextField="Ano" AppendDataBoundItems="true"
                    DataValueField="Ano">
                     <asp:ListItem Value="Tudo" Text="Tudo"></asp:ListItem>
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringIsolamento %>"
                    ProviderName="<%$ ConnectionStrings:ConnectionStringIsolamento.ProviderName %>"
                    SelectCommand="SELECT DISTINCT YEAR([dt_resultado]) AS Ano FROM [Isolamento].[dbo].[Exame] ORDER BY Ano DESC">
                </asp:SqlDataSource>
            </td>
              <td align="center" colspan="4">
                <asp:Button ID="Button1" runat="server" Text="Carregar Gráfico" OnClick="btnCarregar3_Click" />
            </td>
        </tr>
        </table>
        <table>
        <asp:Chart ID="ChartDistribuicaoMic" ImageStorageMode="UseImageLocation" ImageLocation="~/chart_images/#SEQ(100,10)" runat="server" Style="margin-right: 0px" Visible="False"
            Width="747px">
            <Titles>
                <asp:Title Font="Microsoft Sans Serif, 15pt" Name="Title1" Text="Distribuição de Microrganismo por Ano">
                </asp:Title>
            </Titles>
           
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1" IsSameFontSizeForAllAxes="True">
                    <AxisX Interval="1">
                    </AxisX>
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
    </table>  
        
        </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="graphGrupoSiio" runat="server" HeaderText="Grupos Sitios">
        <ContentTemplate> 
        <table align ="center">
        <tr>
              
            <td>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Grupos de Sitios:
            </td>
            <td>
                <asp:DropDownList ID="dllGrupoSitios" runat="server" DataSourceID="SqlDataSource8" DataTextField="Descricao"
                    DataValueField="Descricao">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource8" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringIsolamento %>"
                    ProviderName="<%$ ConnectionStrings:ConnectionStringIsolamento.ProviderName %>"
                    SelectCommand="SELECT [descricao] AS Descricao FROM [Isolamento].[dbo].[Grupo_Materiais] ">
                </asp:SqlDataSource>
            </td>
            <td >
               ANO:
            </td>
            <td >
                <asp:DropDownList ID="ddlAno3" runat="server" DataSourceID="SqlDataSource7" DataTextField="Ano" AppendDataBoundItems="true"
                    DataValueField="Ano">
                     <asp:ListItem Value="Tudo" Text="Tudo"></asp:ListItem>
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource7" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringIsolamento %>"
                    ProviderName="<%$ ConnectionStrings:ConnectionStringIsolamento.ProviderName %>"
                    SelectCommand="SELECT DISTINCT YEAR([dt_resultado]) AS Ano FROM [Isolamento].[dbo].[Exame] ORDER BY Ano DESC">
                </asp:SqlDataSource>
            </td>
              <td align="center" colspan="4">
                <asp:Button ID="Button4" runat="server" Text="Carregar Gráfico" OnClick="btnCarregar4_Click" />
            </td>
        </tr>
        </table>
        <table>
        <asp:Chart ID="ChartGrupoSitio" ImageStorageMode="UseImageLocation" ImageLocation="~/chart_images/#SEQ(100,10)" runat="server" Style="margin-right: 0px" Visible="False"
            Width="747px">
            <Titles>
                <asp:Title Font="Microsoft Sans Serif, 15pt" Name="Title1" Text="Distribuição de Grupos de Sitios por Ano">
                </asp:Title>
            </Titles>
            
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1" IsSameFontSizeForAllAxes="True">
                    <AxisX Interval="1">
                    </AxisX>
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
    </table>
         
        </ContentTemplate>
        </asp:TabPanel>
      
    </asp:TabContainer>
    <p align="center" style="width: 877px">
        &nbsp;</p>
    
    
  
    
   
    
</asp:Content>

