using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TSI.GymTech.OnOff
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Pega o nome do processo deste programa
            string nomeProcesso = Process.GetCurrentProcess().ProcessName;

            //Busca os processos com este nome que estão em execução
            Process[] processos = Process.GetProcessesByName(nomeProcesso);

            //Se já houver um aberto
            if (processos.Length > 1)
            {
                //Mostra mensagem de erro e finaliza
                MessageBox.Show("Não é possível abrir duas instâncias deste programa.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Application.Exit();
            }
            //Caso contrário continue normalmente
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
        }
    }
}
