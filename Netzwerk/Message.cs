using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Netzwerk
{
    class Message
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
           nick = array[3];
           status = array[4];
           laenge = array[5];
           body = array[6];
        }
        public Message(String a, String c, String d, String e, String f)
        {
            typ = a;
            if(c != null)
            version = c;

            nick = d;
            status = e;
            body = f;
        }


        public override String ToString()
        {
            return typ + "|" + zeit + "|" + version + "|" + nick + "|" + status + "|" + laenge + "|" + body;
        }

        public String getBody()
        {
            return body;
        }

        public void setZeit()
        {
            zeit = System.DateTime.Now.ToString();
        }




    }
}
