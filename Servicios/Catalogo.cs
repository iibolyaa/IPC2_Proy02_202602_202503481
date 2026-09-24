using IPC2_Proy02_202602_202503481.Datos;
using IPC2_Proy02_202602_202503481.Estructuras;

namespace IPC2_Proy02_202602_202503481.Servicios;
public class Catalogo
{
    public ArbolCategorias ArbolCategorias { get; set; }
    public ArbolLibros ArbolLibros { get; set; }

    public Catalogo()
    {
        ArbolCategorias = new ArbolCategorias();
        ArbolLibros = new ArbolLibros();
    }

    public bool RegistrarLibro(int isbn, string titulo, string autor, string categoria)
    {
        if(ArbolLibros.Buscar(isbn) != null)
        {
            return false;
        }

        NodoCategoria? nodoCategoria = ArbolCategorias.BuscarCategoria(categoria);

        if (nodoCategoria == null)
        {
            return false;
        }

        Libro nuevoLibro = new Libro(isbn, titulo, autor, categoria);

        ArbolLibros.Insertar(nuevoLibro);

        nodoCategoria.LibrosCategoria.Insertar(nuevoLibro);

        return true;
    }

    public Libro? BuscarLibro(int isbn)
    {
        return ArbolLibros.Buscar(isbn);
    }

    public Libro? ObtenerLibroMenor()
    {
        return ArbolLibros.ObtenerMenorISBN();
    }

    public Libro? ObtenerLibroMayor()
    {
        return ArbolLibros.ObtenerMayorISBN();
    }

    public bool EliminarLibro(int isbn)
    {
        Libro? libroAEliminar = ArbolLibros.Buscar(isbn);

        if (libroAEliminar == null)
        {
            return false;
        }

        NodoCategoria? Categoria = ArbolCategorias.BuscarCategoria(libroAEliminar.Categoria);

        if (Categoria == null)
        {
            return false;
        }

        ArbolLibros.Eliminar(isbn);
        Categoria.LibrosCategoria.Eliminar(isbn);

        return true;
    }

    public bool AgregarCategoria(string nombreCategoria)
    {
        ArbolCategorias.AgregarCategoriaPrincipal(nombreCategoria);
        return true;
    }

    public bool AgregarSubCategoria(string nombreCategoria, string nombreSubCategoria)
    {
        NodoCategoria? nodoCategoria = ArbolCategorias.BuscarCategoria(nombreCategoria);

        if (nodoCategoria == null)
        {
            return false;
        }

        ArbolCategorias.AgregarSubcategoria(nombreCategoria, nombreSubCategoria);
        return true;
    }

    public bool MostrarLibrosCategoria(string nombreCategoria)
    {
        NodoCategoria? categoria = ArbolCategorias.BuscarCategoria(nombreCategoria);

        if (categoria == null)
        {
            return false;
        }

        categoria.LibrosCategoria.MostrarEnOrden();

        return true;
    }
}