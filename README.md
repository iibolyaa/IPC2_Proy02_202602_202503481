# IPC2_Proy02_202602_202503481 — Catálogo de libros

Aplicación web en C# con framework (ASP.NET Core MVC) que organiza un catálogo de libros por
categorías/subcategorías (árbol general) y permite búsqueda eficiente por ISBN
(árbol binario, implementado tipo AVL). 

## Requisitos

- [.NET SDK 8.0](https://dotnet.microsoft.com/download) o superior.
- [Graphviz](https://graphviz.org/download/) instalado, con el comando `dot`
  disponible en el `PATH` (necesario para los reportes gráficos).

## Compilar y ejecutar

```bash
cd IPC2_Proy02_202602_202503481
dotnet build
dotnet run
```

Al ejecutarse, la aplicación levanta un servidor local (por defecto algo como
`https://localhost:5001` o `http://localhost:5000`; la terminal muestra la URL
exacta) y desde ahí se accede a la interfaz web.

## Uso básico

1. Abrir la URL que indica la terminal.
2. Desde la pantalla principal, cargar uno o varios archivos XML de `Entradas/`
   (por ejemplo `entrada_100.xml`) para poblar categorías y libros.
3. Usar el menú de Catálogo para:
   - Ver la estructura de categorías/subcategorías.
   - Ver los libros de una categoría (orden ascendente de ISBN, vía Graphviz).
   - Registrar, buscar o eliminar libros por ISBN.

## Archivos de entrada de ejemplo

En `Entradas/` hay cuatro archivos de prueba (no deben modificarse):
`casos_borde.xml`, `entrada_100.xml`, `entrada_1000.xml`, `entrada_10000.xml`.

## Notas

- Todas las estructuras dinámicas (árbol de categorías, árbol AVL de libros)
  están implementadas a mano en `Estructuras/`
- Si falla la generación de un reporte, confirma que `dot` funcione desde una
  terminal nueva (a veces hay que reiniciarla tras instalar Graphviz).