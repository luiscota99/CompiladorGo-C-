using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompiladorGO
{
    public class Ensamblador
    {
        public string masVar;
        public string EscribirVariables(List<NodoV> lista)
        {
            string texto = "";
            foreach(NodoV var in lista)
            {
                texto += var.nombre+" dw ?"+ Environment.NewLine;
            }
            return texto;
        }

        public string EscribirCodigo(List<ElementoPolish> lista)
        {
            string texto = "";
            masVar = "";
            int cont = 0;
            Stack<NodoV> pila = new Stack<NodoV>();
            foreach (ElementoPolish elem in lista)
            {
                if (elem.Etiqueta.Equals(""))
                {
                    switch (elem.Tipo)
                    {
                        case "numero":
                        case "variable":
                            pila.Push(nuevoNodo(elem.Todo, 0));
                            break;
                        case "cadena":
                            pila.Push(nuevoNodo(elem.Todo, 1));
                            break;
                        case "booleano":
                            if (elem.Todo.Equals("true"))
                            {
                                pila.Push(nuevoNodo("1", 0));
                            }
                            else
                            {
                                pila.Push(nuevoNodo("0", 0));
                            }
                            break;
                        case "operador":
                            if (elem.Todo.Equals("scan"))
                            {
                                texto += "SCANN " + pila.Pop().nombre + Environment.NewLine;
                            }
                            else if(elem.Todo.Equals("print"))
                            {
                                if (pila.Peek().tipo == 1)//se que es una cadena
                                {
                                    texto += "PRINTN " + "'" + pila.Peek().nombre.Substring(1, pila.Pop().nombre.Length - 1)+ "'" + Environment.NewLine;
                                }
                                else
                                {
                                    texto += "ITOA2 " + pila.Pop().nombre + Environment.NewLine;
                                }
                            }
                            else
                            {
                                string operando2 = pila.Pop().nombre;
                                string operando1 = pila.Pop().nombre;
                                string operacion = "";
                                switch (elem.Todo)
                                {
                                    case "-":
                                        operacion = "RESTA ";
                                        break;
                                    case "+":
                                        operacion = "SUMAR ";
                                        break;
                                    case "*":
                                        operacion = "MULTI ";
                                        break;
                                    case ":=":
                                        operacion = "I_ASIGNAR ";
                                        break;
                                    case "<=":
                                        operacion = "I_MENORIGUAL ";
                                        break;
                                    case ">=":
                                        operacion = "I_MAYORIGUAL ";
                                        break;
                                    case "<":
                                        operacion = "I_MENOR ";
                                        break;
                                    case ">":
                                        operacion = "I_MAYOR ";
                                        break;
                                    case "||":
                                        operacion = "ORM ";
                                        break;
                                    case "&&":
                                        operacion = "ANDM ";
                                        break;
                                    case "==":
                                        operacion = "I_IGUAL ";
                                        break;
                                    case "!=":
                                        operacion = "I_DIFERENTES ";
                                        break;
                                }
                                texto += operacion + operando1 + ", " + operando2 +", " + "temporal" + cont + Environment.NewLine;
                                pila.Push(nuevoNodo("temporal" + cont, 0));
                                masVar += "temporal" + cont + " dw ?" + Environment.NewLine;
                                cont++;
                            }
                            break;
                        case "BrincoF":
                            texto += "JF " + pila.Pop().nombre + ", " + elem.Todo.Substring(4) + Environment.NewLine;
                            break;
                        case "BrincoI":
                            texto += "JMP " + elem.Todo.Substring(4) + Environment.NewLine;
                            break;                           
                    }
                }
                else
                {
                    texto += elem.Etiqueta + ":" + Environment.NewLine;
                }                
            }
            return texto;
        }

        private NodoV nuevoNodo(string Nombre, int Tipo)
        {
            NodoV nodo1 = new NodoV();
            nodo1.nombre = Nombre;
            nodo1.tipo = Tipo;
            return nodo1;
        }
    }
}
