using System;
using System.Configuration;
using System.Data;
//using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["Usuario"] = null;
        LblError.Visible = false;

    }
    protected void BtnLogueo_Click(object sender, EventArgs e)
    {
        try
        {
            //verifico usuario

            //string ciusu = TxtNomUsu.Text.Trim();
            //string paswd = (TxtPassUsu.Text.Trim()).ToString();
            //String Nombreusu = null;
            N_EntidadesCompartidas.Usuarios unUsu = N_Logica.LogicaUsuarios.Logueo(TxtNomUsu.Text.Trim(), TxtPassUsu.Text.Trim());
            if (unUsu != null)
            {
                //si llego aca es pq es valido
                Session["Usuario"] = unUsu;
                // LblError.Text = (unUsu.CIEmpleado.);
                Response.Redirect("test.aspx");
            }
            else
                LblError.Visible = true;
            LblError.Text = "Datos Incorrectos";
        }
        catch (Exception ex)
        {
            LblError.Visible = true;
            LblError.Text = ex.Message;
        }
    }
}