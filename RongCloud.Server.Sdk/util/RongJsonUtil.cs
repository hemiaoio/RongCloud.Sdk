using System;
using Newtonsoft.Json;

namespace io.rong.util {
	class RongJsonUtil {
		public static ObjType JsonStringToObj<ObjType>(string JsonString) where ObjType : class {
          	ObjType s = JsonConvert.DeserializeObject<ObjType>(JsonString);
          	return s;
       	}

        public static string ObjToJsonString(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
	}
	
}