// Generando con ayuda de Copilot: menú y filtro básico

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatalogoCursos
{
    class Course
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Area { get; set; } = "";
    }

    class Program
    {
        static List<Course> Courses = new()
        {
            new Course { Id = 1, Name = "Algoritmos I",                 Area = "CS"   },
            new Course { Id = 2, Name = "Introducción a la Programación", Area = "CS" },
            new Course { Id = 3, Name = "Matemática Discreta",          Area = "Math" },
            new Course { Id = 4, Name = "Bases de Datos I",             Area = "CS"   },
            new Course { Id = 5, Name = "Redes de Computadoras",        Area = "CS"   }
        };

        const int PageSize = 3;

        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            int option;
            do
            {
                MostrarMenu();
                var input = Console.ReadLine();
                Console.Clear();

                if (!int.TryParse(input, out option))
                {
                    option = -1;
                }

                switch (option)
                {
                    case 1:
                        ListCourses(Courses);
                        EsperarEnter();
                        break;

                    case 2:
                        SearchCourses();
                        EsperarEnter();
                        break;

                    case 3:
                        PaginateCourses();
                        break;

                    case 0:
                        Console.WriteLine("Saliendo del Catálogo de Cursos...");
                        break;

                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.");
                        EsperarEnter();
                        break;
                }

                Console.Clear();
            }
            while (option != 0);
        }

        static void MostrarMenu()
        {
            Console.WriteLine("=== Catálogo de Cursos - Demo ===");
            Console.WriteLine("1) Listar todos los cursos");
            Console.WriteLine("2) Buscar cursos por nombre");
            Console.WriteLine("3) Ver cursos con paginación");
            Console.WriteLine("0) Salir");
            Console.Write("Seleccione una opción: ");
        }

        static void ListCourses(IEnumerable<Course> courses)
        {
            Console.WriteLine("Lista de cursos:");
            foreach (var c in courses)
            {
                Console.WriteLine($"[{c.Id}] {c.Name} - {c.Area}");
            }
        }

        static void SearchCourses()
        {
            Console.Write("Ingrese texto a buscar: ");
            var q = (Console.ReadLine() ?? "").Trim().ToLower();

            var result = Courses
                .Where(c => c.Name.ToLower().Contains(q))
                .ToList();

            Console.WriteLine();

            if (!result.Any())
            {
                Console.WriteLine("No se encontraron cursos que coincidan con la búsqueda.");
                return;
            }

            ListCourses(result);
        }

        static void PaginateCourses()
        {
            int page = 0;
            ConsoleKey key;

            do
            {
                Console.Clear();
                var pageItems = Courses
                    .Skip(page * PageSize)
                    .Take(PageSize)
                    .ToList();

                Console.WriteLine($"Página {page + 1}");

                if (!pageItems.Any())
                {
                    Console.WriteLine("No hay cursos en esta página.");
                }
                else
                {
                    ListCourses(pageItems);
                }

                Console.WriteLine("\nN = siguiente | P = anterior | S = salir");
                key = Console.ReadKey(intercept: true).Key;

                if (key == ConsoleKey.N && (page + 1) * PageSize < Courses.Count)
                {
                    page++;
                }
                else if (key == ConsoleKey.P && page > 0)
                {
                    page--;
                }

            } while (key != ConsoleKey.S && key != ConsoleKey.Escape);
        }

        static void EsperarEnter()
        {
            Console.WriteLine("\nPresione ENTER para continuar...");
            Console.ReadLine();
        }
    }
}
