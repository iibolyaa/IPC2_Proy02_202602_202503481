using System;
using IPC2_Proy02_202602_202503481.Datos;

namespace IPC2_Proy02_202602_202503481.Estructuras;
public class ArbolLibros
{
        private NodoLibros? raiz;

        public ArbolLibros()
        {
            raiz = null;
        }

        public NodoLibros? ObtenerRaiz()
        {
            return raiz;
        }

        private int ObtenerAltura(NodoLibros? nodo)
        {
            if (nodo == null)
            {
                return 0;
            }

            return nodo.Altura;
        }

        private int ObtenerBalance(NodoLibros? nodo)
        {
            if (nodo == null)
            {
                return 0;
            }

            return ObtenerAltura(nodo.Izquierda) - ObtenerAltura(nodo.Derecha);
        }

        private int Mayor(int a, int b)
        {
            if (a > b)
            {
                return a;
            }

            return b;
        }

        public bool Insertar(Libro libro)
        {
            if (Buscar(libro.ISBN) != null)
            {
                return false;
            }

            raiz = InsertarRecursivo(raiz, libro);

            return true;
        }

        private NodoLibros InsertarRecursivo(NodoLibros? nodo, Libro libro)
        {
            if (nodo == null)
            {
                return new NodoLibros(libro);
            }

            if (libro.ISBN < nodo.Libro.ISBN)
            {
                nodo.Izquierda = InsertarRecursivo(nodo.Izquierda, libro);
            }
            else
            {
                nodo.Derecha = InsertarRecursivo(nodo.Derecha, libro);
            }

            nodo.Altura = 1 + Mayor(ObtenerAltura(nodo.Izquierda), ObtenerAltura(nodo.Derecha));

            int balance = ObtenerBalance(nodo);

            // Izquierda - Izquierda
            if (balance > 1 && libro.ISBN < nodo.Izquierda!.Libro.ISBN)
            {
                return RotacionDerecha(nodo);
            }

            // Derecha - Derecha
            if (balance < -1 && libro.ISBN > nodo.Derecha!.Libro.ISBN)
            {
                return RotacionIzquierda(nodo);
            }

            // Izquierda - Derecha
            if (balance > 1 && libro.ISBN > nodo.Izquierda!.Libro.ISBN)
            {
                nodo.Izquierda = RotacionIzquierda(nodo.Izquierda);

                return RotacionDerecha(nodo);
            }

            // Derecha - Izquierda
            if (balance < -1 && libro.ISBN < nodo.Derecha!.Libro.ISBN)
            {
                nodo.Derecha = RotacionDerecha(nodo.Derecha);

                return RotacionIzquierda(nodo);
            }

            return nodo;
        }

        private NodoLibros RotacionDerecha(NodoLibros nodo)
        {
            NodoLibros nuevaRaiz =
                nodo.Izquierda!;

            NodoLibros temporal = nuevaRaiz.Derecha;

            nuevaRaiz.Derecha = nodo;

            nodo.Izquierda = temporal;

            nodo.Altura = 1 + Mayor(ObtenerAltura(nodo.Izquierda), ObtenerAltura(nodo.Derecha));

            nuevaRaiz.Altura = 1 + Mayor(ObtenerAltura(nuevaRaiz.Izquierda), ObtenerAltura(nuevaRaiz.Derecha));

            return nuevaRaiz;
        }

        private NodoLibros RotacionIzquierda(NodoLibros nodo)
        {
            NodoLibros nuevaRaiz = nodo.Derecha!;

            NodoLibros? temporal = nuevaRaiz.Izquierda;

            nuevaRaiz.Izquierda = nodo;

            nodo.Derecha = temporal;

            nodo.Altura = 1 + Mayor(ObtenerAltura(nodo.Izquierda), ObtenerAltura(nodo.Derecha));

            nuevaRaiz.Altura = 1 + Mayor(ObtenerAltura(nuevaRaiz.Izquierda), ObtenerAltura(nuevaRaiz.Derecha));

            return nuevaRaiz;
        }

        public Libro? Buscar(int isbn)
        {
            NodoLibros? actual = raiz;

            while (actual != null)
            {
                if (isbn == actual.Libro.ISBN)
                {
                    return actual.Libro;
                }

                if (isbn < actual.Libro.ISBN)
                {
                    actual = actual.Izquierda;
                }
                else
                {
                    actual = actual.Derecha;
                }
            }

            return null;
        }

        public void MostrarEnOrden()
        {
            MostrarEnOrdenRecursivo(raiz);
        }

        private void MostrarEnOrdenRecursivo(NodoLibros? nodo)
        {
            if (nodo == null)
            {
                return;
            }

            MostrarEnOrdenRecursivo(nodo.Izquierda);

            Console.WriteLine(
                nodo.Libro.ISBN +
                " - " +
                nodo.Libro.Titulo);

            MostrarEnOrdenRecursivo(nodo.Derecha);
        }

        //Libro con el ISBN más pequeño (el primer libro en orden ascendente)
        public Libro? ObtenerMenorISBN()
        {
            if (raiz == null)
            {
            return null;
            }

            NodoLibros actual = raiz;

            while (actual.Izquierda != null)
            {
                actual = actual.Izquierda;
            }

        return actual.Libro;
    }

    //Libro con el ISBN más grande (el último libro en orden ascendente)
    public Libro? ObtenerMayorISBN()
    {
        if (raiz == null)
        {
            return null;
        }

        NodoLibros actual = raiz;

        while (actual.Derecha != null)
        {
            actual = actual.Derecha;
        }

        return actual.Libro;
    }

    //Eliminar un libro por búsqueda de ISBN
    public bool Eliminar(int isbn)
    {
        if (Buscar(isbn) == null)
        {
            return false;
        }

        raiz = EliminarRecursivo(raiz, isbn);

        return true;
    }

    private NodoLibros? EliminarRecursivo(NodoLibros? nodo, int isbn){
        if (nodo == null)
        {
            return null;
        }

        if (isbn < nodo.Libro.ISBN)
        {
            nodo.Izquierda =
                EliminarRecursivo(nodo.Izquierda, isbn);
        }
        else if (isbn > nodo.Libro.ISBN)
        {
            nodo.Derecha = EliminarRecursivo(nodo.Derecha, isbn);
        }
        else
        {
            if (nodo.Izquierda == null)
            {
                return nodo.Derecha;
            }

            if (nodo.Derecha == null)
            { 
                return nodo.Izquierda;
            }

            NodoLibros sucesor = ObtenerNodoMenor(nodo.Derecha); //Método para reemplazar elemento eliminado con el el más pequeño de su rama

            nodo.Libro = sucesor.Libro;

            nodo.Derecha = EliminarRecursivo(nodo.Derecha, sucesor.Libro.ISBN);
        }

        nodo.Altura = 1 + Mayor(ObtenerAltura(nodo.Izquierda), ObtenerAltura(nodo.Derecha));

        int balance = ObtenerBalance(nodo);

        if (balance > 1 && ObtenerBalance(nodo.Izquierda) >= 0)
        {
            return RotacionDerecha(nodo);
        }

        if (balance > 1 && ObtenerBalance(nodo.Izquierda) < 0)
        {
            nodo.Izquierda = RotacionIzquierda(nodo.Izquierda!);

        return RotacionDerecha(nodo);
        }

        if (balance < -1 && ObtenerBalance(nodo.Derecha) <= 0)
        {
            return RotacionIzquierda(nodo);
        }

        if (balance < -1 && ObtenerBalance(nodo.Derecha) > 0){
        nodo.Derecha = RotacionDerecha(nodo.Derecha!);

            return RotacionIzquierda(nodo);
        }

        return nodo;
    }

    private NodoLibros ObtenerNodoMenor(NodoLibros nodo)
    {
        NodoLibros actual = nodo;

        while (actual.Izquierda != null)
        {
            actual = actual.Izquierda;
        }

        return actual;
    }
}