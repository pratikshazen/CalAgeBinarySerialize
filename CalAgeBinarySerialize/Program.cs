using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

namespace CalAgeBinarySerialize
{
        [Serializable]
        class SerializationClass : IDeserializationCallback
        {
            public int birthYear;   
            [NonSerialized]

            public int age;
            public SerializationClass(int birthYear)
            {
                this.birthYear = birthYear;
            }

            public void OnDeserialization(object sender)
            {
                DateTime dateTime = DateTime.Now;
                age = DateTime.Now.Year - birthYear;
            }
        }
        class Program
        {
            static void Main(string[] args)
            {
                Console.WriteLine("Enter the Year of Birth :");
                int year = int.Parse(Console.ReadLine());

                SerializationClass sc = new SerializationClass(year);
                FileStream fs = new FileStream(@"AgeCal.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);

                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, sc);
                fs.Seek(0, SeekOrigin.Begin);

                SerializationClass s = (SerializationClass)bf.Deserialize(fs);

                Console.WriteLine("Age is : " + s.age);

            }
        }
    }
 