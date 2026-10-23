using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP
{
    internal class Program
    {
        // Arreglos paralelos para almacenar los datos del trámite documentario (Tema 4.3)
        static string[] codigos = new string[100];
        static string[] documentos = new string[100];
        static string[] remitentes = new string[100];
        static string[] asuntos = new string[100];
        static string[] areas = new string[100];
        static int totalRegistros = 0;

        static void Main(string[] args)
        {
            int opcion = 0;

            do
            {
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("   SISTEMA MESA DE PARTES DIGITAL (EP)   ");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Registrar nuevo expediente");
                Console.WriteLine("2. Consultar expediente por código");
                Console.WriteLine("3. Ordenar expedientes alfabéticamente (Burbuja)");
                Console.WriteLine("4. Listar todos los expedientes");
                Console.WriteLine("5. Salir del programa");
                Console.Write("Seleccione una opción (1-5): ");

                string entrada = Console.ReadLine();

                if (!int.TryParse(entrada, out opcion))
                {
                    Console.WriteLine("\nError: Ingrese un número válido.");
                    Console.WriteLine("Presione una tecla para continuar...");
                    Console.ReadKey();
                    continue;
                }

                switch (opcion)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("--- REGISTRO DE EXPEDIENTE ---");
                        try
                        {
                            Console.Write("Ingrese código (Ej: EXP-001): ");
                            string rawCod = Console.ReadLine();
                            string codValidado = ValidarCodigoExpediente(rawCod);

                            Console.Write("Ingrese DNI (8 dígitos) o RUC (11 dígitos): ");
                            string rawDoc = Console.ReadLine();
                            string docValidado = ValidarDocumento(rawDoc);

                            Console.Write("Ingrese remitente: ");
                            string remitente = Console.ReadLine();

                            Console.Write("Ingrese asunto: ");
                            string asunto = Console.ReadLine();

                            Console.Write("Ingrese área de destino: ");
                            string area = Console.ReadLine();

                            Registrar(codValidado, docValidado, remitente, asunto, area);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"\n[Error]: {ex.Message}");
                        }
                        break;

                    case 2:
                        Console.Clear();
                        Console.WriteLine("--- CONSULTAR EXPEDIENTE ---");
                        Console.Write("Ingrese el código a buscar: ");
                        string busqueda = Console.ReadLine().Trim().ToUpper();

                        int indice = BuscarSecuencial(busqueda);
                        if (indice != -1)
                        {
                            Console.WriteLine("\n¡EXPEDIENTE ENCONTRADO!");
                            Console.WriteLine($"Código    : {codigos[indice]}");
                            Console.WriteLine($"Documento : {documentos[indice]}");
                            Console.WriteLine($"Remitente : {remitentes[indice]}");
                            Console.WriteLine($"Asunto    : {asuntos[indice]}");
                            Console.WriteLine($"Área      : {areas[indice]}");
                        }
                        else
                        {
                            Console.WriteLine("\n[X] No se encontró ningún registro con ese código.");
                        }
                        break;

                    case 3:
                        Console.Clear();
                        Console.WriteLine("--- ORDENAMIENTO ---");
                        OrdenarPorCodigos();
                        break;

                    case 4:
                        Listar();
                        break;

                    case 5:
                        Console.WriteLine("\nSaliendo del programa. ¡Mucho éxito mañana!");
                        break;

                    default:
                        Console.WriteLine("\nOpción no válida. Elija entre 1 y 5.");
                        break;
                }

                if (opcion != 5)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                }

            } while (opcion != 5);
        }

        // Métodos de Validación de Cadenas (Tema 4.2)
        public static string ValidarCodigoExpediente(string texto)
        {
            if (string.IsNullOrEmpty(texto))
                throw new Exception("El código no puede estar vacío.");

            string codigoLimpiado = texto.Trim().ToUpper();
            string[] partes = codigoLimpiado.Split('-');

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

        // Métodos de Gestión con Arreglos Paralelos y Burbuja (Temas 3.2, 4.3, 4.4)
        public static void Registrar(string cod, string doc, string rem, string asu, string are)
        {
            if (totalRegistros >= 100)
            {
                Console.WriteLine("Error: Capacidad máxima del sistema alcanzada.");
                return;
            }

            int pos = BuscarSecuencial(cod);
            if (pos != -1)
            {
                Console.WriteLine($"Error: El código {cod} ya se encuentra registrado.");
                return;
            }

            codigos[totalRegistros] = cod;
            documentos[totalRegistros] = doc;
            remitentes[totalRegistros] = rem;
            asuntos[totalRegistros] = asu;
            areas[totalRegistros] = are;

            totalRegistros++;
            Console.WriteLine("¡Expediente registrado con éxito!");
        }

        public static int BuscarSecuencial(string codigoBuscado)
        {
            for (int i = 0; i < totalRegistros; i++)
            {
                if (codigos[i] == codigoBuscado)
                    return i;
            }
            return -1;
        }

        public static void OrdenarPorCodigos()
        {
            if (totalRegistros <= 1)
            {
                Console.WriteLine("No hay suficientes registros para ordenar.");
                return;
            }

            for (int i = 0; i < totalRegistros - 1; i++)
            {
                for (int j = 0; j < totalRegistros - i - 1; j++)
                {
                    if (codigos[j].CompareTo(codigos[j + 1]) > 0)
                    {
                        string tempCod = codigos[j];
                        codigos[j] = codigos[j + 1];
                        codigos[j + 1] = tempCod;

                        string tempDoc = documentos[j];
                        documentos[j] = documentos[j + 1];
                        documentos[j + 1] = tempDoc;

                        string tempRem = remitentes[j];
                        remitentes[j] = remitentes[j + 1];
                        remitentes[j + 1] = tempRem;

                        string tempAsu = asuntos[j];
                        asuntos[j] = asuntos[j + 1];
                        asuntos[j + 1] = tempAsu;

                        string tempAre = areas[j];
                        areas[j] = areas[j + 1];
                        areas[j + 1] = tempAre;
                    }
                }
            }
            Console.WriteLine("¡Expedientes ordenados alfabéticamente por código con éxito!");
        }

        public static void Listar()
        {
            if (totalRegistros == 0)
            {
                Console.WriteLine("No hay expedientes registrados en el sistema.");
                return;
            }

            Console.WriteLine($"\n--- LISTADO DE EXPEDIENTES ({totalRegistros} en total) ---");
            for (int i = 0; i < totalRegistros; i++)
            {
                Console.WriteLine($"[{i + 1}] Código: {codigos[i]} | Doc: {documentos[i]} | Remitente: {remitentes[i]} | Área: {areas[i]}");
            }
        }
    }
}

