using System;
using System.Reflection;
using System.Collections;
using CapaAplicacion;
class PruebasBloqueo
{
 static int n;
 static void Check(bool x,string m){if(!x)throw new Exception(m);n++;Console.WriteLine("OK: "+m);}
 static int Main(){try{
  IntentosLogin.Limpiar("prueba");IntentosLogin.Validar("prueba");Check(true,"acceso inicial sin bloqueo");
  for(int i=0;i<4;i++)IntentosLogin.Fallar("prueba");IntentosLogin.Validar("prueba");Check(true,"cuatro fallos no bloquean");
  IntentosLogin.Fallar("prueba");bool bloqueo=false;try{IntentosLogin.Validar(" PRUEBA ");}catch(AccesoTemporalmenteBloqueadoException){bloqueo=true;}Check(bloqueo,"cinco fallos producen bloqueo específico con usuario normalizado");
  IntentosLogin.Validar("otro");Check(true,"bloqueo no afecta a otra cuenta");
  var estados=(IDictionary)typeof(IntentosLogin).GetField("estados",BindingFlags.Static|BindingFlags.NonPublic).GetValue(null);var estado=estados["prueba"];estado.GetType().GetField("Hasta").SetValue(estado,DateTime.UtcNow.AddSeconds(-1));IntentosLogin.Validar("prueba");Check(true,"bloqueo vencido permite reintentar");
  IntentosLogin.Fallar("prueba");IntentosLogin.Validar("prueba");Check(true,"ventana vencida reinicia contador");
  IntentosLogin.Limpiar("prueba");Check(!estados.Contains("prueba"),"acceso correcto limpia contador");Console.WriteLine(n+" pruebas aprobadas. Sin SQL.");return 0;
 }catch(Exception e){Console.WriteLine(e);return 1;}}
}
