using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ArchivosBinarios
{
    class Program
    {
        //creacion de clase
        public class ArchivoBinarioEmpleados
        {
            //declaracionde flujos
            BinaryWriter bw = null;
            BinaryReader br = null;

            //campos de la clase 
            string nombre, direccion;
            long telefono;
            int numEmp, diasTrabajados;
            float salarioDiario;

            public void CrearArchivo(string Archivo)
            {
                char resp;

                try
                {
                    //creacion de flujo para escribir datos al archivo
                    bw = new BinaryWriter(new FileStream(Archivo, FileMode.Create, FileAccess.Write));

                    //captura de datos 
                    do
                    {
                        Console.Clear();
                        Console.Write("Numero del empleado: ");
                        numEmp = Int32.Parse(Console.ReadLine());
                        Console.Write("Nombre del empleado: ");
                        nombre = Console.ReadLine();
                        Console.Write("Direccion del empleado: ");
                        direccion = Console.ReadLine();
                        Console.Write("Telefono del empleado: ");
                        telefono = Int64.Parse(Console.ReadLine());
                        Console.Write("Dias trabajados del empleado: ");
                        diasTrabajados = int.Parse(Console.ReadLine());
                        Console.Write("Salario diario del empleado: ");
                        salarioDiario = Single.Parse(Console.ReadLine());

                        //Escribe los datos al archivo
                        bw.Write(numEmp);
                        bw.Write(nombre);
                        bw.Write(direccion);
                        bw.Write(telefono);
                        bw.Write(diasTrabajados);
                        bw.Write(salarioDiario);

                        Console.Write("\n\nDeseas almacenar otro registro (s/n?");

                        resp = Char.Parse(Console.ReadLine());
                    } while ((resp == 's') || (resp == 'S'));
                }
                catch (IOException e)
                {
                    Console.WriteLine("\nError: " + e.Message);
                    Console.WriteLine("\nRuta: " + e.StackTrace);
                }
                finally
                {
                    if (bw != null)
                    {
                        bw.Close();// cierra flujo de escritura
                    }
                    Console.Write("Presione <Enter> para terminar la escritura de datos  y regresar al menu");
                    Console.ReadKey();
                }
            }

            public void MostrarArchivo(string Archivo)
            {
                try
                {
                    //verifica si existe el archivo
                    if (File.Exists(Archivo))
                    {
                        //creacion flujo pra leer datos del archivo
                        br = new BinaryReader(new FileStream(Archivo, FileMode.Open, FileAccess.Read));

                        //desplieue de datos en pantalla
                        Console.Clear();
                        do
                        {
                            //lectura de registros mientras no lleguen a EndOfFile
                            numEmp = br.ReadInt32();
                            nombre = br.ReadString();
                            direccion = br.ReadString();
                            telefono = br.ReadInt64();
                            diasTrabajados = br.ReadInt32();
                            salarioDiario = br.ReadSingle();

                            //muestra los datos
                            Console.WriteLine("Numero del empleado: " + numEmp);
                            Console.WriteLine("Nombre del empleado: " + nombre);
                            Console.WriteLine("Direccion del empleado: " + direccion);
                            Console.WriteLine("Telefono del empleado: " + telefono);
                            Console.WriteLine("Dias trabajados del empleado: " + diasTrabajados);
                            Console.WriteLine("Salario diario del empleado: " + salarioDiario);

                            Console.WriteLine("SUELDO TOTAL DEL empleado: {0:C}");
                            Console.WriteLine("\n");
                        } while (true);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("\n\nEl Archivo " + Archivo + " No Existe en el Disco!!");

                        Console.Write("\nPresione <enter> para Continuar...");
                        Console.ReadKey();
                    }
                }
                catch (EndOfStreamException)
                {
                    Console.WriteLine("\n\nFin del Listado de Empleados");
                    Console.Write("\nPresione <enter> para Continuar...");
                    Console.ReadKey();
                }
                finally
                {
                    if (br != null) br.Close(); //cierra flujo
                    Console.Write("\nPresione <enter> para terminar la Lectura de Datos y regresar al Menu.");

                    Console.ReadKey();
                }
            }
        }
        static void Main(string[] args)
        {
            //declaración variables auxiliares
            string Arch = null;
            int opcion;

            //creación del objeto
            ArchivoBinarioEmpleados A1 = new ArchivoBinarioEmpleados();

            //Menu de Opciones
            do
            {
                Console.Clear();
                Console.WriteLine("\n*** ARCHIVO BINARIO EMPLEADOS***");
                Console.WriteLine("1.- Creación de un Archivo.");
                Console.WriteLine("2.- Lectura de un Archivo.");
                Console.WriteLine("3.- Salida del Programa.");
                Console.Write("\nQue opción deseas: ");
                opcion = Int16.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        //bloque de escritura
                        try
                        {
                            //captura nombre archivo
                            Console.Write("\nAlimenta el Nombre del Archivo a Crear: "); 
                            Arch = Console.ReadLine();
                            
                            //verifica si esxiste el archivo
                            char resp = 's';
                            if (File.Exists(Arch))
                            {
                                Console.Write("\nEl Archivo Existe!!, Deseas Sobreescribirlo(s / n) ? ");
                                resp = Char.Parse(Console.ReadLine());
                            }
                            if ((resp == 's') || (resp == 'S'))
                            {
                                A1.CrearArchivo(Arch);
                            }
                                
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("\nError: " + e.Message);
                            Console.WriteLine("\nRuta: " + e.StackTrace);
                        }
                        break;

                    case 2:
                        //bloque de lectura
                        try
                        {
                            //captura nombre archivo
                            Console.Write("\nAlimenta el Nombre del Archivo que deseas Leer: "); 
                            Arch = Console.ReadLine();
                            A1.MostrarArchivo(Arch);
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("\nError : " + e.Message);
                            Console.WriteLine("\nRuta : " + e.StackTrace);
                        }
                        break;

                    case 3:
                        Console.Write("\nPresione <enter> para Salir del Programa.");
                        Console.ReadKey();
                        break;

                    default:
                        Console.Write("\nEsa Opción No Existe!!, Presione <Enter> para Continuar. . .");
                        Console.ReadKey();
                        break;
                }
            } while (opcion != 3);
        }
    }
}