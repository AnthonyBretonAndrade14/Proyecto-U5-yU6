using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Proyecto_U5_yU6
{
    class Comentario
    {
        public string id { get; set; }                                           //Determinacion de Variables en Clase de comentario
        public string creador { get; set; }
        public DateTime fechadepublicacion { get; set; }
        public string comentario { get; set; }
        public string direccion_ip { get; set; }
       
       public int likes { get; set; }
        public bool inapropiado { get; set; }
        public override string ToString()
        {
            return string.Format($"{creador} - Comentario: {comentario} - Inapropiado {inapropiado} - IP: {direccion_ip} - Fecha {fechadepublicacion} - Likes {likes}");          //Codigo para que imprima 
        }
    }

    class Comentary

    {
        public static void SaveToFile(List<Comentario> comentarios, string path)                               //Metodo para que guarde el archivo
        {
            StreamWriter textOut = null;

            try
            {
                textOut = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write));
                foreach (var comentario in comentarios)
                {
                    textOut.Write(comentario.creador + "|");
                    textOut.Write(comentario.comentario + "|");
                    textOut.Write(comentario.direccion_ip + "|");
                    textOut.Write(comentario.inapropiado + "|");
                    textOut.Write(comentario.fechadepublicacion + "|");
                    textOut.Write(comentario.likes + "|");
                    textOut.WriteLine(comentario.id);
                }
            }                                                                       //Ecepciones que podrian ocurrir 
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)                                  //Catch generica por si al final de cuneta alguna se pasa 
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (textOut != null)
                    textOut.Close();
            }
        }
               //Esta en punto texto sin necesidad de binario 
        public static List<Comentario>ReadFromFile(string path)
        {


            List<Comentario> comentarios = new List<Comentario>();

            StreamReader textIn = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read));
            try
            {

                while (textIn.Peek() != -1)
                {

                    string now = textIn.ReadLine();
                    string[] colums = now.Split("|");
                    Comentario c = new Comentario();
                    c.creador = colums[0];
                    c.comentario = colums[1];
                    c.direccion_ip = colums[2];
                    c.inapropiado = bool.Parse(colums[3]);
                    c.fechadepublicacion = DateTime.Parse(colums[4]);
                    c.likes = int.Parse(colums[5]);
                    c.id = colums[6];
                    comentarios.Add(c);


                }

            }

            catch (ArgumentException e)
            {


                Console.WriteLine(e.Message);

            }
            catch(NotSupportedException e)
            {

                Console.WriteLine(e.Message);

            }
            catch(FileNotFoundException e)
            {

                Console.WriteLine(e.Message);

            }
            catch (DirectoryNotFoundException e)
            {

                Console.WriteLine(e.Message);

            }
            catch (UnauthorizedAccessException e)
            {

                Console.WriteLine(e.Message);

            }
            catch(FormatException e)
            {

                Console.WriteLine(e.Message);

            }
            catch (IOException e)
            {

                Console.WriteLine(e.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);

            }
            finally
            {

                textIn.Close();

            }
            return comentarios;
        }
        //Orden de Likes 
        public static void GetLikes(string path)
        {

            List<Comentario> comentarios;
            comentarios = ReadFromFile(path);
            var filtro_comentarios = from c in comentarios              
                                     orderby c.likes descending
                                     select c;
            foreach (var c in filtro_comentarios)
                if (c.inapropiado == false)
                    Console.WriteLine(c);                     



        }
        //Orden de comentarios por fechas 
        public static void GetDate(string path)
        {

            List<Comentario> comentarios;
            comentarios = ReadFromFile(path);
            var filtro_comentarios = from c in comentarios
                                     orderby c.fechadepublicacion descending
                                     select c;
            foreach (var c in filtro_comentarios)
                if (c.inapropiado == false)
                    Console.WriteLine(c);

        }

        class program
        {

            public static void BorrarComentario(List<Comentario> comentarios)
            {


                Console.Clear();
                Console.WriteLine("Ingrese el ID de comentario que desea Eliminar permanentemente");
                string r = Console.ReadLine();

                comentarios.RemoveAll(a => a.id.Contains(r));
                foreach (var c in comentarios.Where(c => c.inapropiado == false))
                {
                    Console.WriteLine(c);


                }
                Console.ReadKey();



            }
            static void Main(string[] args)
            {

                List<Comentario> comentarios = Comentary.ReadFromFile(@"C:\Users\tonyb\Documents\comentarios.txt");            //Ruta de documento asociado 
                foreach (var c in from c in comentarios
                                  where c.inapropiado == false             //comentarios inapropiados seran ocultos 
                                  select c)
                {

                    Console.WriteLine(c);


                }
                Console.WriteLine();
                Console.WriteLine("| ordenados por likes |");
                Comentary.GetLikes(@"C:\Users\tonyb\Documents\comentarios.txt");                              //Ruta de documento asociado 

                Console.WriteLine();
                Console.WriteLine("| ordenados por las fechas  |");
                Comentary.GetDate(@"C:\Users\tonyb\Documents\comentarios.txt");                                //Ruta de documento asociado 
                Console.ReadKey();

                Console.Clear();
                Console.WriteLine("Desearia borrar un comentario no deseado? [S/N]");
                string respuesta = Console.ReadLine();
                if (respuesta == "Si" | respuesta == "si")
                {

                    BorrarComentario(comentarios);

                }
                else if (respuesta == "No"  |  respuesta == "no")
                {

                    Console.Clear();
                    Console.WriteLine("No metio ningun formato valido");


                }
                Console.ReadKey();

            }
        }



               
                

                


                



           
                    
                    
                    
                    
            

            

            
                


                


        }
        

           

        
    } 

