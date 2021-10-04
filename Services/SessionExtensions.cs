using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSSAProject.Services
{
    public static class SessionExtensions
    {
        //503.  Create 2 methods to turn complex data into jason
        public static void SetJson(this ISession session, string key, object value)
        {
            //504.  Call setstring method for serialization
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        //503.  Create a method to get json to return a type for deserialization
        public static T GetJson<T>(this ISession session, string key)
        {
            var sessionData = session.GetString(key);
            return sessionData == null ? default(T) : JsonConvert.DeserializeObject<T>(sessionData);
            //504.  If session is null, return default, if not, deserialize
        }
        //505.  Go to CartControllers
    }
}
