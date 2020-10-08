using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Web.Services;

public partial class Administracao_deleteLogin : System.Web.UI.Page
{
    [WebMethod]

    public static string[] GetUserId(string prefixo)
    {
        List<string> clientes = new List<string>();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionStringIsolamento"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "SELECT [UserName],[UserId] FROM [Isolamento].[dbo].[aspnet_Users] where Username like @Texto +'%'";
                cmd.Parameters.AddWithValue("@Texto", prefixo);
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        clientes.Add(string.Format("{0};{1}", sdr["UserName"], sdr["UserId"]));
                    }
                }
                conn.Close();
            }
        }
        return clientes.ToArray();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["UserId"] != null )
            {
                txbUserId.Text = Request.QueryString["UserId"];
                txbUser.Text = Request.QueryString["UserName"];
            }
        }
    }

    protected void btDelete_Click(object sender, EventArgs e){
        txbUserId.Enabled = true;
        txbUser.Enabled = true;
        string userId = txbUserId.Text;
        Deletar(userId);
        txbUserId.Text = "";
        txbUser.Text = "";
       txbUserId.Enabled = false;
        txbUser.Enabled = false;
       
    }
    public void Deletar(string userId)
    {
        if (!userId.Equals(""))
        {
            string sSqlMember = "DELETE FROM Isolamento.dbo.aspnet_Membership WHERE userId = '" + userId + "';";
            string sSqlUserInRole = "DELETE FROM Isolamento.dbo.aspnet_UsersInRoles WHERE UserId = '" + userId + "';";
            string sSqlUsers = "DELETE FROM Isolamento.dbo.aspnet_Users WHERE UserId = '" + userId + "';";

            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringIsolamento"].ToString()))
            {
                SqlCommand cmm = new SqlCommand();
                try
                {
                    cmm.Connection = cnn;
                    cnn.Open();

                    cmm.CommandText = sSqlMember + sSqlUserInRole + sSqlUsers;

                    cmm.ExecuteNonQuery();
                    Response.Write("<script language='javascript'>alert('Gravado com Sucesso!');</script>");
                    Response.Redirect("~/Administracao/deleteLogin.aspx");
                }
                catch (SqlException e1)
                {
                    Response.Write("<script language='javascript'>alert('Erro ao inserir registro :' " + e1 + "');</script>");
                }
            }
        }
        else
        {
            Response.Write("<script language='javascript'>alert('Selecione um registro');</script>");
        }
    }
    protected void btnPesquisarNome_Click(object sender, EventArgs e)
    {
        string id = hfCustomerId.Value;
        txbUserId.Enabled = true;
        txbUser.Enabled = true;
        txbUserId.Text = hfCustomerId.Value;
        txbUser.Text = txtProcurar.Text;
       txbUserId.Enabled = false;
       txbUser.Enabled = false;
     

    }
}
