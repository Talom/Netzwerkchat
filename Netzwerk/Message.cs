using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Netzwerk
{
    public class Message
    {
        private String typ;
        private String zeit;  
        private String version = "0.0.0.1";
        private String nick;
        private String status;
        private String laenge;
        private String body;
        private char[] delimiter = {'|'}; 

        public Message(String text)
        {
           
           String [] array = text.Split(delimiter, 7);
           typ = array[0];
           zeit = array[1];
           version = array[2];
           status = array[3];
           nick = array[4];
           laenge = array[5];
           body = array[6];
        }
        public Message(String a, String c, String d, String e, String f)
        {
            typ = a;
            if(c != "")
            version = c;

            nick = d;
            status = e;
            body = f;
        }

        private void setLaenge()
        {
            int x = (typ.Length + zeit.Length + version.Length + nick.Length + status.Length + body.Length);
            laenge = x.ToString();
        }

        public override String ToString()
        {
            setZeit();
            setLaenge();
            return typ + "|" + zeit + "|" + version + "|" + status + "|" + nick + "|" + laenge + "|" + body;
        }

        public String getBody()
        {
            return body;
        }

        public void setZeit()
        {
            DateTime date1 = new DateTime(1970, 1, 1);  //Refernzdatum (festgelegt)
            DateTime date2 = DateTime.Now;              //jetztiges Datum / Uhrzeit
            TimeSpan ts = new TimeSpan(date2.Ticks - date1.Ticks);  // das Delta ermitteln
            zeit = Convert.ToInt32(ts.TotalSeconds).ToString();

        }
        public String getZeit()
        {
            if (zeit != "")
            {
                return zeit;
            }
            else 
            {
                setZeit();
                return zeit;
            }
        }
        public String getUser()
        {
            return nick;
        }




    }
}
