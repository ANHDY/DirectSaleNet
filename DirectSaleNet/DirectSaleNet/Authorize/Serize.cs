using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
namespace DirectSaleNet.Authorize
{
    public class Serize<T> where T:class
    {
        //對象字节之间相互转换
        public static byte[] ObjectToByte(T obj)
        {
            string str = JsonConvert.SerializeObject(obj);
            return Encoding.UTF8.GetBytes(str);
        }
        public static T ByteToObject(byte[] bytes)
        {
            string str = Encoding.UTF8.GetString(bytes);
            return JsonConvert.DeserializeObject<T>(str);
        }
    }
}
