using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //cargo dos listas en la session para trabajar
            Session["_listaC"] = N_Logica.LogicaEntidades.ListarEntidades();
            Session["_listaS"] = new List<N_EntidadesCompartidas.Entidades>();

            //cargo las dos grillas
            EntidadesCompleto.DataSource = (List<N_EntidadesCompartidas.Entidades>)Session["_listaC"];
            EntidadesCompleto.DataBind();
            EntidadesSelecion.DataSource = (List<N_EntidadesCompartidas.Entidades>)Session["_listaS"];
            EntidadesSelecion.DataBind();
        }
    }

    protected void btnVer_Click(object sender, EventArgs e)
    {
        try
        {
            if (EntidadesCompleto.SelectedIndex >= 0)
            {
                List<N_EntidadesCompartidas.Entidades> _Milista = (List<N_EntidadesCompartidas.Entidades>)Session["_listaC"];
                N_EntidadesCompartidas.Entidades entd = _Milista[EntidadesCompleto.SelectedIndex];
                lblerror.Text += "Entidad selecionada es " + entd.ToString();
            }
        }
        catch (Exception ex)
        {
            lblerror.Text = ex.Message;
        }

    }

    protected void btnMostrarTramites_Click(object sender, EventArgs e)
    {

        try
        {
            if (EntidadesCompleto.SelectedIndex >= 0)
            {
                List<N_EntidadesCompartidas.Entidades> _Milista = (List<N_EntidadesCompartidas.Entidades>)Session["_listaC"];
                N_EntidadesCompartidas.Entidades entd = _Milista[EntidadesCompleto.SelectedIndex];
                //lblerror.Text += "Entidad selecionada es " + entd.ToString();
                string NombreEntidadSel = (entd.NombreEntidad).ToString();
                //List<N_EntidadesCompartidas.Tramites> _MilistaTramites = N_Logica.LogicaTramites.ListarTramites(NombreEntidadSel);
                // N_EntidadesCompartidas.Tramites listarTramites = N_Logica.LogicaTramites.ListarTramites(NombreEntidadSel);
                //  EntidadesSelecion.DataSource = listarTramites.TipoTramite;
                // EntidadesSelecion.DataBind();

                EntidadesSelecion.DataSource = N_Logica.LogicaTramites.ListarTramites(NombreEntidadSel);
                EntidadesSelecion.DataBind();






                //lblerror.Text += "Entidad selecionada es " +NombreEntidadSel ;
                lblerror.Text += "Entidad selecionada es " + NombreEntidadSel;
            }
        }
        catch (Exception ex)
        {
            lblerror.Text = ex.Message;
        }

    }
}