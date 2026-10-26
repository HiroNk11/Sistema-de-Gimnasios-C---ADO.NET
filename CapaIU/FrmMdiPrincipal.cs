using CapaAplicacion;
using CapaAplicacion.Negocio;
using CapaDominio;
using CapaInterfaces.INTERFACES;
using CapaIU.Properties;
using CapaServicios;
using CapaServicios.Composite;
using CapaServicios.DV;
using LogIn;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaIU
{
    public partial class FrmMdiPrincipal : Form, IIdiomaObserver
    {
        UsuarioBLL _usuarioBLL;
        PermisosBLL permisosbll;
        TraductorBLL tradu;
        PermisoUsuario usu;
        BitacoraBLL bitbll;
        Bitacora bit;
        CapaIU.Presentacion.NavegadorMdi navegador;
        public FrmMdiPrincipal() : this(false) {}
        internal FrmMdiPrincipal(bool vistaPrevia)
        {
            InitializeComponent();
            navegador = new CapaIU.Presentacion.NavegadorMdi(this);
            if(vistaPrevia){negocioToolStripMenuItem.Available=true;toolStripSesion.Text="Vista previa · datos ficticios";PrepararNavegacion();return;}
            _usuarioBLL = new UsuarioBLL();
            permisosbll = new PermisosBLL();
            usu = new PermisoUsuario();
            tradu = new TraductorBLL();
            bitbll = new BitacoraBLL();
            ValidarForm();
            MostrarIdiomas();
            MarcarIdioma();
            Traducir();
            ValidarPermisos();
            var operacion=new ToolStripMenuItem("Operación diaria");
            operacion.Visible=negocioToolStripMenuItem.Available;
            operacion.Click+=(sender,e)=>{AbrirVentana<CapaIU.Negocio.FrmOperacionDiaria>(()=>new CapaIU.Negocio.FrmOperacionDiaria());};
            negocioToolStripMenuItem.DropDownItems.Add(operacion);
            PrepararNavegacion();
        }
        void AbrirVentana<T>(Func<T> crear) where T:Form
        {
            navegador.Abrir(crear);
        }
        void PrepararNavegacion()
        {
            Text="Body Sport Gym · Gestión";Font=CapaIU.Controles.TemaVisual.Fuente;MinimumSize=new Size(1180,720);
            crearClaseToolStripMenuItem.Available=false;
            gestionarClaseToolStripMenuItem.Text="Actividades";
            asignarHorarioToolStripMenuItem1.Text="Programar actividades";
            administrarClaseToolStripMenuItem.Text="Reservas y asistencia";
            verClasesSocioToolStripMenuItem.Text="Clases semanales del socio (histórico)";
            calendarioToolStripMenuItem.Text="Calendario semanal";
            menuStrip1.BackColor=Color.White;menuStrip1.ForeColor=CapaIU.Controles.TemaVisual.Tinta;menuStrip1.Font=new Font("Segoe UI",10);menuStrip1.Padding=new Padding(10,8,8,8);
            statusStrip1.BackColor=Color.White;statusStrip1.Font=new Font("Segoe UI",9);statusStrip1.Padding=new Padding(12,4,12,4);
            var lateral=new FlowLayoutPanel {Dock=DockStyle.Left,Width=206,BackColor=CapaIU.Controles.TemaVisual.Oscuro,FlowDirection=FlowDirection.TopDown,WrapContents=false,Padding=new Padding(16,28,16,12)};
            var marca=new Label {Text="BODY SPORT",Font=new Font("Segoe UI",17,FontStyle.Bold),ForeColor=Color.White,AutoSize=true,Margin=new Padding(2,0,0,4)};lateral.Controls.Add(marca);
            lateral.Controls.Add(new Label {Text="GESTIÓN DEL GIMNASIO",Font=new Font("Segoe UI",8),ForeColor=Color.FromArgb(149,178,199),AutoSize=true,Margin=new Padding(3,0,0,34)});
            Action abrirSocios=()=>AbrirVentana<CapaIU.Negocio.FrmSocios>(()=>new CapaIU.Negocio.FrmSocios());
            Action abrirOperacion=()=>AbrirVentana<CapaIU.Negocio.FrmOperacionDiaria>(()=>new CapaIU.Negocio.FrmOperacionDiaria());
            Action abrirCalendario=()=>AbrirVentana<Negocio.FrmCalendarioSemanal>(()=>new Negocio.FrmCalendarioSemanal());
            Action<int> abrirCobros=id=>{
                var existente=MdiChildren.OfType<Negocio.FrmOperacionDiaria>().FirstOrDefault(f=>!f.IsDisposed);
                if(existente==null)navegador.Abrir(()=>new Negocio.FrmOperacionDiaria(id));
                else{navegador.Abrir(()=>existente);existente.AbrirCobrosSocio(id);}
            };
            Action abrirSaldos=()=>AbrirVentana<Negocio.FrmSaldosPendientes>(()=>new Negocio.FrmSaldosPendientes(abrirCobros));
            bool vistaPrevia=_usuarioBLL==null;
            Action inicio=()=>AbrirVentana<FrmInicio>(()=>new FrmInicio(abrirSocios,abrirOperacion,negocioToolStripMenuItem.Available,mnuges.Available?(Action)(()=>AbrirVentana<FrmAltaUsuarios>(()=>new FrmAltaUsuarios())):null,abrirCalendario,abrirSaldos,abrirCobros,vistaPrevia));
            Action<string,Action,bool> boton=(texto,accion,disponible)=>{
                var b=new Button {Text=texto,Width=174,Height=43,FlatStyle=FlatStyle.Flat,BackColor=CapaIU.Controles.TemaVisual.Oscuro,ForeColor=Color.White,Font=new Font("Segoe UI",10),TextAlign=ContentAlignment.MiddleLeft,Cursor=Cursors.Hand,Margin=new Padding(0,3,0,5),Visible=disponible};
                b.FlatAppearance.BorderSize=0;b.FlatAppearance.MouseOverBackColor=Color.FromArgb(38,65,88);b.Click+=(s,e)=>accion();
                MdiChildActivate+=(s,e)=>{bool activa=ActiveMdiChild!=null && ((texto=="Inicio" && ActiveMdiChild is FrmInicio) || (texto=="Socios" && ActiveMdiChild is CapaIU.Negocio.FrmSocios) || (texto=="Entrenadores" && ActiveMdiChild is CapaIU.Negocio.FrmEntrenadores) || (texto=="Operación diaria" && ActiveMdiChild is CapaIU.Negocio.FrmOperacionDiaria) || (texto=="Actividades" && ActiveMdiChild is CapaIU.Negocio.FrmActividades) || (texto=="Horarios" && ActiveMdiChild is CapaIU.Negocio.FrmPlanificacion));b.BackColor=activa?CapaIU.Controles.TemaVisual.Primario:CapaIU.Controles.TemaVisual.Oscuro;};
                lateral.Controls.Add(b);
            };
            boton("Usuarios",()=>AbrirVentana<FrmAltaUsuarios>(()=>new FrmAltaUsuarios()),mnuges.Available);
            boton("Inicio",inicio,true);boton("Operación diaria",abrirOperacion,negocioToolStripMenuItem.Available);boton("Socios",abrirSocios,negocioToolStripMenuItem.Available);
            boton("Entrenadores",()=>gestionarEntrenadorToolStripMenuItem_Click(this,EventArgs.Empty),negocioToolStripMenuItem.Available);
            boton("Actividades",()=>gestionarClaseToolStripMenuItem_Click(this,EventArgs.Empty),negocioToolStripMenuItem.Available);
            boton("Horarios",()=>AbrirVentana<Negocio.FrmPlanificacion>(()=>new Negocio.FrmPlanificacion()),negocioToolStripMenuItem.Available);
            boton("Calendario semanal",abrirCalendario,negocioToolStripMenuItem.Available);
            boton("Saldos pendientes",abrirSaldos,negocioToolStripMenuItem.Available);
            Controls.Add(lateral);lateral.BringToFront();
            foreach(Control c in Controls)if(c is MdiClient)c.BackColor=CapaIU.Controles.TemaVisual.Fondo;
            Shown+=(s,e)=>inicio();MdiChildActivate+=(s,e)=>{if(ActiveMdiChild!=null && !(ActiveMdiChild is FrmInicio))ActiveMdiChild.WindowState=FormWindowState.Maximized;};
        }
        public void ValidarPermisos()
        {
            var actual = SingletonSesion.Instancia.Usuario;
            if(actual == null) throw new InvalidOperationException("Inicie sesión para continuar.");
            usu = new PermisoUsuario { idusuario = actual.idusuario, Nombre = actual.usuario };
            mnuidioma.Visible = permisosbll.BuscarPermisos(TipoPermiso.PuedeCambiarIdioma, usu);
            gestionarIdiomaToolStripMenuItem.Visible = permisosbll.BuscarPermisos(TipoPermiso.PuedeGestionarIdioma, usu);
            mnufyp.Visible = permisosbll.BuscarPermisos(TipoPermiso.PuedeConfigurarSeguridadPatentesyFamilias, usu);
            mnuusuario.Visible = permisosbll.BuscarPermisos(TipoPermiso.PuedeConfigurarSeguridadUsuarios, usu);
            mnuseg.Visible = mnufyp.Available || mnuusuario.Available;
            mnuges.Visible = permisosbll.BuscarPermisos(TipoPermiso.PuedeGestionarUsuarios, usu) || permisosbll.BuscarPermisos(TipoPermiso.PuedeCrearUsuarios, usu);
            negocioToolStripMenuItem.Available = permisosbll.BuscarPermisos(TipoPermiso.PuedeAccederANegocio, usu);
            backupRestoreToolStripMenuItem.Visible = permisosbll.BuscarPermisos(TipoPermiso.PuedeAccederBackupRestore, usu) || permisosbll.BuscarPermisos(TipoPermiso.PuedeAccederBackup, usu) || permisosbll.BuscarPermisos(TipoPermiso.PuedeAccederRestore, usu);
            restaurarDVToolStripMenuItem.Visible = permisosbll.BuscarPermisos(TipoPermiso.PuedeRestaurardv, usu);
            bitacoraToolStripMenuItem.Visible = permisosbll.BuscarPermisos(TipoPermiso.PuedeAccederBitacora, usu);
        }
        private void itemLogout_Click(object sender, EventArgs e)
        {
            if(MdiChildren.Any(f=>f.UseWaitCursor)){MessageBox.Show(this,"Esperá a que termine la operación antes de cambiar de usuario.","Operación en curso",MessageBoxButtons.OK,MessageBoxIcon.Information);return;}
            if (MessageBox.Show("¿Está seguro?", "Confirme", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string bit = "Cierre de Sesion de : " + SingletonSesion.Instancia.Usuario.usuario;
                GrabarBitacora(bit);
                foreach(Form child in MdiChildren)child.Close();
                _usuarioBLL.Logout();
                ValidarForm();
                FrmLogin frm = new FrmLogin();
                frm.Show();
                this.Hide();
            }
        }
        public void GrabarBitacora(string detallebit)
        {
            bit = new Bitacora();
            bit.id_usuario = SingletonSesion.Instancia.Usuario.idusuario;
            bit.actividad = detallebit;
            bit.criticidad = "Baja";
            bit.fecha = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
            bit.hora = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
            bitbll.InsertarEvento(bit);
            bit = null;
        }
        public void ValidarForm()
        {
            this.mnulogut.Enabled = SingletonSesion.Instancia.IsLogged();
            this.mnuges.Enabled = SingletonSesion.Instancia.IsLogged();
            if (SingletonSesion.Instancia.IsLogged())
                this.toolStripSesion.Text = SingletonSesion.Instancia.Usuario.usuario;
            else
                this.toolStripSesion.Text = "[Sesión no iniciada]";
            MarcarIdioma();
            Traducir();
        }
        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
                AbrirVentana<FrmAltaUsuarios>(() => new FrmAltaUsuarios());     
        }
        private void familiasYPatentesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirVentana<Seguridad.FrmPatentesYFamilias>(() => new Seguridad.FrmPatentesYFamilias());
        }

        private void usuariosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
                AbrirVentana<Seguridad.FrmSegUsuarios>(() => new Seguridad.FrmSegUsuarios());
        }
        private void FrmMdiPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }    
        private void FrmMdiPrincipal_Load(object sender, EventArgs e)
        {
            
            SingletonSesion.Instancia.SuscribirObservador(this);
        }

        private void FrmMdiPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            SingletonSesion.Instancia.DesuscribirObservador(this);
        }

        public void UpdateLanguage(IIdioma idioma)
        {
            MarcarIdioma();
            Traducir();
        }
        void MarcarIdioma()
        {

            if (!SingletonSesion.Instancia.IsLogged()) // si no esta logueado
            {
                foreach (var item in MnuIdiomas.DropDownItems) // por cada item en mnuidiomas. items desplegables
                {

                    var i = ((IIdioma)((ToolStripMenuItem)item).Tag);
                    ((ToolStripMenuItem)item).Checked = i.Default;
                    ((ToolStripMenuItem)item).Enabled = false;

                }
            }
            else
            {
                foreach (var item in MnuIdiomas.DropDownItems)
                {
               
                    ((ToolStripMenuItem)item).Enabled = true;
                    
                }
            }
        }
        public void MostrarIdiomas()
        {
            var idiomas = tradu.ObtenerIdiomas();
            // variable idiomas es igual al metodo obtener idiomas del traductor
            // // se hace un select * de la tabla idiomas
            // por cada item en idiomas creamos un objeto toolstripmenuitem y al texto le asignamos nombre y etiqueta
            // a la lista despegable le agregamos este objeto
          this.MnuIdiomas.DropDownItems.Clear();
            foreach (var item in idiomas)
            {             
                var t = new ToolStripMenuItem();
                t.Text = item.Nombre;
                t.Tag = item;
                t.Click += idioma_Click;
                this.MnuIdiomas.DropDownItems.Add(t);  
            }

        }

        public void idioma_Click(object sender, EventArgs e)
        {
            SingletonSesion.Instancia.CambiarIdioma(((IIdioma)((ToolStripMenuItem)sender).Tag));
            MarcarIdioma();
        }

        private void Traducir()
        {
            IIdioma idioma = null; // instancio un objeto de la interfaz iidioma 
            if (SingletonSesion.Instancia.IsLogged()) // si el usuario esta logeado
                idioma = SingletonSesion.Instancia.Usuario.Idioma; // el objeto idioma va a ser igual a la instancia idioma del usuario

            // creo variable tradduciones y la igualo al metodo obtener traducciones de la clase Traductor
            // y le paso como parametro el objeto creado idioma
            var traducciones = tradu.ObtenerTraducciones(idioma);

            if (mnuSesion.Tag != null && traducciones.ContainsKey(mnuSesion.Tag.ToString()))
                this.mnuSesion.Text = traducciones[mnuSesion.Tag.ToString()].Texto;
            // si la etiqueta de mnu session no es nula y además traducciones e traducciones contiene la llave del texto 
            // igual al texto de la etiqueta

            if (cambiarContraseñaToolStripMenuItem.Tag != null && traducciones.ContainsKey(cambiarContraseñaToolStripMenuItem.Tag.ToString()))
                this.cambiarContraseñaToolStripMenuItem.Text = traducciones[cambiarContraseñaToolStripMenuItem.Tag.ToString()].Texto;

            if (mnulogut.Tag != null && traducciones.ContainsKey(mnulogut.Tag.ToString()))
                this.mnulogut.Text = traducciones[mnulogut.Tag.ToString()].Texto;
    
            if (mnuidioma.Tag != null && traducciones.ContainsKey(mnuidioma.Tag.ToString()))
                this.mnuidioma.Text = traducciones[mnuidioma.Tag.ToString()].Texto;

            if (MnuIdiomas.Tag != null && traducciones.ContainsKey(MnuIdiomas.Tag.ToString()))
                this.MnuIdiomas.Text = traducciones[MnuIdiomas.Tag.ToString()].Texto;

            if (gestionarIdiomaToolStripMenuItem.Tag != null && traducciones.ContainsKey(gestionarIdiomaToolStripMenuItem.Tag.ToString()))
                this.gestionarIdiomaToolStripMenuItem.Text = traducciones[gestionarIdiomaToolStripMenuItem.Tag.ToString()].Texto;

            if (actualizarListaIdiomasToolStripMenuItem.Tag != null && traducciones.ContainsKey(actualizarListaIdiomasToolStripMenuItem.Tag.ToString()))
                this.actualizarListaIdiomasToolStripMenuItem.Text = traducciones[actualizarListaIdiomasToolStripMenuItem.Tag.ToString()].Texto;


            if (mnuseg.Tag != null && traducciones.ContainsKey(mnuseg.Tag.ToString()))
                this.mnuseg.Text = traducciones[mnuseg.Tag.ToString()].Texto;

            if (mnufyp.Tag != null && traducciones.ContainsKey(mnufyp.Tag.ToString()))
                this.mnufyp.Text = traducciones[mnufyp.Tag.ToString()].Texto;

            if(mnuusuario.Tag != null && traducciones.ContainsKey(mnuusuario.Tag.ToString()))
                this.mnuusuario.Text = traducciones[mnuusuario.Tag.ToString()].Texto;

            if (mnuges.Tag != null && traducciones.ContainsKey(mnuges.Tag.ToString()))
                this.mnuges.Text = traducciones[mnuges.Tag.ToString()].Texto;

            if (mnugesusu.Tag != null && traducciones.ContainsKey(mnugesusu.Tag.ToString()))
                this.mnugesusu.Text = traducciones[mnugesusu.Tag.ToString()].Texto;


            if (negocioToolStripMenuItem.Tag != null && traducciones.ContainsKey(negocioToolStripMenuItem.Tag.ToString()))
                this.negocioToolStripMenuItem.Text = traducciones[negocioToolStripMenuItem.Tag.ToString()].Texto;

            if (socioToolStripMenuItem.Tag != null && traducciones.ContainsKey(socioToolStripMenuItem.Tag.ToString()))
                this.socioToolStripMenuItem.Text = traducciones[socioToolStripMenuItem.Tag.ToString()].Texto;

            if (entrenadorToolStripMenuItem.Tag != null && traducciones.ContainsKey(entrenadorToolStripMenuItem.Tag.ToString()))
                this.entrenadorToolStripMenuItem.Text = traducciones[entrenadorToolStripMenuItem.Tag.ToString()].Texto;
            
            if (claseToolStripMenuItem.Tag != null && traducciones.ContainsKey(claseToolStripMenuItem.Tag.ToString()))
                this.claseToolStripMenuItem.Text = traducciones[claseToolStripMenuItem.Tag.ToString()].Texto;

            if (gestionarSocioToolStripMenuItem.Tag != null && traducciones.ContainsKey(gestionarSocioToolStripMenuItem.Tag.ToString()))
                this.gestionarSocioToolStripMenuItem.Text = traducciones[gestionarSocioToolStripMenuItem.Tag.ToString()].Texto;

        


            if (gestionarEntrenadorToolStripMenuItem.Tag != null && traducciones.ContainsKey(gestionarEntrenadorToolStripMenuItem.Tag.ToString()))
                this.gestionarEntrenadorToolStripMenuItem.Text = traducciones[gestionarEntrenadorToolStripMenuItem.Tag.ToString()].Texto;

            if (crearClaseToolStripMenuItem.Tag != null && traducciones.ContainsKey(crearClaseToolStripMenuItem.Tag.ToString()))
                this.crearClaseToolStripMenuItem.Text = traducciones[crearClaseToolStripMenuItem.Tag.ToString()].Texto;

            if (gestionarEntrenamientoToolStripMenuItem.Tag != null && traducciones.ContainsKey(gestionarEntrenamientoToolStripMenuItem.Tag.ToString()))
                this.gestionarEntrenamientoToolStripMenuItem.Text = traducciones[gestionarEntrenamientoToolStripMenuItem.Tag.ToString()].Texto;

            if (gestionarEntrenadorToolStripMenuItem.Tag != null && traducciones.ContainsKey(gestionarEntrenadorToolStripMenuItem.Tag.ToString()))
                this.gestionarEntrenadorToolStripMenuItem.Text = traducciones[gestionarEntrenadorToolStripMenuItem.Tag.ToString()].Texto;

           
            if (verClasesSocioToolStripMenuItem.Tag != null && traducciones.ContainsKey(verClasesSocioToolStripMenuItem.Tag.ToString()))
                this.verClasesSocioToolStripMenuItem.Text = traducciones[verClasesSocioToolStripMenuItem.Tag.ToString()].Texto;

            if (entrenamientoToolStripMenuItem.Tag != null && traducciones.ContainsKey(entrenamientoToolStripMenuItem.Tag.ToString()))
                this.entrenamientoToolStripMenuItem.Text = traducciones[entrenamientoToolStripMenuItem.Tag.ToString()].Texto;

            if (gestionarEntrenamientoToolStripMenuItem.Tag != null && traducciones.ContainsKey(gestionarEntrenamientoToolStripMenuItem.Tag.ToString()))
                this.gestionarEntrenamientoToolStripMenuItem.Text = traducciones[gestionarEntrenamientoToolStripMenuItem.Tag.ToString()].Texto;

            if (crearClaseToolStripMenuItem.Tag != null && traducciones.ContainsKey(crearClaseToolStripMenuItem.Tag.ToString()))
                this.crearClaseToolStripMenuItem.Text = traducciones[crearClaseToolStripMenuItem.Tag.ToString()].Texto;

           
          
            // entonces el texto del menu va a ser igual a lo que hay en traducciones 

            if (traducciones.ContainsKey("username"))
                    this.toolStripSesion.Text = $"{traducciones["username"].Texto}: {SingletonSesion.Instancia.Usuario}";
                MarcarIdioma();
            

        }
        private void gestionarIdiomaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirVentana<Seguridad.FrmGestionarIdioma>(() => new Seguridad.FrmGestionarIdioma());

        }

        private void actualizarListaIdiomasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MostrarIdiomas();
        }

        private void cambiarContraseñaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirVentana<FrmCambiarContraseña>(() => new FrmCambiarContraseña());
        }







        private void gestionarEntrenadorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirVentana<Negocio.FrmEntrenadores>(() => new Negocio.FrmEntrenadores());
        }

        private void gestionarSocioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirVentana<CapaIU.Negocio.FrmSocios>(()=>new CapaIU.Negocio.FrmSocios());
        }

        private void verClasesSocioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirVentana<Negocio.FrmVerClasesSocio>(() => new Negocio.FrmVerClasesSocio());
        }

        private void gestionarEntrenamientoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirVentana<Negocio.FrmEntrenamiento>(() => new Negocio.FrmEntrenamiento());
        }

        private void crearClaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirVentana<Negocio.FrmActividades>(() => new Negocio.FrmActividades());
        }
        private void backupRestoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirVentana<FrmBackupRestore>(() => new FrmBackupRestore());
        }

    

        private void bitacoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirVentana<FrmBitacora>(() => new FrmBitacora());
        }

        private void gestionarClaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirVentana<Negocio.FrmActividades>(() => new Negocio.FrmActividades());
        }



        private void administrarClaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirVentana<Negocio.FrmOperacionDiaria>(() => new Negocio.FrmOperacionDiaria());
        }

        private void asignarHorarioToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AbrirVentana<Negocio.FrmPlanificacion>(() => new Negocio.FrmPlanificacion());
        }

        private void calendarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            navegador.Abrir(()=>new Negocio.FrmCalendarioSemanal());
        }

        private void FrmMdiPrincipal_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            string manual=System.IO.Path.Combine(Application.StartupPath,"ManualUsuario.pdf");
            if(System.IO.File.Exists(manual))System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(manual){UseShellExecute=true});
            else MessageBox.Show(this,"No se encontró ManualUsuario.pdf junto al programa. Consultá el manual entregado con el proyecto.","Manual de usuario",MessageBoxButtons.OK,MessageBoxIcon.Information);
            hlpevent.Handled=true;
        }

        private void restaurarDVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea restaurar los Digitos Verificadores?", "Confirme", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _usuarioBLL.RepararDigitos();
                _usuarioBLL.ActualizarDigitos();
            }
        }

        private void verDetalleClaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirVentana<Negocio.FrmVerDetalle>(() => new Negocio.FrmVerDetalle());
        }
    }
}
