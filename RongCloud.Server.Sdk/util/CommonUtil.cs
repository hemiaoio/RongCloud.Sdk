using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RongCloud.Server.models.response;
using RongCloud.Server.models.user;

namespace RongCloud.Server.util
{
    public class CommonUtil

    {
        public static string VERIFY_JSON_NAME = "/verify.json";
        public static string API_JSON_NAME = "/api.json";
        public static string CHRARCTER = "UTF-8";

        public static bool ValidateParams(object paramObj, int length)
        {
            try
            {
                if (null == paramObj)
                {
                    return false;
                }

                if (paramObj.GetType() == typeof(string[]))
                {
                    string[] param = (string[]) paramObj;
                    int len = param.Length;
                    if (len <= length)
                    {
                        return true;
                    }
                }
                else if (paramObj is string param1)
                {
                    int len = param1.Length;
                    if (len <= length)
                    {
                        return true;
                    }
                }
                else if (paramObj is int)
                {
                    int param = (int) paramObj;
                    if (param <= length)
                    {
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("长度校验错误" + e.Message);
            }

            return false;
        }

        /**
         * 从文件读取 json 对象
         * 
         * @param path  文件路径
         * 
         * @return Jobj
         **/
        private static JObject FromPath(string path)
        {
            StreamReader file =
                File.OpenText(Path.Combine(AppContext.BaseDirectory, "jsonsource", path));
            JsonTextReader reader = new JsonTextReader(file);
            JObject jObject = (JObject) JToken.ReadFrom(reader);

            //reader.Close();

            return jObject;
        }

        private static List<string> GetKeys(JToken jToken)
        {
            Dictionary<string, object> dictObj = jToken.ToObject<Dictionary<string, object>>();

            return dictObj.Keys.ToList();
        }

        private static Dictionary<string, Dictionary<string, string>> GetMessages(JToken jToken)
        {
            Dictionary<string, Dictionary<string, string>> dictObj =
                jToken.ToObject<Dictionary<string, Dictionary<string, string>>>();
            Dictionary<string, Dictionary<string, string>> messageSet =
                new Dictionary<string, Dictionary<string, string>>();
            foreach (var item in dictObj)
            {
                Dictionary<string, string> msg = new Dictionary<string, string>();
                foreach (var dict in item.Value)
                {
                    msg.Add(dict.Key, dict.Value);
                }

                messageSet.Add(item.Key, msg);
            }

            return messageSet;
        }

        /**
         * 参数校验方法
         *
         * @param model  校验对象
         * @param path   路径
         * @param method 需要校验方法
         *
         * @return String
         **/
        public static string CheckFiled(object model, string path, string method)
        {
            try
            {
                string code = "200";
                int max = 64;
                //api.json 的路径
                string apiPath = path;
                string type = "";
                if (path.Contains("/"))
                {
                    path = path.Substring(0, path.IndexOf("/", StringComparison.Ordinal));
                }

                string[] fileds = { };
                string checkObjectKey = "";
                //获取需要校验的参数
                Dictionary<string, string[]> checkInfo = GetCheckInfo(apiPath, method);
                foreach (var item in checkInfo)
                {
                    checkObjectKey = item.Key;
                    fileds = item.Value;
                }

                //获取校验文件
                JObject verify = FromPath(path + VERIFY_JSON_NAME);

                //获取校验key
                List<string> keys = GetKeys(verify.GetValue(checkObjectKey));
                //获取具体校验规则

                JObject entity = (JObject) verify.GetValue(checkObjectKey);
                //Dictionary<String, Object> entity = verify.GetValue(checkObjectKey).ToObject<Dictionary<String, Object>>();
                foreach (string name in fileds)
                {
                    foreach (string key in keys)
                    {
                        if (name.Equals(key))
                        {
                            //将属性的首字符大写，方便构造get，set方法
                            string nameTemp = name.Substring(0, 1).ToUpper() + name.Substring(1);
                            //获取属性的类型
                            //String type = field.getGenericType().ToString();

                            MethodInfo m = model.GetType().GetMethod("Get" + nameTemp);
                            PropertyInfo propertyInfo = model.GetType().GetProperty(nameTemp);
                            //.GetValue(model, null)

                            //获取字段的具体校验规则
                            JObject obj = (JObject) entity.GetValue(name);
                            if (obj.GetValue("require") != null)
                            {
                                bool must = (bool) ((JObject) obj.GetValue("require")).GetValue("must");
                                var result = null != m
                                    ? m.Invoke(model, new object[] { })
                                    : propertyInfo.GetValue(model, null);

                                if (result is string value1)
                                {
                                    if (string.IsNullOrEmpty(value1))
                                    {
                                        code = (string) ((JObject) obj.GetValue("require")).GetValue("invalid");
                                    }
                                }
                                else
                                {
                                    var value = result;
                                    if (null == value)
                                    {
                                        code = (string) ((JObject) obj.GetValue("require")).GetValue("invalid");
                                    }
                                }
                            }

                            if (obj.GetValue("length") != null)
                            {
                                max = (int) ((JObject) obj.GetValue("length")).GetValue("max");
                                var result = null != m
                                    ? m.Invoke(model, new object[] { })
                                    : propertyInfo.GetValue(model, null);
                                if (result is string value1)
                                {
                                    if ("200".Equals(code) && string.IsNullOrEmpty(value1))
                                    {
                                        code = (string) ((JObject) obj.GetValue("length")).GetValue("invalid");
                                    }

                                    if ("200".Equals(code) && value1.Length > max)
                                    {
                                        code = (string) ((JObject) obj.GetValue("length")).GetValue("invalid");
                                    }
                                }
                                else if (null != result && result.GetType() == typeof(string[]))
                                {
                                    string[] value = (string[]) result;
                                    if ("200".Equals(code) && value.Length > max)
                                    {
                                        code = (string) ((JObject) obj.GetValue("length")).GetValue("invalid");
                                    }
                                }
                            }

                            if (obj.GetValue("size") != null)
                            {
                                max = (int) ((JObject) obj.GetValue("size")).GetValue("max");
                                type = (string) ((JObject) obj.GetValue("typeof")).GetValue("type");
                                if (type.Contains("array"))
                                {
                                    object[] value;
                                    if (null != m)
                                    {
                                        value = (object[]) m.Invoke(model, new object[] { });
                                    }
                                    else
                                    {
                                        value = (object[]) propertyInfo.GetValue(model, null);
                                    }

                                    if ("200".Equals(code) && null == value)
                                    {
                                        code = (string) ((JObject) obj.GetValue("size")).GetValue("invalid");
                                    }

                                    if ("200".Equals(code) && value.Length > max)
                                    {
                                        code = (string) ((JObject) obj.GetValue("size")).GetValue("invalid");
                                    }
                                }
                                else if (type.Contains("int"))
                                {
                                    int value = 0;
                                    try
                                    {
                                        if (null != m)
                                        {
                                            value = (int) m.Invoke(model, new object[] { });
                                        }
                                        else
                                        {
                                            value = (int) propertyInfo.GetValue(model, null);
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        code = (string) ((JObject) obj.GetValue("typeof")).GetValue("invalid");
                                    }

                                    if ("200".Equals(code) && 0 == value)
                                    {
                                        code = (string) ((JObject) obj.GetValue("size")).GetValue("invalid");
                                    }

                                    if ("200".Equals(code) && value > max)
                                    {
                                        code = (string) ((JObject) obj.GetValue("size")).GetValue("invalid");
                                    }
                                }
                            }

                            if (!"200".Equals(code))
                            {
                                //根据错误码获取错误信息
                                string message = (string) GetErrorMessage(apiPath, method, code, name, max.ToString(),
                                    "1", type);
                                //对 errorMessage  替换
                                message = message.Replace("errorMessage", "msg");
                                return message;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }

            return null;
        }

        /**
         * 获取校验信息
         *
         * @param path   路径 （获取校验文件路径）
         * @param method 校验方法（需要校验的方法）
         *
         * @return Map
         **/
        public static Dictionary<string, string[]> GetCheckInfo(string path, string method)
        {
            try
            {
                var api = FromPath(path + API_JSON_NAME);
                List<string> keys = GetKeys(((JObject) api.GetValue(method)).GetValue("params"));
                //ISet<String> keys = api.GetValue(method).GetValue("params").keySet();
                string key = keys[0];
                if (string.IsNullOrEmpty(key))
                {
                    return null;
                }

                List<string> subkeys;

                JToken obj = ((JObject) ((JObject) api.GetValue(method)).GetValue("params")).GetValue(key);

                if (null != obj)
                {
                    subkeys = GetKeys(obj);
                }
                else
                {
                    subkeys = keys;
                }

                Dictionary<string, string[]> map = new Dictionary<string, string[]>
                {
                    {key, subkeys.ToArray()}
                };
                return map;
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }


        /**
         * 获取错误信息
         *
         * @param path   路径 （获取校验文件路径）
         * @param method 校验方法（需要校验的方法）
         * @param errorCode 错误码
         * @param name  具体字段名
         * @param max   字段需要的最大值
         * @param min   字段的最小值
         * @param type  类型
         *
         * @return Map
         **/
        public static object GetErrorMessage(string path, string method, string errorCode, string name, string max,
            string min, string type)
        {
            try
            {
                var api = FromPath(path + API_JSON_NAME);
                Dictionary<string, Dictionary<string, string>> messages =
                    GetMessages(((JObject) ((JObject) api.GetValue(method)).GetValue("response")).GetValue("fail"));
                string[] searchList = {"{{name}}", "{{max}}", "{{name}}", "{{min}}", "{{currentType}}"};
                string[] replaceList = {name, max, name, min, type};
                foreach (var item in messages)
                {
                    if (errorCode.Equals(item.Key))
                    {
                        string text = JsonConvert.SerializeObject(item.Value);
                        //StringUtils.replaceEach(text,serchList,replaceList);
                        for (int i = 0; i < searchList.Length; i++)
                        {
                            text = text.Replace(searchList[i], replaceList[i]);
                        }

                        return text;
                    }
                }
            }
            catch (IOException e)
            {
                throw e;
            }

            return null;
        }

        /**
         * 参数校验
         *
         * @param checkFiled 需要校验的字段
         * @param value  传入参数值
         * @param path   路径 （获取校验文件路径）
         * @param method 需要校验方法
         *
         * @return String
         **/
        public static string CheckParam(string checkFiled, object value, string path, string method)
        {
            try
            {
                string code = "200";
                int max = 64;
                string type = "";
                string apiPath = path;
                if (path.Contains("/"))
                {
                    path = path.Substring(0, path.IndexOf("/", StringComparison.Ordinal));
                }

                //String[] fileds = {};
                string checkObject = "";
                //获取需要校验的key
                Dictionary<string, string[]> checkInfo = GetCheckInfo(apiPath, method);
                foreach (var item in checkInfo)
                {
                    //fileds = entry.getValue();
                    checkObject = item.Key;
                }

                JToken verify = FromPath(path + VERIFY_JSON_NAME);
                List<string> keys = GetKeys(((JObject) verify).GetValue(checkObject));
                JObject entity = (JObject) ((JObject) verify).GetValue(checkObject);
                foreach (string key in keys)
                {
                    if (checkFiled.Equals(key))
                    {
                        JObject obj = (JObject) entity.GetValue(checkFiled);
                        if (obj.GetValue("require") != null)
                        {
                            bool must = (bool) ((JObject) obj.GetValue("require")).GetValue("must");
                            if (value is string)
                            {
                                if (string.IsNullOrEmpty(value.ToString()))
                                {
                                    code = (string) ((JObject) obj.GetValue("require")).GetValue("invalid");
                                }
                            }
                            else
                            {
                                if (null == value)
                                {
                                    code = (string) ((JObject) obj.GetValue("require")).GetValue("invalid");
                                }
                            }
                        }

                        if (obj.GetValue("length") != null)
                        {
                            max = (int) ((JObject) obj.GetValue("length")).GetValue("max");
                            if (value is string)
                            {
                                if ("200".Equals(code) && string.IsNullOrEmpty(value.ToString()))
                                {
                                    code = (string) ((JObject) obj.GetValue("length")).GetValue("invalid");
                                }

                                if ("200".Equals(code) && value.ToString().Length > max)
                                {
                                    code = (string) ((JObject) obj.GetValue("length")).GetValue("invalid");
                                }
                            }
                            else if (value.GetType() == typeof(string[]))
                            {
                                string[] valueTemp = { };
                                try
                                {
                                    valueTemp = (string[]) value;
                                }
                                catch (Exception e)
                                {
                                    code = (string) ((JObject) obj.GetValue("typeof")).GetValue("invalid");
                                }

                                if ("200".Equals(code) && valueTemp.Length > max)
                                {
                                    code = (string) ((JObject) obj.GetValue("length")).GetValue("invalid");
                                }
                            }
                        }

                        if (obj.GetValue("size") != null)
                        {
                            max = (int) ((JObject) obj.GetValue("size")).GetValue("max");
                            type = (string) ((JObject) obj.GetValue("typeof")).GetValue("type");
                            if (type.Contains("array"))
                            {
                                string[] valueTemp = null;
                                if ("200".Equals(code) && null == value)
                                {
                                    code = (string) ((JObject) obj.GetValue("size")).GetValue("invalid");
                                }

                                try
                                {
                                    valueTemp = (string[]) value;
                                }
                                catch (Exception e)
                                {
                                    code = (string) ((JObject) obj.GetValue("typeof")).GetValue("invalid");
                                }

                                if ("200".Equals(code) && valueTemp.Length > max)
                                {
                                    code = (string) ((JObject) obj.GetValue("size")).GetValue("invalid");
                                }
                            }
                            else if (type.Contains("int"))
                            {
                                int valueTemp = 64;
                                try
                                {
                                    if (value != null) valueTemp = (int) value;
                                }
                                catch (Exception e)
                                {
                                    code = (string) ((JObject) obj.GetValue("typeof")).GetValue("invalid");
                                }

                                if ("200".Equals(code) && null == value)
                                {
                                    code = (string) ((JObject) obj.GetValue("size")).GetValue("invalid");
                                }

                                if ("200".Equals(code) && valueTemp > max)
                                {
                                    code = (string) ((JObject) obj.GetValue("size")).GetValue("invalid");
                                }
                            }
                        }

                        string message = (string) GetErrorMessage(apiPath, method, code, checkFiled, max.ToString(),
                            "1", type);
                        if (null != message)
                        {
                            message = message.Replace("errorMessage", "msg");
                            return message;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }

            return null;
        }

        /**
         * 获取response信息
         *
         * @param path   路径 （获取校验文件路径）
         * @param method 校验方法（需要校验的方法）
         * @param response 返回信息
         *
         * @return String
         **/
        public static string GetResponseByCode(string path, string method, string response)
        {
            try
            {
                JObject obj = (JObject) JToken.Parse(response);
                string code = obj.GetValue("code").ToString();
                var api = FromPath(path + API_JSON_NAME);
                Dictionary<string, Dictionary<string, string>> messages =
                    GetMessages(((JObject) ((JObject) api.GetValue(method)).GetValue("response")).GetValue("fail"));
                string text = response;
                if (code.Equals("200"))
                {
                    if (path.Contains("blacklist") && method.Equals("getList"))
                    {
                        UserList userList = JsonConvert.DeserializeObject<UserList>(response);
                        List<UserModel> users = new List<UserModel>();
                        foreach (string id in userList.getUsers())
                        {
                            UserModel tmpUser = new UserModel
                            {
                                Id = id
                            };
                            users.Add(tmpUser);
                        }

                        UserModel[] members = users.ToArray();

                        BlackListResult blacklist = new BlackListResult(userList.getCode(), null, members);

                        text = blacklist.ToString();
                    }
                    else if (path.Contains("whitelist/user") && method.Equals("getList"))
                    {
                        UserList userList = JsonConvert.DeserializeObject<UserList>(response);
                        //User[] members = {};
                        List<UserModel> users = new List<UserModel>();
                        foreach (string id in userList.getUsers())
                        {
                            users.Add(new UserModel() {Id = id});
                        }

                        UserModel[] members = users.ToArray();
                        WhiteListResult whitelist = new WhiteListResult(userList.getCode(), null, members);

                        text = whitelist.ToString();
                    }
                    else if (path.Contains("chatroom") || path.Contains("group"))
                    {
                        text = response.Replace("users", "members");
                        if (text.Contains("whitlistMsgType"))
                        {
                            text = text.Replace("whitlistMsgType", "objNames");
                        }

                        if (path.Contains("gag") || path.Contains("block"))
                        {
                            text = text.Replace("userId", "id");
                        }
                    }
                    else if (path.Contains("user"))
                    {
                        if (path.Contains("block") || path.Contains("blacklist"))
                        {
                            text = response.Replace("userId", "id");
                        }
                    }
                    else if (path.Contains("sensitiveword"))
                    {
                        text = response.Replace("word", "keyword");
                        if (text.Contains("keywords"))
                        {
                            text = text.Replace("keywords", "words");
                        }

                        text = text.Replace("replaceWord", "replace");
                    }
                    else
                    {
                        text = response;
                    }

                    return text;
                }
                else
                {
                    foreach (var item in messages)
                    {
                        if (code.Equals(item.Key))
                        {
                            text = JsonConvert.SerializeObject(item.Value);
                            //text = StringUtils.replace(text,"msg","errorMessage");
                            text = text.Replace("errorMessage", "msg");

                            return text;
                        }
                    }

                    text = response.Replace("errorMessage", "msg");
                    if (path.Contains("chatroom"))
                    {
                        text = text.Replace("users", "members");
                        //对于 聊天室保活成功返回的code是0 更改统一返回200
                        if (path.Contains("keepalive") && "0".Equals(code))
                        {
                            text = text.Replace("chatroomIds", "chatrooms");
                            text = text.Replace("0", "200");
                        }
                    }

                    return text;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("-------------" + e.Message);
            }

            return response;
        }
    }
}