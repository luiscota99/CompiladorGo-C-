using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompiladorGO
{
    public class Lexico
    {
        int[,] matriz =
        {
                       {  1,   2,   5, 115, 103, 104, 105,   9,  16, 107, 108, 109, 110, 111, 112, 113, 114, 117,  10,  11,  12,  13,  14,  15,   0, 0,   0, 500},
                       {  1,   1, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100},
                       {101,   2, 101, 101,   3, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101},
                       {507,   4, 507, 507, 507, 507, 507, 507, 507, 507, 507, 507, 507, 507, 507, 507, 507, 507, 507, 507, 507, 507, 507, 507, 507, 507, 507, 507},
                       {102,   4, 102, 102, 102, 102, 102, 102, 102, 102, 102, 102, 102, 102, 102, 102, 102, 102, 102, 102, 102, 102, 102, 102, 102, 102, 102, 102},
                       {116, 116,   6,   7, 116, 116, 116, 116, 116, 116, 116, 116, 116, 116, 116, 116, 116, 116, 116, 116, 116, 116, 116, 116, 116, 116, 116, 116},
                       {  6,   6,   6,   6,   6,   6,   6,   6,   6,   6,   6,   6,   6,   6,   6,   6,   6,   6,   6,   6,   6,   6,   6,   6,   0, 0,   6,   6},
                       {  7,   7,   7,   8,   7,   7,   7,   7,   7,   7,   7,   7,   7,   7,   7,   7,   7,   7,   7,   7,   7,   7,   7,   7,   7, 506,   7,   7},
                       {  7,   7,   0,   7,   7,   7,   7,   7,   7,   7,   7,   7,   7,   7,   7,   7,   7,   7,   7,   7,   7,   7,   7,   7,   7, 506,   7,   7},
                       {505, 505, 505, 505, 505, 505, 505, 505, 124, 505, 505, 505, 505, 505 ,505, 505, 505, 505, 505, 505, 505, 505, 505, 505, 505, 505, 505, 505},
                       {121, 121, 121, 121, 121, 121, 121, 121, 123, 121, 121, 121, 121, 121, 121, 121, 121, 121, 121, 121, 121, 121, 121, 121, 121, 121, 121, 121},
                       {120, 120, 120, 120, 120, 120, 120, 120, 122, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120},
                       {128, 128, 128, 128, 128, 128, 128, 128, 119, 128, 128, 128, 128, 128, 128, 128, 128, 128, 128, 128, 128, 128, 128, 128, 128, 128, 128, 128},
                       { 13,  13,  13,  13,  13,  13,  13,  13,  13,  13,  13,  13,  13,  13,  13,  13,  13,  13,  13,  13,  13, 125,  13,  13, 504, 504,  13,  13},
                       {503, 503, 503, 503, 503, 503, 503, 503, 503, 503, 503, 503, 503, 503, 503, 503, 503, 503, 503, 503, 503, 503, 126, 503, 503, 503, 503, 503},
                       {502, 502, 502, 502, 502, 502, 502, 502, 502, 502, 502, 502, 502, 502, 502, 502, 502, 502, 502, 502, 502, 502, 502, 127, 502, 502, 502, 502},
                       {501, 501, 501, 501, 501, 501, 501, 501, 118, 501, 501, 501, 501, 501, 501, 501, 501, 501, 501, 501, 501, 501, 501, 501, 501, 501, 501, 501}
        };

        public List<Nodo> Nodos = new List<Nodo>();
        public int contadorRenglon=1;
        public int contadorCaracter=0;
        public Nodo item;
        public string error = "";
        public string[] ArrayReservadas = { "if", "else", "for", "break", "continue", "true", "false", "scan", "print", "go", "package", "main", "func", "var", "const", "type", "int", "string", "bool", "float" };

        public void LeerCaracteres(string txtCodigo)
        {
            int estado = 0;
            string textoAlmacenado="";
            bool llegoFinal = false;
            while (contadorCaracter < txtCodigo.Length + 1)
            {
                if(contadorCaracter==txtCodigo.Length)
                {
                    estado = matriz[estado, 25];
                    llegoFinal = true;
                }
                else
                {
                    if (Char.IsLetter(txtCodigo.ElementAt(contadorCaracter)))
                    {
                        estado = matriz[estado, 0];
                        textoAlmacenado += txtCodigo.ElementAt(contadorCaracter);
                    }
                    else if (Char.IsDigit(txtCodigo.ElementAt(contadorCaracter)))
                    {
                        estado = matriz[estado, 1];
                        textoAlmacenado += txtCodigo.ElementAt(contadorCaracter);
                    }
                    else
                    {
                        switch(txtCodigo.ElementAt(contadorCaracter))
                        {
                            case '/':
                                estado = matriz[estado, 2];
                                textoAlmacenado += txtCodigo.ElementAt(contadorCaracter);
                                break;
                            case '*':
                                estado = matriz[estado, 3];
                                textoAlmacenado += txtCodigo.ElementAt(contadorCaracter);
                                break;
                            case '.':
                                estado = matriz[estado, 4];
                                textoAlmacenado += txtCodigo.ElementAt(contadorCaracter);
                                break;
                            case ';':
                                estado = matriz[estado, 5];
                                textoAlmacenado += txtCodigo.ElementAt(contadorCaracter);
                                break;
                            case ',':
                                estado = matriz[estado, 6];
                                textoAlmacenado += txtCodigo.ElementAt(contadorCaracter);
                                break;
                            case ':':
                                estado = matriz[estado, 7];
                                textoAlmacenado += txtCodigo.ElementAt(contadorCaracter);
                                break;
                            case '=':
                                estado = matriz[estado, 8];
                                textoAlmacenado += txtCodigo.ElementAt(contadorCaracter);
                                break;
                            case '(':
                                estado = matriz[estado, 9];
                                textoAlmacenado += txtCodigo.ElementAt(contadorCaracter);
                                break;
                            case ')':
                                estado = matriz[estado, 10];
                                textoAlmacenado += txtCodigo.ElementAt(contadorCaracter);
                                break;
                            case '{':
                                estado = matriz[estado, 11];
                                textoAlmacenado += txtCodigo.ElementAt(contadorCaracter);
                                break;
                            case '}':
                                estado = matriz[estado, 12];
                                textoAlmacenado += txtCodigo.ElementAt(contadorCaracter);
                                break;
                            case '[':
                                estado = matriz[estado, 13];
                                textoAlmacenado += txtCodigo.ElementAt(contadorCaracter);
                                break;
                            case ']':
                                estado = matriz[estado, 14];
                                textoAlmacenado += txtCodigo.ElementAt(contadorCaracter);
                                break;
                            case '+':
                                estado = matriz[estado, 15];
                                textoAlmacenado += txtCodigo.ElementAt(contadorCaracter);
                                break;
                            case '-':
                                estado = matriz[estado, 16];
                                textoAlmacenado += txtCodigo.ElementAt(contadorCaracter);
                                break;
                            case '%':
                                estado = matriz[estado, 17];
                                textoAlmacenado += txtCodigo.ElementAt(contadorCaracter);
                                break;
                            case '>':
                                estado = matriz[estado, 18];
                                textoAlmacenado += txtCodigo.ElementAt(contadorCaracter);
                                break;
                            case '<':
                                estado = matriz[estado, 19];
                                textoAlmacenado += txtCodigo.ElementAt(contadorCaracter);
                                break;
                            case '!':
                                estado = matriz[estado, 20];
                                textoAlmacenado += txtCodigo.ElementAt(contadorCaracter);
                                break;
                            case '"':
                                estado = matriz[estado, 21];
                                textoAlmacenado += txtCodigo.ElementAt(contadorCaracter);
                                break;
                            case '&':
                                estado = matriz[estado, 22];
                                textoAlmacenado += txtCodigo.ElementAt(contadorCaracter);
                                break;
                            case '|':
                                estado = matriz[estado, 23];
                                textoAlmacenado += txtCodigo.ElementAt(contadorCaracter);
                                break;
                            case '\r':
                                break;
                            case '\n':
                                contadorRenglon++;
                                estado = matriz[estado, 24];
                                textoAlmacenado += txtCodigo.ElementAt(contadorCaracter);
                                break;
                            case ' ':
                                estado = matriz[estado, 26];
                                textoAlmacenado += txtCodigo.ElementAt(contadorCaracter);
                                break;
                            case '\t':
                                estado = matriz[estado, 26];
                                textoAlmacenado += txtCodigo.ElementAt(contadorCaracter);
                                break;
                            default:
                                estado = matriz[estado, 27];
                                textoAlmacenado += txtCodigo.ElementAt(contadorCaracter);
                                break;
                        }
                    }
                }
                if(estado==0)
                {
                    textoAlmacenado = "";
                }
                if (estado >= 500) break;
                switch(estado)
                {
                    case 100:
                        if (!llegoFinal) textoAlmacenado = textoAlmacenado.Substring(0, textoAlmacenado.Length - 1);
                        int tipo = palabrasReservadas(textoAlmacenado, 100);
                        Nodos.Add(nuevoNodo(textoAlmacenado, tipo, contadorRenglon));
                        contadorCaracter--;
                        textoAlmacenado = "";
                        break;
                    case 101:
                    case 102:
                    case 116:
                    case 120:
                    case 121:
                    case 128:
                        if (!llegoFinal) textoAlmacenado = textoAlmacenado.Substring(0, textoAlmacenado.Length - 1);
                        Nodos.Add(nuevoNodo(textoAlmacenado, estado, contadorRenglon));
                        contadorCaracter--;
                        textoAlmacenado = "";
                        break;
                    case 103:
                    case 104:
                    case 105:
                    case 107:
                    case 108:
                    case 109:
                    case 110:
                    case 111:
                    case 112:
                    case 113:
                    case 114:
                    case 115:
                    case 117:
                    case 118:
                    case 119:
                    case 122:
                    case 123:
                    case 124:
                    case 125:
                    case 126:
                    case 127:
                        Nodos.Add(nuevoNodo(textoAlmacenado, estado, contadorRenglon));
                        textoAlmacenado = "";
                        break;
                }
                if(estado>=100)
                { estado = 0; }
                contadorCaracter++;
            }
            if(estado>=500)
            {
                switch (estado)
                {
                    case 500:
                        error = "Error lexico caracter no valido en el renglon "+ contadorRenglon;
                        break;
                    case 501:
                        error = "Se espera '=' despues de '='";
                        break;
                    case 502:
                        error = "Se espera '|' despues de '|'";
                        break;
                    case 503:
                        error = "Se espera '&' despues de '&'";
                        break;
                    case 504:
                        error = "Comillas no estan cerradas";
                        break;
                    case 505:
                        error = "Se espera '=' despues de ':'";
                        break;
                    case 506:
                        error = "Comentario no cerrado";
                        break;
                    case 507:
                        error = "numero decimal no terminado";
                        break;
                }
            }
        }

        private int palabrasReservadas(string textoAlmacenado, int tipo)
        {
            int index;
            index = Array.IndexOf(ArrayReservadas, textoAlmacenado.ToLower());
            switch (index)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                case 17:
                case 18:
                case 19:
                    tipo = 200 + index ;
                    return tipo;
                default:
                    return tipo;
            }
        }

        private Nodo nuevoNodo(string textoAlmacenado, int tipo, int renglon)
        {
            Nodo nodo1 = new Nodo();
            nodo1.txt = textoAlmacenado;
            nodo1.tipo = tipo;
            nodo1.renglon = renglon;
            return nodo1;
        }

    }
}
