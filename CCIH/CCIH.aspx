<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CCIH.aspx.cs" Inherits="CCIH" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h1  style="width: 877px"> Relatório de Pacientes Internados com Microrganismo</h1>


    <asp:Label ID="lbDataHora" runat="server" Text="Label"></asp:Label><br />
    <br />

    <asp:GridView ID="GridInternado" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" >
    <RowStyle BackColor="#F7F6F3" ForeColor="#333333"  />
             <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
             <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#FF66CDAA" Font-Bold="True" ForeColor="White" />
             <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
             <EditRowStyle BackColor="#999999" />
             <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    </asp:GridView>
    <table>
    <tr>
          
        
        <td >
            <asp:Button ID="btnExportar" runat="server" Text="Exportar para Excel" onclick="btnExportar_Click" 
                 />
        </td>
      </tr>
    </table>
</asp:Content>

