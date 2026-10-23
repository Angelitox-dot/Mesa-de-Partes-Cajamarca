using System;

namespace ordenamiento
{
    public class Program
    {
        // Método Main de prueba independiente
        public static void Main(string[] args)
        {
            Console.WriteLine("--- PRUEBAS DE BÚSQUEDA Y ORDENAMIENTO ---");
            string[] codigos = { "EXP-003", "EXP-001", "EXP-005" };
            string[] documentos = { "12345678", "87654321", "11223344" };
            string[] remitentes = { "Juan", "Maria", "Carlos" };
            string[] asuntos = { "Solicitud 1", "Solicitud 2", "Solicitud 3" };
            string[] areas = { "TI", "Legal", "Logistica" };
            int totalRegistros = 3;

            OrdenarPorCodigos(codigos, documentos, remitentes, asuntos, areas, totalRegistros);

            Console.WriteLine("\nCódigos ordenados:");
            for (int i = 0; i < totalRegistros; i++)
            {
                Console.WriteLine($"- {codigos[i]}");
            }

            Console.WriteLine("\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }

        // Métodos de Gestión con Arreglos Paralelos, Búsqueda y Burbuja (Temas 3.2, 4.3, 4.4)
        public static void Registrar(string[] codigos, string[] documentos, string[] remitentes, string[] asuntos, string[] areas, ref int totalRegistros, string cod, string doc, string rem, string asu, string are)
        {
            if (totalRegistros >= 100)
            {
                Console.WriteLine("Error: Capacidad máxima del sistema alcanzada.");
                return;
            }

            int pos = BuscarSecuencial(codigos, totalRegistros, cod);
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

        public static int BuscarSecuencial(string[] codigos, int totalRegistros, string codigoBuscado)
        {
            for (int i = 0; i < totalRegistros; i++)
            {
                if (codigos[i] == codigoBuscado)
                    return i;
            }
            return -1;
        }

        public static void OrdenarPorCodigos(string[] codigos, string[] documentos, string[] remitentes, string[] asuntos, string[] areas, int totalRegistros)
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
                        string tempCod = codigos[j]; codigos[j] = codigos[j + 1]; codigos[j + 1] = tempCod;
                        string tempDoc = documentos[j]; documentos[j] = documentos[j + 1]; documentos[j + 1] = tempDoc;
                        string tempRem = remitentes[j]; remitentes[j] = remitentes[j + 1]; remitentes[j + 1] = tempRem;
                        string tempAsu = asuntos[j]; asuntos[j] = asuntos[j + 1]; asuntos[j + 1] = tempAsu;
                        string tempAre = areas[j]; areas[j] = areas[j + 1]; areas[j + 1] = tempAre;
                    }
                }
            }
            Console.WriteLine("¡Expedientes ordenados alfabéticamente por código con éxito!");
        }
    }
}