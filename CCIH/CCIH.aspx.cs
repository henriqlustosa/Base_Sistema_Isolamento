using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Drawing;
using System.Collections;
using System.Net;
using Newtonsoft.Json;
using System.Reflection;

public partial class CCIH : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                String cd_prontuario = "";
                DateTime now = DateTime.Now;
                lbDataHora.Text = now.ToString();
                string URI = "http://intranethspm:5003/hspmsgh-api/censo/";
                WebRequest request = WebRequest.Create(URI);
                List<Censo> final = new List<Censo>();

                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(URI);
                // Sends the HttpWebRequest and waits for a response.
                HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    var reader = new StreamReader(httpResponse.GetResponseStream());

                    JsonSerializer json = new JsonSerializer();

                    var objText = reader.ReadToEnd();

                    var details = JsonConvert.DeserializeObject<List<Censo>>(objText);

                    foreach (Censo detail in details)
                    {
                        cd_prontuario = detail.cd_prontuario;

                        // Buscar na Base de Dados do Sistema Isolado se o paciente que se encontra no censo hospitalar já foi alguma vez positivado com MDR 
                        using (SqlConnection cnn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringIsolamento"].ToString()))
                        {

                            SqlCommand cmm2 = cnn2.CreateCommand();
                            cmm2.CommandText = "SELECT rh FROM Paciente WHERE rh = " + cd_prontuario;
                            cnn2.Open();
                            SqlDataReader dr2 = cmm2.ExecuteReader();
                            if (dr2.Read())
                            {


                                final.Add(detail);

                            }//if


                        }//using

                    }
          

                    GridInternado.DataSource = final; // apresentação dos dados da lista
                    GridInternado.DataBind();
                }


            }

            catch (WebException ex)
            {
                string err = ex.Message;
            }
        }
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        DateTime dtarq = DateTime.Now;

        string dia = Convert.ToString(Convert.ToInt32(dtarq.Day));//dia atual + 1 = dia seguinte
        if (dia.Length == 1)
            dia = dia.PadLeft(2, '0');

        string mes = Convert.ToString(dtarq.Month);
        if (mes.Length == 1)
            mes = mes.PadLeft(2, '0');

        string data = Convert.ToString(dia) + Convert.ToString(mes) + Convert.ToString(dtarq.Year);

        HttpResponse oResponse = System.Web.HttpContext.Current.Response;
        System.IO.StringWriter tw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);

        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=Isolamento" + data + ".xls");
        HttpContext.Current.Response.Charset = "UTF-8";

        GridInternado.RenderControl(hw);

        oResponse.Write(tw.ToString());
        oResponse.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }


    public class Censo
    {


        public string cd_prontuario { get; set; }

        public string nm_paciente { get; set; }

        public string dt_nascimento { get; set; }
        public string nr_quarto { get; set; }

        public string dt_internacao_data { get; set; }

        public string dt_internacao_hora { get; set; }
        public string nm_clinica { get; set; }

        public string in_sexo { get; set; }

        public string nr_idade { get; set; }


        public string cod_CID { get; set; }

        public string descricaoCID { get; set; }

        public string nm_unidade_funcional { get; set; }
        public string tempo { get; set; }

        public string vinculo { get; set; }

        


    }
  
  
  

}

