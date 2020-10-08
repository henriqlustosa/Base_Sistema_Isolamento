<%@ Page Title="Sistema Identificação" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Laboratorio.aspx.cs" Inherits="Laboratorio_Laboratorio" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>







<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script src="../js/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="../js/maskedinput-1.2.2.js" type="text/javascript"></script>
     <script type="text/javascript">



         function isNumberKey(evt) {
             var charCode = (evt.which) ? evt.which : evt.keyCode;

             if (charCode == 46 || charCode > 31
            && (charCode < 48 || charCode > 57))
                 return false;

             return true;
         }


     
         
    </script>
    <style type="text/css">
        .style7
        {
            width: 348px;
        }
    </style>

    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" 
        EnableScriptGlobalization="True">
    </asp:ToolkitScriptManager>
    <h1 align = "center" style="width: 873px">Laboratório</h1>
     
                <asp:Label ID="lbUser" runat="server" Visible="false"></asp:Label>
    
<asp:Panel ID="Paciente" runat="server" GroupingText="Paciente">
 
    <table align="center">
        <tr>
            <td >
                <asp:Label ID="lbRH"  runat="server" Text="Registro Hospitalar:" ></asp:Label>
            </td>
            <td class="style7">
                <asp:TextBox onkeypress="return isNumberKey(event)" ID="txbRH" class="campoAndar" runat="server"></asp:TextBox>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txbRH"
                    ErrorMessage="O campo [Registro Hospitalar] não foi preenchido." ValidationGroup="cadastro"
                    >*</asp:RequiredFieldValidator>
              </td>
              <td>
                <asp:Button ID="Pesquisar" runat="server" Text="Pesquisar" 
                      onclick="Pesquisar_Click" />
              
            </td>
            
            
        </tr>
        <tr>
            <td >
                <asp:Label ID="lbNome" runat="server" Text="Nome:"></asp:Label>
                
            </td>
            <td class="style7" >
                <asp:Label ID="lbNomePreenchido" class="campoNome" runat="server" Text=""></asp:Label>
               
            </td>
          
        </tr>
        
            <tr>
                <td >
                    <asp:Label ID="lbMicroorgnismo" runat="server" Text="Microrganismo:"></asp:Label>
                </td>
                <td class="style7" >
                 
                     <asp:DropDownList ID="ddlMicroorganismo" runat="server" 
                         DataSourceID="SqlDataSource1" DataTextField="descricao" 
                         DataValueField="cod_microorg" Height="23px" Width="257px">
                     </asp:DropDownList>
                     <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                         ConnectionString="<%$ ConnectionStrings:ConnectionStringIsolamento %>" 
                         ProviderName="<%$ ConnectionStrings:ConnectionStringIsolamento.ProviderName %>" 
                         SelectCommand="SELECT [cod_microorg], [descricao] FROM [tipos_microorganismos] order by descricao">
                     </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td >
                    <asp:Label ID="lbMaterial" runat="server" Text="Material:"></asp:Label>
                </td>
                <td class="style7" >
               
                     <asp:DropDownList ID="ddlMaterial" runat="server" 
                         DataSourceID="SqlDataSource2" DataTextField="descricao" 
                         DataValueField="cod_material" Height="25px" Width="255px">
                     </asp:DropDownList>
                     <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                         ConnectionString="<%$ ConnectionStrings:ConnectionStringIsolamento %>" 
                         ProviderName="<%$ ConnectionStrings:ConnectionStringIsolamento.ProviderName %>" 
                         SelectCommand="SELECT [cod_material], [descricao] FROM [tipos_materiais] order by descricao">
                     </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td >
                    <asp:Label ID="lbClinica" runat="server" Text="Clínica:"></asp:Label>
                </td>
                <td class="style7" >
                     <asp:Label ID="lbClinicaPreenchido" runat="server" Text=""></asp:Label>
                </td>
             </tr>
             <tr>
                <td >
                    <asp:Label ID="lbContato" runat="server" Text="Contato:"></asp:Label>
                </td>
                <td class="style7">
                     <asp:TextBox ID="txbContato" runat="server" Width="423px"></asp:TextBox>
           
                </td>
            </tr>
            <tr>
                <td >
                    <asp:Label ID="lbData" runat="server" Text="Data do Resultado do Exame:"></asp:Label>
                </td>
                <td class="style7" >
                <asp:TextBox ID="txbData" runat="server" ></asp:TextBox>
                <asp:Image ID="Image1" runat="server" ImageUrl="~/image/Calendar_scheduleHS.png" 
                        Width="17px" />
                    <asp:CalendarExtender id="txbData_CalendarExtender" runat="server" 
                    enabled="True" targetcontrolid="txbData" popupbuttonid="Image1" 
                    format="dd/MM/yyyy">
                    </asp:CalendarExtender>
                   
                 
               
                 
                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ControlToValidate="txbData" ErrorMessage="*" Type="Date" 
                    Operator="DataTypeCheck" ValidationGroup="2"></asp:CompareValidator>
                </td>
        </tr>
     
    </table>
  
    </asp:Panel>  
   
     <br/>
   
     <br/>
 
   
<table align = "center">
<tr>
<td>
    <asp:Button ID="btnGravar" runat="server" Text="Gravar" 
        onclick="btnGravar_Click" ValidationGroup="cadastro" />
</td>



</tr>
</table>
  
 
    

    
    

 
    
         
    

     
  
 
    <br />
  <table align="center" >

  </table>

  <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Atenção"
        ShowMessageBox="True" ShowSummary="False" ValidationGroup="cadastro"  />
</asp:Content>

