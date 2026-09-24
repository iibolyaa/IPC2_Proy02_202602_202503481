using IPC2_Proy02_202602_202503481.Datos;

namespace IPC2_Proy02_202602_202503481.Estructuras;
public class NodoCategoria
{
    public Categoria Categoria { get; set; }
    public NodoCategoria? Hijo { get; set; }
    public NodoCategoria? Hoja { get; set; }

    public ArbolLibros LibrosCategoria { get; set; }

    public NodoCategoria(Categoria categoria)
    {
        Categoria = categoria;
        Hijo = null;
        Hoja = null;

        LibrosCategoria = new ArbolLibros();
    }
}