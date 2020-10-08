<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Nova_Senha.aspx.cs" Inherits="Administracao_Nova_Senha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style7
        {
            width: 175px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h1 style="text-align: center">Nova Senha</h1>
<div>
<table align="center">
                <tr >
                    <td colspan = "2" style="width: 523px">
                        <h3 style="text-align: center">
                            CADASTRO DE UMA NOVA SENHA<br />
                        </h3>
                    </td>
                   
                </tr>
                <tr>
                    <td colspan = "2" style="width: 523px; text-align: center;">
                        Usuário:
                        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                        </asp:DropDownList>
                        <br />
                    </td>
                    
                    
                </tr>
                
                <tr>
                    <td colspan="2" style="width: 523px">
                    </td>
                </tr>
               
                 
                          
                            <tr>
                                <td align="right" >
                                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Nome de Usuário:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="UserName" runat="server" style="margin-left: 0px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                        ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" >
                                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Senha:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                        ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" >
                                    <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">Confirma Senha:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword"
                                        ErrorMessage="Confirmar senha." ToolTip="Confirm Password is required."
                                        ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            
              
                   
         
           
          
            
             
            
            <tr>
                <td align="center" colspan="2">
                    <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password"
                        ControlToValidate="ConfirmPassword" Display="Dynamic" ErrorMessage="A Senha e Confirmação de Senha devem corresponder."
                        ValidationGroup="CreateUserWizard1"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" style="color: Red;">
                    <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                    <asp:RegularExpressionValidator Display="Dynamic" ValidationGroup="CreateUserWizard1" ControlToValidate="Password" ID="MyPassordMinLengthValidator" ValidationExpression="^[\s\S]{7,}$" runat="server" ErrorMessage="A Senha deve ter 7 caracteres no mínimo."></asp:RegularExpressionValidator>

                </td>
            </tr>
            <tr>



                <td align="center" colspan="2">
                    <asp:Button ID="btnCad" runat="server" Text="Cadastrar"
                        ValidationGroup="CreateUserWizard1" OnClick="btnCad_Click1" />

                </td>
            </tr>
        </table>




    </div>
</asp:Content>

