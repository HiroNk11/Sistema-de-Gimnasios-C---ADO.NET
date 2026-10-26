using LogIn;
using System;
using System.IO;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace CapaIU
{
    internal static class Program
    {
        internal static void Error(Exception error)
        {
            string id = Guid.NewGuid().ToString("N").Substring(0, 10);
            var sqlError = error as SqlException;
            string details = "";
            if(sqlError != null)
                foreach(SqlError item in sqlError.Errors) details += " SQL=" + item.Number + " Estado=" + item.State + " Clase=" + item.Class;
            if(error.InnerException != null) details += " Causa=" + error.InnerException.GetType().Name + " HResult=" + error.InnerException.HResult;
            try
            {
                string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BodySportGym", "logs");
                Directory.CreateDirectory(folder);
                // No registra mensajes SQL, parámetros, contraseñas ni contenido de correos.
                File.AppendAllText(Path.Combine(folder, DateTime.Today.ToString("yyyy-MM-dd") + ".log"), DateTime.Now.ToString("o") + " " + id + " " + error.GetType().FullName + details + Environment.NewLine + error.StackTrace + Environment.NewLine);
            }
            catch { }
            string message = error is ArgumentException || error is UnauthorizedAccessException || error is InvalidOperationException ? error.Message : "No se pudo completar la operación. Revise la conexión y la configuración. Referencia: " + id;
            if(sqlError != null)
            {
                string causa;
                string texto=sqlError.Message.ToLowerInvariant();
                if(texto.Contains("certificate") || texto.Contains("certificado") || texto.Contains("ssl") || texto.Contains("tls"))
                    causa="Falló la conexión cifrada con SQL Server. Revise el certificado de la instancia configurada; en LocalDB de pruebas puede usar TrustServerCertificate=True.";
                else if(sqlError.Number==4060)
                    causa="No se puede abrir la base configurada. Compruebe que DatabaseGym_Mejorado exista en esa instancia y que su usuario Windows tenga acceso.";
                else if(sqlError.Number==18456)
                    causa="SQL Server rechazó el inicio de sesión de Windows. Revise los permisos en la instancia configurada.";
                else if(sqlError.Number==201)
                    causa="La conexión con SQL Server funciona, pero una operación no envió un parámetro requerido. Actualice la aplicación; si persiste, informe esta referencia.";
                else if(sqlError.Number==1205)
                    causa="Otra operación coincidió con esta y SQL Server revirtió este intento. Actualice los datos y vuelva a intentarlo.";
                else if(sqlError.Number==208 || sqlError.Number==2812)
                    causa="La conexión funciona, pero faltan tablas o procedimientos. Aplique las migraciones sobre la base configurada.";
                else
                    causa="No se pudo conectar u operar con SQL Server. Revise la instancia LocalDB, la base configurada y las migraciones.";
                message=causa+Environment.NewLine+"Código SQL: "+sqlError.Number+". Referencia: "+id;
            }
            MessageBox.Show(message, "Body Sport Gym", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        [STAThread]
        static void Main()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (s, e) => Error(e.Exception);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try { Application.Run(new FrmLogin()); } catch (Exception e) { Error(e); }
        }
    }
}
