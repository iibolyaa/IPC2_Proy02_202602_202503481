using System.Xml;
using IPC2_Proy02_202602_202503481.Datos;
using IPC2_Proy02_202602_202503481.Estructuras;
using IPC2_Proy02_202602_202503481.Servicios;

namespace IPC2_Proy02_202602_202503481.Archivos;
public class LectorLibros
{
    private Catalogo catalogo;

    public LectorLibros(Catalogo catalogo)
    {
        this.catalogo = catalogo;
    }

    public bool CargarArchivo(string ruta)
    {
        if (!File.Exists(ruta))
        {
            Console.WriteLine("El archivo no existe.");
            return false;
        }

        try
        {
            XmlDocument documento = new XmlDocument();

            documento.Load(ruta);

            XmlNode? raiz = documento.SelectSingleNode("/config");

            if (raiz == null)
            {
                Console.WriteLine("El archivo XML no contiene el nodo config.");

                return false;
            }

            CargarCategorias(raiz);
            CargarLibros(raiz);

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al cargar XML: " + ex.Message);

            return false;
        }
    }

        private void CargarCategorias(XmlNode raiz)
        {
            XmlNode? listaCategorias = raiz.SelectSingleNode("listaCategorias");

            if (listaCategorias == null)
            {
                return;
            }

            foreach (XmlNode nodo in listaCategorias.ChildNodes)
            {
                if (nodo.Name != "categoria")
                {
                    continue;
                }

                string nombre =
                    nodo.InnerText.Trim();

                if (string.IsNullOrWhiteSpace(nombre))
                {
                    continue;
                }

                XmlAttribute? atributoPadre = nodo.Attributes?["padre"];

                if (atributoPadre == null)
                {
                    bool resultado =
                        catalogo.AgregarCategoria(nombre);

                    Console.WriteLine("Categoria: " + nombre + " -> " + resultado);
                }
                else
                {
                    string padre = atributoPadre.Value.Trim();

                    bool resultado = catalogo.AgregarSubCategoria(padre, nombre);

                    Console.WriteLine("Subcategoria: " + nombre + " -> " + padre +
                        " -> " + resultado);
                }
            }
        }

        private void CargarLibros(XmlNode raiz)
        {
            XmlNode? listaLibros = raiz.SelectSingleNode("listaLibros");

            if (listaLibros == null)
            {
                return;
            }

            foreach (XmlNode nodo in listaLibros.ChildNodes)
            {
                if (nodo.Name != "libro")
                {
                    continue;
                }

                XmlNode? isbnNodo =
                    nodo.SelectSingleNode("ISBN");

                XmlNode? tituloNodo =
                    nodo.SelectSingleNode("titulo");

                XmlNode? autorNodo =
                    nodo.SelectSingleNode("autor");

                XmlNode? categoriaNodo =
                    nodo.SelectSingleNode("categoria");

                if (isbnNodo == null ||
                    tituloNodo == null ||
                    autorNodo == null ||
                    categoriaNodo == null)
                {
                    Console.WriteLine( "Libro ignorado por datos incompletos.");

                    continue;
                }

                int isbn;

                if (!int.TryParse(isbnNodo.InnerText.Trim(), out isbn))
                {
                    Console.WriteLine("ISBN invalido.");

                    continue;
                }

                string titulo = tituloNodo.InnerText.Trim();

                string autor = autorNodo.InnerText.Trim();

                string categoria = categoriaNodo.InnerText.Trim();

                bool resultado = catalogo.RegistrarLibro(isbn, titulo, autor, categoria);

                Console.WriteLine("Libro ISBN " + isbn + " -> " + resultado);
            }
        }
}