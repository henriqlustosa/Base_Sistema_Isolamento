using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class Laboratorio_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            AtualizarDDLExcluir();

        }

    }
    protected void btnGravar_Click(object sender, EventArgs e)
    {

        using (SqlConnection cnn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringIsolamento"].ToString()))
        {

            SqlCommand cmm2 = cnn2.CreateCommand();
            cmm2.CommandText = "SELECT * FROM [Isolamento].[dbo].[tipos_microorganismos] WHERE descricao = '" + txbMicrorg.Text.ToUpper() + "'";
            cnn2.Open();
            SqlDataReader dr2 = cmm2.ExecuteReader();
            if (!dr2.Read())
            {
                using (SqlConnection cnn1 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringIsolamento"].ToString()))
                {
                    SqlCommand cmm1 = cnn1.CreateCommand();
                    cmm1.CommandText = "INSERT INTO [Isolamento].[dbo].[tipos_microorganismos] VALUES (@nome)";


                    cmm1.Parameters.Add("@nome", SqlDbType.VarChar).Value = txbMicrorg.Text.ToUpper();


                    try
                    {
                        cnn1.Open();
                        cmm1.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        string err = ex.Message;
                    
                    }

                }//using
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Gravado com sucesso!');", true);

                txbMicrorg.Text = "";

            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('O microrganisnmo " + txbMicrorg.Text.Trim() + " já está cadastrado!');", true);
                txbMicrorg.Text = "";
            }
        }
        AtualizarDDLExcluir();

    }


    protected void btnExcluir_Click(object sender, EventArgs e)
    {
        if (txtconformmessageValue.Value == "Yes")
        {
            string codMicro = ddlExcluir.SelectedValue;

            string strQuery = "";

            strQuery = "DELETE FROM tipos_microorganismos " +

            " where cod_microorg = " + codMicro;
            using (SqlConnection cnn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringIsolamento"].ToString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(strQuery, cnn2);
                    cnn2.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                }

                catch (Exception ex)
                {
                    string erro = ex.Message;
                }
            }

        }// if
        AtualizarDDLExcluir();
    }
    public void AtualizarDDLExcluir()
    {

        ddlExcluir.DataTextField = "descricao";
        ddlExcluir.DataValueField = "cod_microorg";
        using (SqlConnection cnn3 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringIsolamento"].ToString()))
        {

            SqlCommand cmm3 = cnn3.CreateCommand();
            cmm3.CommandText = "SELECT * FROM [Isolamento].[dbo].[tipos_microorganismos] order by descricao ";
            cnn3.Open();
            SqlDataReader dr3 = cmm3.ExecuteReader();


            ddlExcluir.DataSource = dr3;
            ddlExcluir.DataBind();



        }


    }
}
