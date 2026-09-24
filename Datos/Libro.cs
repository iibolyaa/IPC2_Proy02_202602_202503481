namespace IPC2_Proy02_202602_202503481.Datos;

public class Libro
{
        public int ISBN { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Categoria { get; set; }

        public Libro(int isbn, string titulo, string autor, string categoria)
        {
            ISBN = isbn;
            Titulo = titulo;
            Autor = autor;
            Categoria = categoria;
        }
    
}