using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuiasEmpacadora
{
    public class Init
    {
        public static void Main(string[] args)
        {

            ConnectEndpoints Guias = null;
            try
            {
                DateTime ProcessDay = DateTime.Now;
                Guias = new ConnectEndpoints(true, "www.santa-priscila.com.ec", "192.168.8.51", "IPSPGuias","UserDesarrollo", "UserDesarrollo");
                Guias.Connect("api", "ipspProfremar", "7cx2CK%#", ProcessDay);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in [" + nameof(Guias) + "]..." +
                    "\nDownloaded: ¿Guias? " + Guias.infoGuiasDownloaded +
                    "\nSaved: ¿Guias? " + Guias.infoGuiasSaved);

            }
            Console.ReadLine();

        }
    }
}
