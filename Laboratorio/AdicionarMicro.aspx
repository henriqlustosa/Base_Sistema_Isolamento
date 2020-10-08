<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AdicionarMicro.aspx.cs" Inherits="Laboratorio_Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script src="../js/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="../js/maskedinput-1.2.2.js" type="text/javascript"></script>
    <script type="text/javascript">

        function ConfirmMessage() {

            var myindex = document.getElementById('<%=ddlExcluir.ClientID %>');

            var SelValue = myindex.options[myindex.selectedIndex].text;
    
           
             var selectedvalue = confirm("Tem certeza que quer deletar o microrganismo " + SelValue + " ?");
             if (selectedvalue) {
                 document.getElementById('<%=txtconformmessageValue.ClientID %>').value = "Yes";
             } else {
                 document.getElementById('<%=txtconformmessageValue.ClientID %>').value = "No";
             }
         }
    </script>
    
    
    
    <style type="text/css">
        .style10
        {
            width: 249px;
        }
        .style13
        {
            width: 219px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h1 align = "center" style="width: 877px"> Cadastro de Microrganismo</h1>
    <br />
    <br />
    <p align ="center" > Adicionar Microrganismo</p>
     <table align="center" width: 674px>
               
       
           
       
        <tr>
               <td class="style13"   >
                   <asp:Label ID="lbNovoMaterial" runat="server" Text="Nome do Microrganismo"></asp:Label>
                   </td>
               <td class="style10"  >
                   <asp:TextBox ID="txbMicrorg" runat="server" Height="25px" Width="217px"></asp:TextBox>
                   &nbsp;</td>
            <td>
                     
            
                   
                <asp:Button ID="btnGravar" runat="server" Text="Gravar" Height="31px" 
                    Width="152px" onclick="btnGravar_Click" /></td>
            
            
            
        </tr>
         
    </table>
    </br>
    </br>
    </br>
    <p align ="center" > Excluir Microrganismo</p>
     <table align="center" width: 674px>
               
       
           
       
        <tr>
               <td class="style13"   >
                   Lista de Microrganismo</td>
               <td class="style10"  >
                   <asp:DropDownList ID="ddlExcluir" runat="server" Height="35px" Width="239px" 
                       >
                   </asp:DropDownList>
               </td>
            <td>
                     
            
                   
                <asp:Button ID="btnExcluir" runat="server" Text="Excluir Microrganismo" Height="33px" 
                    Width="154px" onclick="btnExcluir_Click" 
                    OnClientClick="javascript:ConfirmMessage();"/></td>
            
            
            
        </tr>
         </table>
         <asp:HiddenField ID="txtconformmessageValue" runat="server" />
</asp:Content>

