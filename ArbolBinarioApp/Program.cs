using System;
using System.Collections.Generic;

namespace ArbolBinarioApp
{
    // Clase para representar un nodo del árbol binario
    class Nodo
    {
        public int Valor { get; set; }
        public Nodo Izquierda { get; set; }
        public Nodo Derecha { get; set; }

        public Nodo(int valor)
        {
            Valor = valor;
            Izquierda = null;
            Derecha = null;
        }
    }

    // Clase principal del Árbol Binario
    class ArbolBinario
    {
        public Nodo Raiz { get; private set; }
        
        public ArbolBinario()
        {
            Raiz = null;
        }

        // Método para insertar un valor en el árbol
        public void Insertar(int valor)
        {
            Raiz = InsertarRecursivo(Raiz, valor);
        }

        private Nodo InsertarRecursivo(Nodo nodo, int valor)
        {
            // Si el nodo es nulo, crear un nuevo nodo
            if (nodo == null)
            {
                return new Nodo(valor);
            }

            // Si el valor es menor, insertarlo en el subárbol izquierdo
            if (valor < nodo.Valor)
            {
                nodo.Izquierda = InsertarRecursivo(nodo.Izquierda, valor);
            }
            // Si el valor es mayor, insertarlo en el subárbol derecho
            else if (valor > nodo.Valor)
            {
                nodo.Derecha = InsertarRecursivo(nodo.Derecha, valor);
            }
            // Si el valor ya existe, no hacer nada (árbol binario de búsqueda no permite duplicados)

            return nodo;
        }

        // Calcular la altura del árbol
        public int Altura()
        {
            return CalcularAltura(Raiz);
        }

        private int CalcularAltura(Nodo nodo)
        {
            if (nodo == null)
            {
                return 0;
            }

            int alturaIzquierda = CalcularAltura(nodo.Izquierda);
            int alturaDerecha = CalcularAltura(nodo.Derecha);

            return Math.Max(alturaIzquierda, alturaDerecha) + 1;
        }

        // Calcular el grado del árbol (número máximo de hijos de un nodo)
        public int Grado()
        {
            // En un árbol binario, el grado máximo es 2 (izquierda y derecha)
            // Pero vamos a calcularlo para permitir extensibilidad en caso de árboles n-arios
            return CalcularGrado(Raiz);
        }

        private int CalcularGrado(Nodo nodo)
        {
            if (nodo == null)
            {
                return 0;
            }

            int hijosActuales = 0;
            if (nodo.Izquierda != null) hijosActuales++;
            if (nodo.Derecha != null) hijosActuales++;

            int gradoIzquierda = CalcularGrado(nodo.Izquierda);
            int gradoDerecha = CalcularGrado(nodo.Derecha);

            return Math.Max(hijosActuales, Math.Max(gradoIzquierda, gradoDerecha));
        }

        // Calcular el orden del árbol (promedio de nodos por nivel)
        public double Orden()
        {
            if (Raiz == null)
            {
                return 0;
            }

            int altura = Altura();
            int totalNodos = ContarNodos(Raiz);

            return (double)totalNodos / altura;
        }

        private int ContarNodos(Nodo nodo)
        {
            if (nodo == null)
            {
                return 0;
            }

            return 1 + ContarNodos(nodo.Izquierda) + ContarNodos(nodo.Derecha);
        }

        // Método para obtener la cantidad de nodos por nivel
        public Dictionary<int, int> NodosPorNivel()
        {
            Dictionary<int, int> nodosPorNivel = new Dictionary<int, int>();
            ContarNodosPorNivel(Raiz, 1, nodosPorNivel);
            return nodosPorNivel;
        }

        private void ContarNodosPorNivel(Nodo nodo, int nivel, Dictionary<int, int> nodosPorNivel)
        {
            if (nodo == null)
            {
                return;
            }

            if (!nodosPorNivel.ContainsKey(nivel))
            {
                nodosPorNivel[nivel] = 0;
            }

            nodosPorNivel[nivel]++;

            ContarNodosPorNivel(nodo.Izquierda, nivel + 1, nodosPorNivel);
            ContarNodosPorNivel(nodo.Derecha, nivel + 1, nodosPorNivel);
        }

        // Métodos para recorridos del árbol
        // Recorrido en Preorden (Raíz-Izquierda-Derecha)
        public List<int> Preorden()
        {
            List<int> resultado = new List<int>();
            RecorridoPreorden(Raiz, resultado);
            return resultado;
        }

        private void RecorridoPreorden(Nodo nodo, List<int> resultado)
        {
            if (nodo == null)
            {
                return;
            }

            resultado.Add(nodo.Valor); // Primero la raíz
            RecorridoPreorden(nodo.Izquierda, resultado); // Luego subárbol izquierdo
            RecorridoPreorden(nodo.Derecha, resultado); // Finalmente subárbol derecho
        }

        // Recorrido en Inorden (Izquierda-Raíz-Derecha)
        public List<int> Inorden()
        {
            List<int> resultado = new List<int>();
            RecorridoInorden(Raiz, resultado);
            return resultado;
        }

        private void RecorridoInorden(Nodo nodo, List<int> resultado)
        {
            if (nodo == null)
            {
                return;
            }

            RecorridoInorden(nodo.Izquierda, resultado); // Primero subárbol izquierdo
            resultado.Add(nodo.Valor); // Luego la raíz
            RecorridoInorden(nodo.Derecha, resultado); // Finalmente subárbol derecho
        }

        // Recorrido en Postorden (Izquierda-Derecha-Raíz)
        public List<int> Postorden()
        {
            List<int> resultado = new List<int>();
            RecorridoPostorden(Raiz, resultado);
            return resultado;
        }

        private void RecorridoPostorden(Nodo nodo, List<int> resultado)
        {
            if (nodo == null)
            {
                return;
            }

            RecorridoPostorden(nodo.Izquierda, resultado); // Primero subárbol izquierdo
            RecorridoPostorden(nodo.Derecha, resultado); // Luego subárbol derecho
            resultado.Add(nodo.Valor); // Finalmente la raíz
        }

        // Métodos para buscar un valor y registrar el camino seguido
        // Búsqueda en Preorden
        public (bool encontrado, List<int> camino, int comparaciones) BuscarPreorden(int valor)
        {
            List<int> camino = new List<int>();
            int comparaciones = 0;
            bool encontrado = BuscarPreordenRecursivo(Raiz, valor, camino, ref comparaciones);
            return (encontrado, camino, comparaciones);
        }

        private bool BuscarPreordenRecursivo(Nodo nodo, int valor, List<int> camino, ref int comparaciones)
        {
            if (nodo == null)
            {
                return false;
            }

            comparaciones++;
            camino.Add(nodo.Valor);

            if (nodo.Valor == valor)
            {
                return true;
            }

            if (BuscarPreordenRecursivo(nodo.Izquierda, valor, camino, ref comparaciones))
            {
                return true;
            }

            if (BuscarPreordenRecursivo(nodo.Derecha, valor, camino, ref comparaciones))
            {
                return true;
            }

            return false;
        }

        // Búsqueda en Inorden
        public (bool encontrado, List<int> camino, int comparaciones) BuscarInorden(int valor)
        {
            List<int> camino = new List<int>();
            int comparaciones = 0;
            bool encontrado = BuscarInordenRecursivo(Raiz, valor, camino, ref comparaciones);
            return (encontrado, camino, comparaciones);
        }

        private bool BuscarInordenRecursivo(Nodo nodo, int valor, List<int> camino, ref int comparaciones)
        {
            if (nodo == null)
            {
                return false;
            }

            if (BuscarInordenRecursivo(nodo.Izquierda, valor, camino, ref comparaciones))
            {
                return true;
            }

            comparaciones++;
            camino.Add(nodo.Valor);

            if (nodo.Valor == valor)
            {
                return true;
            }

            if (BuscarInordenRecursivo(nodo.Derecha, valor, camino, ref comparaciones))
            {
                return true;
            }

            return false;
        }

        // Búsqueda en Postorden
        public (bool encontrado, List<int> camino, int comparaciones) BuscarPostorden(int valor)
        {
            List<int> camino = new List<int>();
            int comparaciones = 0;
            bool encontrado = BuscarPostordenRecursivo(Raiz, valor, camino, ref comparaciones);
            return (encontrado, camino, comparaciones);
        }

        private bool BuscarPostordenRecursivo(Nodo nodo, int valor, List<int> camino, ref int comparaciones)
        {
            if (nodo == null)
            {
                return false;
            }

            if (BuscarPostordenRecursivo(nodo.Izquierda, valor, camino, ref comparaciones))
            {
                return true;
            }

            if (BuscarPostordenRecursivo(nodo.Derecha, valor, camino, ref comparaciones))
            {
                return true;
            }

            comparaciones++;
            camino.Add(nodo.Valor);

            if (nodo.Valor == valor)
            {
                return true;
            }

            return false;
        }

        // Método optimizado para buscar utilizando las propiedades del BST (Binary Search Tree)
        public (bool encontrado, List<int> camino, int comparaciones) BuscarBST(int valor)
        {
            List<int> camino = new List<int>();
            int comparaciones = 0;
            bool encontrado = BuscarBSTRecursivo(Raiz, valor, camino, ref comparaciones);
            return (encontrado, camino, comparaciones);
        }

        private bool BuscarBSTRecursivo(Nodo nodo, int valor, List<int> camino, ref int comparaciones)
        {
            if (nodo == null)
            {
                return false;
            }

            comparaciones++;
            camino.Add(nodo.Valor);

            if (nodo.Valor == valor)
            {
                return true;
            }

            if (valor < nodo.Valor)
            {
                return BuscarBSTRecursivo(nodo.Izquierda, valor, camino, ref comparaciones);
            }
            else
            {
                return BuscarBSTRecursivo(nodo.Derecha, valor, camino, ref comparaciones);
            }
        }

        // Método para imprimir el árbol de forma visual
        public void ImprimirArbol()
        {
            if (Raiz == null)
            {
                Console.WriteLine("El árbol está vacío");
                return;
            }

            int altura = Altura();
            ImprimirArbolRecursivo(Raiz, 0, altura);
        }

        private void ImprimirArbolRecursivo(Nodo nodo, int nivel, int altura)
        {
            if (nodo == null)
            {
                return;
            }

            // Imprimir subárbol derecho primero
            ImprimirArbolRecursivo(nodo.Derecha, nivel + 1, altura);

            // Imprimir el nodo actual
            string espacios = new string(' ', nivel * 4);
            string conexion = nivel > 0 ? "└── " : "";
            Console.WriteLine($"{espacios}{conexion}{nodo.Valor}");

            // Imprimir subárbol izquierdo
            ImprimirArbolRecursivo(nodo.Izquierda, nivel + 1, altura);
        }
    }

    // Clase principal del programa
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Sistema de Gestión de Árbol Binario ===");
            ArbolBinario arbol = new ArbolBinario();
            bool salir = false;

            while (!salir)
            {
                Console.WriteLine("\nMenú de opciones:");
                Console.WriteLine("1. Insertar valor en el árbol");
                Console.WriteLine("2. Calcular propiedades del árbol (altura, grado, orden)");
                Console.WriteLine("3. Visualizar árbol");
                Console.WriteLine("4. Recorrer árbol (Preorden, Inorden, Postorden)");
                Console.WriteLine("5. Buscar valor y comparar recorridos");
                Console.WriteLine("6. Salir");
                Console.Write("\nSeleccione una opción: ");

                if (int.TryParse(Console.ReadLine(), out int opcion))
                {
                    Console.WriteLine();

                    switch (opcion)
                    {
                        case 1:
                            InsertarValor(arbol);
                            break;
                        case 2:
                            CalcularPropiedades(arbol);
                            break;
                        case 3:
                            Console.WriteLine("Representación visual del árbol:");
                            arbol.ImprimirArbol();
                            break;
                        case 4:
                            MostrarRecorridos(arbol);
                            break;
                        case 5:
                            BuscarYCompararRecorridos(arbol);
                            break;
                        case 6:
                            salir = true;
                            Console.WriteLine("¡Gracias por utilizar el sistema de gestión de árboles!");
                            break;
                        default:
                            Console.WriteLine("Opción inválida. Por favor, intente nuevamente.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Entrada inválida. Por favor, ingrese un número.");
                }
            }
        }

        static void InsertarValor(ArbolBinario arbol)
        {
            Console.Write("Ingrese el valor a insertar: ");
            if (int.TryParse(Console.ReadLine(), out int valor))
            {
                arbol.Insertar(valor);
                Console.WriteLine($"Valor {valor} insertado correctamente.");
            }
            else
            {
                Console.WriteLine("Valor inválido. Por favor, ingrese un número entero.");
            }
        }

        static void CalcularPropiedades(ArbolBinario arbol)
        {
            Console.WriteLine($"Altura del árbol: {arbol.Altura()}");
            Console.WriteLine($"Grado del árbol: {arbol.Grado()}");
            Console.WriteLine($"Orden del árbol (promedio de nodos por nivel): {arbol.Orden():F2}");
            
            // Mostrar la cantidad de nodos por nivel
            Dictionary<int, int> nodosPorNivel = arbol.NodosPorNivel();
            Console.WriteLine("\nNodos por nivel:");
            foreach (var par in nodosPorNivel)
            {
                Console.WriteLine($"Nivel {par.Key}: {par.Value} nodos");
            }
        }

        static void MostrarRecorridos(ArbolBinario arbol)
        {
            Console.WriteLine("Recorrido Preorden (Raíz-Izquierda-Derecha):");
            MostrarLista(arbol.Preorden());

            Console.WriteLine("\nRecorrido Inorden (Izquierda-Raíz-Derecha):");
            MostrarLista(arbol.Inorden());

            Console.WriteLine("\nRecorrido Postorden (Izquierda-Derecha-Raíz):");
            MostrarLista(arbol.Postorden());
        }

        static void MostrarLista(List<int> lista)
        {
            if (lista.Count == 0)
            {
                Console.WriteLine("(Árbol vacío)");
                return;
            }

            foreach (int valor in lista)
            {
                Console.Write($"{valor} ");
            }
            Console.WriteLine();
        }

        static void BuscarYCompararRecorridos(ArbolBinario arbol)
        {
            Console.Write("Ingrese el valor a buscar: ");
            if (int.TryParse(Console.ReadLine(), out int valor))
            {
                Console.WriteLine($"\nBuscando el valor {valor} utilizando diferentes recorridos...");

                // Búsqueda en Preorden
                var resultadoPreorden = arbol.BuscarPreorden(valor);
                MostrarResultadoBusqueda("Preorden", resultadoPreorden, valor);

                // Búsqueda en Inorden
                var resultadoInorden = arbol.BuscarInorden(valor);
                MostrarResultadoBusqueda("Inorden", resultadoInorden, valor);

                // Búsqueda en Postorden
                var resultadoPostorden = arbol.BuscarPostorden(valor);
                MostrarResultadoBusqueda("Postorden", resultadoPostorden, valor);

                // Búsqueda BST (utilizando propiedades del árbol binario de búsqueda)
                var resultadoBST = arbol.BuscarBST(valor);
                MostrarResultadoBusqueda("BST Optimizado", resultadoBST, valor);

                // Comparar eficiencia de los recorridos
                CompararEficienciaRecorridos(resultadoPreorden, resultadoInorden, resultadoPostorden, resultadoBST);
            }
            else
            {
                Console.WriteLine("Valor inválido. Por favor, ingrese un número entero.");
            }
        }

        static void MostrarResultadoBusqueda((bool encontrado, List<int> camino, int comparaciones) resultado, int valor)
        {
            if (resultado.encontrado)
            {
                Console.WriteLine($"  ✓ Valor {valor} encontrado");
            }
            else
            {
                Console.WriteLine($"  ✗ Valor {valor} no encontrado");
            }

            Console.WriteLine($"  → Camino seguido: {string.Join(" → ", resultado.camino)}");
            Console.WriteLine($"  → Comparaciones realizadas: {resultado.comparaciones}");
            Console.WriteLine();
        }

        static void CompararEficienciaRecorridos(
            (bool, List<int>, int) resultadoPreorden, 
            (bool, List<int>, int) resultadoInorden, 
            (bool, List<int>, int) resultadoPostorden,
            (bool, List<int>, int) resultadoBST)
        {
            Console.WriteLine("Comparación de eficiencia:");
            
            var comparaciones = new Dictionary<string, int>
            {
                { "Preorden", resultadoPreorden.Item3 },
                { "Inorden", resultadoInorden.Item3 },
                { "Postorden", resultadoPostorden.Item3 },
                { "BST Optimizado", resultadoBST.Item3 }
            };

            // Ordenar por número de comparaciones (menor a mayor)
            var recorridosOrdenados = comparaciones.OrderBy(x => x.Value).ToList();

            Console.WriteLine("\nRanking de eficiencia (menor número de comparaciones es mejor):");
            for (int i = 0; i < recorridosOrdenados.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {recorridosOrdenados[i].Key}: {recorridosOrdenados[i].Value} comparaciones");
            }

            Console.WriteLine($"\nEl recorrido más eficiente es: {recorridosOrdenados[0].Key}");

            // Comparar con BST optimizado
            if (recorridosOrdenados[0].Key != "BST Optimizado")
            {
                float mejora = (float)recorridosOrdenados[0].Value / resultadoBST.Item3;
                Console.WriteLine($"El método BST Optimizado es {mejora:F2} veces más eficiente que el mejor recorrido tradicional.");
            }
        }
    }
}