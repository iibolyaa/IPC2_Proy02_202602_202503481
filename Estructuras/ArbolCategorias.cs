using IPC2_Proy02_202602_202503481.Datos;

namespace IPC2_Proy02_202602_202503481.Estructuras;
public class ArbolCategorias
{
    public NodoCategoria? raiz { get; set; }

    public ArbolCategorias()
    {
        raiz = null;
    }

    public NodoCategoria? ObtenerRaiz()
    {
        return raiz;
    }

    public NodoCategoria? BuscarCategoria(string nombre)
    {
        return BuscarRecursivo(raiz, nombre);
    }

    private NodoCategoria? BuscarRecursivo(NodoCategoria? actual, string nombre)
    {
        if (actual == null)
        {
            return null;
        }

        if (actual.Categoria.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase))
        {
            return actual;
        }

        NodoCategoria? encontrado = BuscarRecursivo(actual.Hijo, nombre);

        if (encontrado != null)
        {
            return encontrado;
        }

        return BuscarRecursivo(actual.Hoja, nombre);
    }

    public bool AgregarCategoriaPrincipal(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            return false;
        }

        if (BuscarCategoria(nombre) != null)
        {
            return false;
        }

        Categoria nuevaCategoria = new Categoria(nombre);
        NodoCategoria nuevoNodo = new NodoCategoria(nuevaCategoria);

        if (raiz == null)
        {
            raiz = nuevoNodo;
            return true;
        }

        raiz = InsertarAlfabeticamente(raiz, nuevoNodo);

        return true;
    }

    private NodoCategoria? InsertarAlfabeticamente(NodoCategoria? primero, NodoCategoria nuevo){
        if (primero == null)
        {
            return nuevo;
        }

        if (string.Compare(nuevo.Categoria.Nombre, primero.Categoria.Nombre, StringComparison.OrdinalIgnoreCase) < 0)
        {
            nuevo.Hoja = primero;
            return nuevo;
        }

        primero.Hoja = InsertarAlfabeticamente(primero.Hoja, nuevo);
        return primero;
    }

    public bool AgregarSubcategoria(string nombrePadre, string nombreNuevaCategoria){
        if (string.IsNullOrWhiteSpace(nombrePadre) || string.IsNullOrWhiteSpace(nombreNuevaCategoria))
        {
            return false;
        }
        
        if (BuscarCategoria(nombreNuevaCategoria) != null)
        {
            return false;
        }

        NodoCategoria? padre = BuscarCategoria(nombrePadre);

        if (padre == null)
        {
            return false;
        }

        Categoria nuevaCategoria = new Categoria(nombreNuevaCategoria);

        NodoCategoria nuevoNodo = new NodoCategoria(nuevaCategoria);

        NodoCategoria? Hijo = padre.Hijo;

        padre.Hijo = InsertarAlfabeticamente(Hijo, nuevoNodo);

        return true;
    }

    public void MostrarCategorias()
    {
        MostrarRecursivo(raiz, 0);
    }

    private void MostrarRecursivo(NodoCategoria? actual, int nivel)
    {
        if (actual == null)
        {
            return;
        }

        for (int i = 0; i < nivel; i++)
        {
            Console.Write("    ");
        }

        Console.WriteLine(
            "- " + actual.Categoria.Nombre);

        MostrarRecursivo(actual.Hijo, nivel + 1);

        MostrarRecursivo(actual.Hoja, nivel);
    }

    
}