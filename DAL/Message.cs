using System;
using System.Collections.Generic;

namespace Acme.CarRentalService.DAL
{
    public class Message
    {

        public Message(string typeOfMessage)
        {
            Type = typeOfMessage;

            Parameters = new Dictionary<string, Tuple<object, Type>>();
        }

        public void AddParameter(string name, Tuple<object, Type> value)
        {
            Parameters.Add(name, value);
        }

        public string Type { get; private set; }

        public Dictionary<string, Tuple<object, Type>> Parameters { get; private set; }
    }
}
