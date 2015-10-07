using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace ExerciseViewModels
{
    public class ViewModelUtils
    {
        /// <summary>
        /// Serializer
        /// </summary>
        /// <param name="inObject">Object to be serialized.</param>
        /// <returns>Serialized Object in byte array format.</returns>
        public static byte[] Serializer(Object inObject)
        {
            byte[] byteArrayObject;
            BinaryFormatter frm = new BinaryFormatter();
            MemoryStream strm = new MemoryStream();
            frm.Serialize(strm, inObject);
            byteArrayObject = strm.ToArray();
            return byteArrayObject;
        }

        public static Object Deserializer(byte[] byteArrayIn)
        {
            BinaryFormatter frm = new BinaryFormatter();
            MemoryStream strm = new MemoryStream(byteArrayIn);
            return frm.Deserialize(strm);
        }
    }
}
