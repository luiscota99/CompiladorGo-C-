using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompiladorGO
{
    public partial class Form1 : Form
    {
        public Lexico ChecarLexico;
        public Sintatico ChecarSintaxis;
        public Ensamblador Ensamblar;
        public Form1()
        {
            ChecarLexico = new Lexico();
            ChecarSintaxis = new Sintatico();
            Ensamblar = new Ensamblador();
            InitializeComponent();
        }

        private void btnChecar_Click(object sender, EventArgs e)
        {
            //txtCodigo.Text= File.ReadAllText(@"C:\Users\Aldo\Desktop\compi\Codigo.go");
            ChecarLexico.LeerCaracteres(txtCodigo.Text);
            if(!ChecarLexico.error.Equals(""))
            {
                MessageBox.Show(ChecarLexico.error);
            }
            else
            {
                ChecarSintaxis.BloqueGO(ChecarLexico.Nodos);
                if (!ChecarSintaxis.error.Equals(""))
                {
                    MessageBox.Show(ChecarSintaxis.error);
                    
                }
                else
                {
                    string parte1 = "INCLUDE macros.mac"+ Environment.NewLine +
                    "DOSSEG" + Environment.NewLine +
                    ".MODEL SMALL" + Environment.NewLine +
                    "STACK 100h" + Environment.NewLine +
                    ".DATA" + Environment.NewLine +
            "BUFFER      DB 8 DUP('$'); 23h" + Environment.NewLine +
          "BUFFERTEMP  DB 8 DUP('$'); 23h" + Environment.NewLine +
          "n_line DB 0AH,0DH,\"$\""  + Environment.NewLine +

            "BLANCO DB '#'" + Environment.NewLine +

           "BLANCOS DB '$'" + Environment.NewLine +

            "MENOS DB '-$'" + Environment.NewLine +

            "COUNT DW 0" + Environment.NewLine +

            "NEGATIVO DB 0" + Environment.NewLine +

            "ARREGLO DW 0" + Environment.NewLine +

            "ARREGLO1 DW 0" + Environment.NewLine +

            "ARREGLO2 DW 0" + Environment.NewLine +

            "LISTAPAR LABEL BYTE" + Environment.NewLine +
         "LONGMAX DB 254" + Environment.NewLine +

            "TOTCAR DB ?" + Environment.NewLine +

            "INTRODUCIDOS DB 254 DUP('$')" + Environment.NewLine +

            "MULT10 DW 1" + Environment.NewLine +

            "t1 dw ?" + Environment.NewLine +

            "t2 dw ?" + Environment.NewLine +

            "t3 dw ?" + Environment.NewLine +

            "temp dw ?" + Environment.NewLine +

            "num1 dw 0" + Environment.NewLine +

            "num2 dw 0" + Environment.NewLine +

            "num3 dw 0" + Environment.NewLine +

            "num4 dw 0" + Environment.NewLine +

            "ten dw 10" + Environment.NewLine +

            "cadena db 'Hola Mundo','$'" + Environment.NewLine +
                       "RESULT DB 6 DUP('$')" + Environment.NewLine +

            "TMP1 DW ?" + Environment.NewLine+"";

                    string variables = Ensamblar.EscribirVariables(ChecarSintaxis.Nodos);

                    string parte2 = ".CODE" + Environment.NewLine +
".386" + Environment.NewLine +
"BEGIN:" + Environment.NewLine +
                    "MOV AX, @DATA" + Environment.NewLine +

            "MOV DS, AX" + Environment.NewLine +
"CALL COMPI" + Environment.NewLine +

            "MOV AX, 4C00H" + Environment.NewLine +
            "INT 21H" + Environment.NewLine +
"COMPI  PROC" + Environment.NewLine;

                    string codigo = Ensamblar.EscribirCodigo(ChecarSintaxis.listapolish);
                    variables += Ensamblar.masVar;
                    string parte3 = "ret" + Environment.NewLine +
"COMPI ENDP" + Environment.NewLine +
"END BEGIN" + Environment.NewLine;

                    string texto = parte1 + variables + parte2 + codigo + parte3;
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"c:\tasm\MyTest.asm", true))
                    {
                        file.WriteLine(texto);
                    }
                }
            }
            string cadena="";
            /*
            foreach(int Nombre in ChecarSintaxis.postfijo)
            {
                cadena += Nombre + "  ";        
            }*/
            //MessageBox.Show(cadena);
            foreach (Nodo nodo in ChecarLexico.Nodos)
            {
                dgvData.Rows.Add(nodo.txt, nodo.tipo, nodo.renglon);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dgvData.Rows.Clear();
            foreach (ElementoPolish elemento in ChecarSintaxis.listapolish)
            {
                dgvData.Rows.Add(elemento.Todo, elemento.Etiqueta, elemento.Tipo);
            }
        }
    }
}
