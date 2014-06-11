using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Orkidea.Porthos.Entities
{
    public class CustomIdentity : IIdentity
    {

        public CustomIdentity(string name, string userData)
        {
            IsAuthenticated = true;
            Name = name;

            string[] userDataArray = userData.Split('|');

            id = int.Parse(userDataArray[0]);
            longName = userDataArray[1];
            AuthenticationType = "Forms";
            admin = bool.Parse(userDataArray[2]);
        }

        public string AuthenticationType { get; private set; }
        public bool IsAuthenticated { get; private set; }
        public string Name { get; private set; }
        
        public int id { get; private set; }
        public string longName { get; set; }
        public bool admin { get; set; }
    }
}
