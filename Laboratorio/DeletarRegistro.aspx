<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DeletarRegistro.aspx.cs" Inherits="Laboratorio_DeletarRegistro" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script src="../js/jquery-1.4.1.min.js" type="text/javascript"></script>

     <script type="text/javascript">
     

        function ConfirmMessage() {
            
            


            var selectedvalue = confirm("Tem certeza que quer deletar o registro ?");
            if (selectedvalue) {
                document.getElementById('<%=txtconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtconformmessageValue.ClientID %>').value = "No";
            }
        }
  
         
    </script>
    
    <style type="text/css">
        .style14
        {
            width: 314px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="True">
    </asp:ToolkitScriptManager>
<h1 align = "center" style="width: 877px"> Pesquisa&nbsp; para Excluir Registro</h1>
    
    <table align ="center" >
    <tr>
        <td class="style14">
            <asp:Label ID="lbData" runat="server" Text="Data para Pesquisa dos Atendimentos:"></asp:Label>
        </td>
        <td></td>
     </tr>
     <tr>
        <td class="style14">
        <asp:TextBox ID="txbData" runat="server" ></asp:TextBox>
        <asp:Image ID="Image1" runat="server" ImageUrl="~/image/Calendar_scheduleHS.png" 
                        Width="17px" />
            <asp:CalendarExtender id="txbData_CalendarExtender" runat="server" 
                    enabled="True" targetcontrolid="txbData" popupbuttonid="Image1" 
                    format="dd/MM/yyyy">
            </asp:CalendarExtender>
        <td>
            <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" onclick="btnPesquisar_Click" 
                 />
        </td>
      </tr>
    </table>
    <br />
    <br />

         
    </table>
   
    </br>
   
    
    </br>
 <asp:Panel ID="pnDados" runat="server" GroupingText="Dados" >
   <asp:GridView ID="gvDados" 
            runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" 
          AutoGenerateColumns="False" 
           onrowdatabound="gvDados_RowDataBound"   >
       
         <Columns>
        
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="cbDados" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:boundfield DataField="Cod_Exame" HeaderText="Cod_Exame"></asp:boundfield>
                    <asp:boundfield DataField="RH" HeaderText="RH"></asp:boundfield>
                    <asp:boundfield DataField="Nome" HeaderText="Nome"></asp:boundfield>
                   <asp:boundfield DataField="Microorganismo" HeaderText="Microorganismo"></asp:boundfield>
                    <asp:boundfield DataField="Material" HeaderText="Material"></asp:boundfield>
                   <asp:boundfield DataField="Data" HeaderText="Data do Resultado"></asp:boundfield>
                    
                
        </Columns>
      
             <RowStyle BackColor="#F7F6F3" ForeColor="#333333"  />
             <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
             <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#FF66CDAA" Font-Bold="True" ForeColor="White" />
             <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
             <EditRowStyle BackColor="#999999" />
             <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
        <br />
        
        </asp:Panel>
         <br /> <br />
  
   
    <table align= "center">
    <tr>     
        <td>
            <asp:Button ID="btnExcluir" runat="server" Text="Excluir Marcados" Width="147px" 
                onclick="btnExcluir_Click"  OnClientClick="javascript:ConfirmMessage();" />
        </td>
    </tr>
   
        
    </table>
    <p>
    <asp:Label ID="DeleteResults" runat="server" EnableViewState="False" 
        Visible="False"></asp:Label>
</p>
<asp:HiddenField ID="txtconformmessageValue" runat="server" />
</asp:Content>

