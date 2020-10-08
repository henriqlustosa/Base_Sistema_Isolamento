<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="cadUser.aspx.cs" Inherits="Administracao_cadUser2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1 style="text-align: center">
        Cadastro de Usuários</h1>
    <div style="text-align: center">
    <table align="center">
    <tr>
    <td>
        <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" 
            RequireEmail="False" >
            <FinishNavigationTemplate>
                <asp:Button ID="FinishPreviousButton" runat="server" CausesValidation="False" 
                    CommandName="MovePrevious" Text="Previous" />
                <asp:Button ID="FinishButton" runat="server" CommandName="MoveComplete" 
                    Text="Finish" />
            </FinishNavigationTemplate>
            <StepNavigationTemplate>
                <asp:Button ID="StepPreviousButton" runat="server" CausesValidation="False" 
                    CommandName="MovePrevious" Text="Previous" />
                <asp:Button ID="StepNextButton" runat="server" CommandName="MoveNext" 
                    Text="Next" />
            </StepNavigationTemplate>
            <StartNavigationTemplate>
                <asp:Button ID="StartNextButton" runat="server" CommandName="MoveNext" Text="Next" />
            </StartNavigationTemplate>
            <WizardSteps>
                <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server">
                    <ContentTemplate>
                        <table border="0">
                            <tr>
                                <td colspan = "2" style="width: 523px">
                        <h3 style="text-align: center">
                            CADASTRO DE UMA NOVA CONTA<br />
                        </h3>
                    </td>
                   
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Nome de Usuário:</asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="UserName"  runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                        ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Senha:</asp:Label>
                                </td>
                                <td align ="left" >
                                    <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                        ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">Confirma Senha:</asp:Label>
                                </td>
                                <td align = "left">
                                    <asp:TextBox ID="ConfirmPassword"  runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword"
                                        ErrorMessage="Confirm Password is required." ToolTip="Confirm Password is required."
                                        ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                             <tr>
                <td align="right">
                    <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">
                        E-mail:</asp:Label></td>
                <td align="left">
                    <asp:TextBox ID="Email"  runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email"
                        ErrorMessage="E-mail is required." ToolTip="E-mail is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                </td>
            </tr>
          
            
             
            
            <tr>
                <td align="center" colspan="2">
                    <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password"
                        ControlToValidate="ConfirmPassword" Display="Dynamic" ErrorMessage="The Password and Confirmation Password must match."
                        ValidationGroup="CreateUserWizard1"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" style="color: red">
                    <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                </td>
            </tr>
        </table>
                        </table>
                    </ContentTemplate>
                    <CustomNavigationTemplate>
                        <table border="0" cellspacing="5" style="width: 100%; height: 100%;">
                            <tr align="right">
                               <td  style="text-align: center;" >
                                    <asp:Button ID="StepNextButton"  align ="center" runat="server" CommandName="MoveNext" Text="Criar Usuário"
                                        ValidationGroup="CreateUserWizard1" />
                                </td>
                            </tr>
                        </table>
                    </CustomNavigationTemplate>
                </asp:CreateUserWizardStep>
                <asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server">
                    <ContentTemplate>
                        <table border="0">
                            <tr>
                                <td align="center" colspan="2">
                                    Completo
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Conta criada com Sucesso.
                                </td>
                            </tr>
                            <tr>
                                <td  style="text-align: center;" >
                                    <asp:Button ID="ContinueButton"  align ="center"  runat="server" CausesValidation="False" CommandName="Continue"
                                        Text="Continue" ValidationGroup="CreateUserWizard1" />
                                </td>
                                
          
                    
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:CompleteWizardStep>
            </WizardSteps>
        </asp:CreateUserWizard>
    </td>
    </tr>
    </table>
    
    </div>
</asp:Content>

