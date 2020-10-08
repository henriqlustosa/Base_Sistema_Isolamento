<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Pesquisa.aspx.cs" Inherits="Pesquisa_Pesquisa" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script src="../Scripts/jquery-2.2.3.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui-1.11.4.min.js" type="text/javascript"></script>

    <link href="../Scripts/jquery-ui.css" rel="stylesheet" type="text/css" />

 

     <script type="text/javascript">

         $(function() {
             $("[id$=txtProcurar]").autocomplete({
                 source: function(request, response) {
                     $.ajax({
                         url: '<%=ResolveUrl("~/Internacao/Pesquisa.aspx/GetClientes") %>',
                         data: "{ 'prefixo': '" + request.term + "'}",
                         dataType: "json",
                         type: "POST",
                         contentType: "application/json; charset=utf-8",
                         success: function(data) {
                             response($.map(data.d, function(item) {
                                 return {
                                     label: item.split('-')[0],
                                     val: item.split('-')[1]
                                 }
                             }))
                         },
                         error: function(response) {
                             alert(response.responseText);
                         },
                         failure: function(response) {
                             alert(response.responseText);
                         }
                     });
                 },
                 select: function(e, i) {
                     $("[id$=hfCustomerId]").val(i.item.val);
                 },
                 minLength: 1
             });
         });


         function isNumberKey(evt) {
          
            

                 var charCode = (evt.which) ? evt.which : evt.keyCode;

                 if (charCode == 46 || charCode > 31
            && (charCode < 48 || charCode > 57))
                     return false;

                 return true;
             
    }



    function submitButton(event) {
    
        if (event.which == 13) {


            //alert($("input[id=btnPesquisar]"));

            document.getElementById('<%=btnPesquisar.ClientID%>').value = 'testing out';
           
        }
    }
         
    </script>
    
    <style type="text/css">
        .style14
        {
            width: 197px;
        }
        .style15
        {
            width: 197px;
            height: 42px;
        }
        .style16
        {
            height: 42px;
        width: 39px;
    }
        .style17
    {
        width: 312px;
    }
    .style18
    {
        width: 39px;
    }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    
    <h1 align = "center" style="width: 877px"> Pesquisa </h1>
    
    <table id="my_table" align ="center" style="margin-left: 1px;" class="style17" >
      <tr>
        <td class="style15">
            <asp:Label ID="Label1" runat="server" Text="NOME DO PACIENTE:"></asp:Label>
        </td>
        <td class="style16"></td>
     </tr>
     <tr>
        <td class="style14">
   <asp:TextBox ID="txtProcurar" runat="server" Width="269px" Height="28px"   />
     <asp:HiddenField ID="hfCustomerId" runat="server" />
           
               
              
        </td>
        <td class="style18">
            <asp:Button ID="btnPesquisarNome" runat="server" Text="Pesquisar" 
                onclick="btnPesquisarNome_Click" />
        </td>
      </tr>
    <tr>
        <td class="style15">
            <asp:Label ID="lbData" runat="server" Text="RH PARA PESQUISA:"></asp:Label>
        </td>
        <td class="style16"></td>
     </tr>
     <tr>
        <td class="style14">
            <asp:TextBox  ID="txtRH" runat="server" Height="28px" Width="148px"  onKeyDown="submitButton(event)"
                ></asp:TextBox>
               
              
        </td>
        <td class="style18">
            <asp:Button ID="btnPesquisar" class ="teste" runat="server" Text="Pesquisar" onclick="btnPesquisar_Click" />
        </td>
      </tr>
    </table>
    <br />
    <br />

           <br />  
           <br />
    
   
 <asp:Panel ID="pnDados" runat="server" GroupingText="Dados" >
   <asp:GridView ID="gvDados" 
            runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" 
          AutoGenerateColumns="False" >
       
         <Columns>
        
                  
                    
                    <asp:boundfield DataField="RH" HeaderText="RH"></asp:boundfield>
                    <asp:boundfield DataField="Nome" HeaderText="Nome"></asp:boundfield>
                   <asp:boundfield DataField="Microorganismo" HeaderText="Microorganismo"></asp:boundfield>
                    <asp:boundfield DataField="Sitio" HeaderText="Sitio"></asp:boundfield>
                    <asp:boundfield DataField="Data" HeaderText="Data do Exame"></asp:boundfield>
                   
                
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
  
   
        
    </table>
    <p>
        &nbsp;</p>
</asp:Content>

