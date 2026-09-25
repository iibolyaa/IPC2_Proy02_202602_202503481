using Microsoft.AspNetCore.Mvc;
using IPC2_Proy02_202602_202503481.Servicios;
using IPC2_Proy02_202602_202503481.Datos;
using IPC2_Proy02_202602_202503481.Archivos;

namespace IPC2_Proy02_202602_202503481.Controllers
{
    public class CatalogoController : Controller
    {
        private readonly Catalogo catalogo;

        public CatalogoController(Catalogo catalogo)
        {
            this.catalogo = catalogo;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult AgregarCategoria(string nombre)
        {
            bool resultado =
                catalogo.AgregarCategoria(nombre);

            if (resultado)
            {
                TempData["Mensaje"] =
                    "Categoria agregada correctamente.";
            }
            else
            {
                TempData["Error"] =
                    "No se pudo agregar la categoria.";
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult AgregarSubcategoria(string padre, string nombre)
        {
            bool resultado = catalogo.AgregarSubCategoria(padre, nombre);

            if (resultado)
            {
                TempData["Mensaje"] =
                    "Subcategoria agregada correctamente.";
            }
            else
            {
                TempData["Error"] =
                    "No se pudo agregar la subcategoria.";
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult RegistrarLibro(int isbn, string titulo, string autor, string categoria)
        {
            bool resultado =
                catalogo.RegistrarLibro(isbn, titulo, autor, categoria);

            if (resultado)
            {
                TempData["Mensaje"] =
                    "Libro registrado correctamente.";
            }
            else
            {
                TempData["Error"] =
                    "No se pudo registrar el libro.";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult BuscarLibro(int isbn)
        {
            Libro? libro =
                catalogo.BuscarLibro(isbn);

            if (libro == null)
            {
                TempData["Error"] =
                    "No se encontro el libro.";

                return RedirectToAction("Index");
            }

            ViewBag.Libro = libro;

            return View("ResultadoLibro");
        }


        [HttpPost]
        public IActionResult EliminarLibro(int isbn)
        {
            bool resultado =
                catalogo.EliminarLibro(isbn);

            if (resultado)
            {
                TempData["Mensaje"] =
                    "Libro eliminado correctamente.";
            }
            else
            {
                TempData["Error"] =
                    "No existe un libro con ese ISBN.";
            }

            return RedirectToAction("Index");
        }


        public IActionResult Menor()
        {
            Libro? libro =
                catalogo.ObtenerLibroMenor();

            ViewBag.Libro = libro;
            ViewBag.Tipo = "menor";

            return View("ResultadoLibro");
        }

        
        public IActionResult Mayor()
        {
            Libro? libro =
                catalogo.ObtenerLibroMayor();

            ViewBag.Libro = libro;
            ViewBag.Tipo = "mayor";

            return View("ResultadoLibro");
        }
    }
}