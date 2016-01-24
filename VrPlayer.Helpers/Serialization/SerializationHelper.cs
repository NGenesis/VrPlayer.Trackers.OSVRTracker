//Source: http://stackoverflow.com/questions/6541543/trying-to-serialize-and-deserialize-entity-object-in-c-sharp
//Source: http://stackoverflow.com/questions/11160470/how-to-assign-complex-profile-values

using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace VrPlayer.Helpers
{
    public class SerializationHelper
    {

        #region xml

        /// <summary>
        /// Serializes an object to Xml as a string.
        /// </summary>
        /// <typeparam name="T">Datatype T.</typeparam>
        /// <param name="toSerialize">Object of type T to be serialized.</param>
        /// <returns>Xml string of serialized type T object.</returns>
        public static string SerializeXml<T>(T toSerialize)
        {
            string xmlstream;

            using (var memstream = new MemoryStream())
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                var xmlWriter = new XmlTextWriter(memstream, Encoding.UTF8);

                xmlSerializer.Serialize(xmlWriter, toSerialize);
                xmlstream = Utf8ByteArrayToString(((MemoryStream)xmlWriter.BaseStream).ToArray());
            }

            return xmlstream;
        }

        /// <summary>
        /// Deserializes Xml string of type T.
        /// </summary>
        /// <typeparam name="T">Datatype T.</typeparam>
        /// <param name="xmlString">Input Xml string from which to read.</param>
        /// <returns>Returns rehydrated object of type T.</returns>
        public static T DeserializeXml<T>(string xmlString)
        {
            T tempObject;

            using (var memoryStream = new MemoryStream(StringToUtf8ByteArray(xmlString)))
            {
                var xs = new XmlSerializer(typeof(T));
                new XmlTextWriter(memoryStream, Encoding.UTF8);

                tempObject = (T)xs.Deserialize(memoryStream);
            }

            return tempObject;
        }

        // Convert Array to String
        private static String Utf8ByteArrayToString(Byte[] arrBytes)
        { return new UTF8Encoding().GetString(arrBytes); }
        
        // Convert String to Array
        private static Byte[] StringToUtf8ByteArray(String xmlString)
        { return new UTF8Encoding().GetBytes(xmlString); }

        #endregion

        #region Json

        public static T DeserializeJson<T>(string json)
        {
            var obj = Activator.CreateInstance<T>();

            if (string.IsNullOrWhiteSpace(json))
                return obj;

            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(json)))
            {
                var serializer = new DataContractJsonSerializer(obj.GetType());
                obj = (T)serializer.ReadObject(ms);

                return obj;
            } // using the memory stream
        } // Deserialize - Method

        public static string SerializeJson<T>(object input)
        {
            string result;
            var ser = new DataContractJsonSerializer(typeof(T));

            using (var ms = new MemoryStream())
            {
                ser.WriteObject(ms, input);
                result = Encoding.Default.GetString(ms.ToArray());
            }

            return result;
        } // Serialize - Method

        #endregion
    }
}
