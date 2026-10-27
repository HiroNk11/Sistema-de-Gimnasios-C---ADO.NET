using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaAplicacion;
using CapaDominio;
using CapaIU.Controles;
using CapaServicios.Composite;
namespace CapaIU
{
    public sealed class FrmAltaUsuarios : PantallaGestion
    {
        readonly bool preview;
        readonly TextBox usuario=TemaVisual.Texto(50),nombre=TemaVisual.Texto(50),apellido=TemaVisual.Texto(50),correo=TemaVisual.Texto(254),clave=TemaVisual.Texto(128),confirmar=TemaVisual.Texto(128);
        readonly ComboBox perfil=TemaVisual.Lista();
        readonly Button crear=TemaVisual.Boton("Crear usuario",true);
        public FrmAltaUsuarios():this(false){}
        internal FrmAltaUsuarios(bool vistaPrevia)
        {
            preview=vistaPrevia;if(!preview)Autorizacion.ExigirCualquiera(TipoPermiso.PuedeCrearUsuarios,TipoPermiso.PuedeGestionarUsuarios);
            Text="Alta de usuarios";
            var scroll=new Panel {Dock=DockStyle.Fill,AutoScroll=true};Cuerpo.Controls.Add(scroll);Cuerpo.Controls.Add(Cabecera("Alta de usuarios","Creá cuentas individuales y asigná uno de los perfiles definidos."));
            var ficha=new TableLayoutPanel {Dock=DockStyle.Top,AutoSize=true,ColumnCount=2,Padding=new Padding(20),BackColor=Color.White};ficha.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,50));ficha.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,50));scroll.Controls.Add(ficha);
            perfil.DisplayMember="Key";perfil.ValueMember="Value";perfil.DataSource=new[]{new KeyValuePair<string,PerfilCuenta>("Recepción",PerfilCuenta.Recepcion),new KeyValuePair<string,PerfilCuenta>("Administración de cuentas",PerfilCuenta.AdministracionDeCuentas)};perfil.SelectedIndex=-1;
            clave.UseSystemPasswordChar=confirmar.UseSystemPasswordChar=true;
            ficha.Controls.Add(TemaVisual.Campo("Nombre de usuario",usuario),0,0);ficha.Controls.Add(TemaVisual.Campo("Perfil",perfil),1,0);ficha.Controls.Add(TemaVisual.Campo("Nombre",nombre),0,1);ficha.Controls.Add(TemaVisual.Campo("Apellido",apellido),1,1);ficha.Controls.Add(TemaVisual.Campo("Correo",correo),0,2);
            ficha.Controls.Add(TemaVisual.Campo("Clave inicial de 15 a 128 caracteres",clave),0,3);ficha.Controls.Add(TemaVisual.Campo("Repetir clave inicial",confirmar),1,3);
            var ayuda=TemaVisual.Etiqueta("La persona deberá cambiar la clave inicial al ingresar. Entregala por un canal privado. No se envían correos.",10);ayuda.MaximumSize=new Size(850,0);ficha.Controls.Add(ayuda,0,4);ficha.SetColumnSpan(ayuda,2);ficha.Controls.Add(crear,0,5);
            var regla=TemaVisual.Etiqueta("Administración solo crea cuentas. Recepción gestiona socios, membresías, cobros, actividades, horarios, reservas y asistencia.",10);regla.MaximumSize=new Size(850,0);regla.Margin=new Padding(0,20,0,0);ficha.Controls.Add(regla,0,6);ficha.SetColumnSpan(regla,2);
            crear.Enabled=!preview;crear.Click+=async(s,e)=>await Ejecutar(async()=>{
                if(preview)return;if(!(perfil.SelectedValue is PerfilCuenta))throw new ArgumentException("Seleccioná el perfil.");
                var u=new Usuario {usuario=usuario.Text,nombre=nombre.Text,apellido=apellido.Text,email=correo.Text};var rol=(PerfilCuenta)perfil.SelectedValue;string pass=clave.Text,repite=confirmar.Text;
                try{await Task.Run(()=>new CuentasBLL().Crear(u,rol,pass,repite));usuario.Clear();nombre.Clear();apellido.Clear();correo.Clear();perfil.SelectedIndex=-1;Aviso("Cuenta creada. Ya puede ingresar y cambiar su clave inicial.");}finally{clave.Clear();confirmar.Clear();pass=null;repite=null;}
            });
            Shown+=(s,e)=>perfil.SelectedIndex=-1;
            if(preview)Aviso("Vista previa sin escrituras ni datos reales.");
        }
    }
}
