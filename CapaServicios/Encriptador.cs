using System;
using System.Security.Cryptography;
using System.Text;
namespace CapaServicios
{
    public static class Encriptador
    {
        public static void ValidarPassword(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length > 128) throw new ArgumentException("Use una frase de al menos 15 caracteres y hasta 128 unidades de texto.");
            int caracteres = 0;
            for (int i=0; i<value.Length; i++)
            {
                if (char.IsControl(value[i])) throw new ArgumentException("La contraseña contiene caracteres de control.");
                if (char.IsSurrogate(value[i]))
                {
                    if (!char.IsSurrogatePair(value,i)) throw new ArgumentException("Texto Unicode inválido.");
                    i++;
                }
                caracteres++;
            }
            if (caracteres < 15) throw new ArgumentException("Use una frase de al menos 15 caracteres.");
            foreach(string comun in new[]{"123456789012345","passwordpassword","password123456789","administrador123","qwertyuiopasdfgh"})
                if (value.Equals(comun,StringComparison.OrdinalIgnoreCase)) throw new ArgumentException("La contraseña es demasiado predecible.");
        }
        public static string Hash(string value)
        {
            ValidarPassword(value);
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create()) rng.GetBytes(salt);
            using (var kdf = new Rfc2898DeriveBytes(value, salt, 600000, HashAlgorithmName.SHA256))
                return "PBKDF2$600000$" + Convert.ToBase64String(salt) + "$" + Convert.ToBase64String(kdf.GetBytes(32));
        }
        public static object Hash(object password) { return Hash(Convert.ToString(password)); }
        public static bool Verify(string password, string stored)
        {
            if (password == null || password.Length > 128 || stored == null || stored.Length > 256) return false;
            try
            {
                var parts = stored.Split('$');
                int iterations;
                if (parts.Length != 4 || parts[0] != "PBKDF2" || !int.TryParse(parts[1], out iterations) || iterations < 600000 || iterations > 2000000) return false;
                byte[] salt = Convert.FromBase64String(parts[2]), expected = Convert.FromBase64String(parts[3]);
                if (salt.Length != 16 || expected.Length != 32) return false;
                using (var kdf = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
                {
                    byte[] actual = kdf.GetBytes(32); int difference = 0;
                    for (int i = 0; i < actual.Length; i++) difference |= actual[i] ^ expected[i];
                    return difference == 0;
                }
            }
            catch (FormatException) { return false; }
        }
        public static string GetMd5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                var result = new StringBuilder();
                foreach (byte value in md5.ComputeHash(Encoding.UTF8.GetBytes(input))) result.Append(value.ToString("x2"));
                return result.ToString(); // Compatibilidad de DV; no se utiliza para contraseñas.
            }
        }
        public static string Token()
        {
            byte[] bytes = new byte[24];
            using (var rng = RandomNumberGenerator.Create()) rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
        public static bool TokenValido(string token, string esperado)
        {
            if (string.IsNullOrEmpty(token) || token.Length > 128 || esperado == null || esperado.Length != 44) return false;
            string actual = TokenHash(token); int diferencia = actual.Length ^ esperado.Length;
            for (int i = 0; i < actual.Length; i++) diferencia |= actual[i] ^ esperado[i];
            return diferencia == 0;
        }
        public static string TokenHash(string value)
        {
            using (var sha = SHA256.Create()) return Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(value)));
        }
    }
}
