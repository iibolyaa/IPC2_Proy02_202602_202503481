using IPC2_Proy02_202602_202503481.Datos;

namespace IPC2_Proy02_202602_202503481.Estructuras;
public class NodoLibros
{
    public Libro Libro { get; set; }
    public NodoLibros? Siguiente { get; set; }
    public NodoLibros? Anterior { get; set; }
    public int Altura { get; set; }

    public NodoLibros(Libro libro)
    {
        Libro = libro;
        Siguiente = null;
        Anterior = null;
        Altura = 1; // Inicialmente, la altura es 1 para un nodo hoja
    }
}