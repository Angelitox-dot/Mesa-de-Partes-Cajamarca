using System;

namespace validaciones
{
    public class Program
    {
        // Método Main de prueba para ejecutar el proyecto
        public static void Main(string[] args)
        {
            Console.WriteLine("--- PRUEBAS DEL MÓDULO VALIDACIONES ---");
            try
            {
                string res1 = ValidarCodigoExpediente(" exp-001 ");
                Console.WriteLine($"Prueba 1 Exitosa -> Resultado: {res1}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Prueba 1 Fallida -> Error: {ex.Message}");
            }

            try
            {
                ValidarCodigoExpediente("123-EXP");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Prueba 2 Capturada con éxito -> Error: {ex.Message}");
            }

            Console.WriteLine("\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }

        // Métodos de Validación de Cadenas (Tema 4.2 y Manejo de Excepciones)
        public static string ValidarCodigoExpediente(string texto)
        {
            if (string.IsNullOrEmpty(texto))
                throw new Exception("El código no puede estar vacío.");

            string codigoLimpiado = texto.Trim().ToUpper(); // Manipulación de cadenas: limpiar espacios y mayúsculas
            string[] partes = codigoLimpiado.Split('-');     // Descomposición de cadenas mediante split

            if (partes.Length != 2 || partes[0] != "EXP" || partes[1].Length != 3)
                throw new Exception("Formato inválido. Debe ser EXP-### (Ej: EXP-001).");

            foreach (char c in partes[1])
            {
                if (!char.IsDigit(c))
                    throw new Exception("La parte numérica del código debe contener solo dígitos.");
            }

            return codigoLimpiado;
        }

        public static string ValidarDocumento(string doc)
        {
            if (string.IsNullOrEmpty(doc))
                throw new Exception("El documento no puede estar vacío.");

            string docLimpio = doc.Trim();

            if (docLimpio.Length != 8 && docLimpio.Length != 11)
                throw new Exception("El documento debe tener 8 dígitos (DNI) u 11 dígitos (RUC).");

            foreach (char c in docLimpio)
            {
                if (!char.IsDigit(c))
                    throw new Exception("El documento solo debe contener números.");
            }

            return docLimpio;
        }
    }
}