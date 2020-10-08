using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class Laboratorio_DeletarRegistro : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
     

    }
    protected void btnExcluir_Click(object sender, EventArgs e)
    {
        bool Marcado = false;
        if (txtconformmessageValue.Value == "Yes")
        {

            foreach (GridViewRow row in gvDados.Rows)
            {
                // Access the CheckBox
                CheckBox cb = (CheckBox)row.FindControl("cbDados");
                if (cb != null && cb.Checked)
                {

                    // First, get the codProfissional,codGrupo for the selected row
                    int codExame =
                       Convert.ToInt32(row.Cells[1].Text);
                     string strQuery = "";

                    strQuery = "DELETE FROM Exame " +

                    " where cod_exm = " + codExame.ToString();
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
                    Marcado = true;

                    }
                }
            }
            


            if (Marcado == false)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Nenhum item selecionado para ser excluído!');", true);


            }
            Marcado = false;

        }
        
        gvDados.SelectedIndex = -1; // para desabilitar o selected row style
        gvDados.DataSource = null;
        gvDados.DataBind();
      
    }
    protected void btnPesquisar_Click(object sender, EventArgs e)
    {

      

        gvDados.SelectedIndex = -1;
        using (SqlConnection cnn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringIsolamento"].ToString()))
        {

            SqlCommand cmm2 = cnn2.CreateCommand();
            cmm2.CommandText = "SELECT e.cod_exm as Cod_Exame, p.rh as RH ,p.nome  as Nome ,m.descricao as Microorganismo ,ma.descricao as Material , convert(varchar, e.dt_resultado, 103) as 'Data' FROM [Isolamento].[dbo].[Exame] as e "
            + " INNER JOIN [Isolamento].[dbo].[Paciente] as p ON e.rh = p.rh "
    + " INNER JOIN [Isolamento].[dbo].[tipos_microorganismos] as  m ON e.microorganismo = m.cod_microorg "
    + " INNER JOIN [Isolamento].[dbo].[tipos_materiais] as  ma ON e.material = ma.cod_material where e.dt_resultado = '" + corrigirData(txbData.Text) + "'";
            cnn2.Open();
            SqlDataReader dr2 = cmm2.ExecuteReader();

            if (dr2.HasRows)
            {
                gvDados.DataSource = dr2;
                gvDados.DataBind();

            }
            else
            {
                gvDados.DataSource = null;
                gvDados.DataBind();
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Atenção! Não existe registro nesta data');", true);
                dr2.Close();



            }




        }
    }
    
          protected void gvDados_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // Esconder algumas colunas do gridView Dados para aparecer apenas o nome do Profissional e da Clinica 
        e.Row.Cells[1].Visible = false;// codigo do Profissional
     
    }
          public string corrigirData(string dataErrada)
          {
              string mes =dataErrada.Substring(3,2);
              
              string dia =dataErrada.Substring(0,2);

              string ano = dataErrada.Substring(6, 4);


              string dataSistem = ano + "-" + mes + "-" + dia;
              return dataSistem;
                
          }
       
    
}
