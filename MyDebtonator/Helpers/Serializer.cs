using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using MyDebtonator.Models;

namespace MyDebtonator.Helpers
{
    public class Serializer
    {
        #region Constructor

        public Serializer() { }

        #endregion

        #region Methods

        public void SerializeRepository(string fileName, UserRepository userRepository)
        {
            using (Stream stream = File.Open(fileName, FileMode.OpenOrCreate))
            {
                using (DeflateStream compressedStream = new DeflateStream(stream, CompressionMode.Compress))
                {
                    BinaryFormatter bFormatter = new BinaryFormatter();
                    bFormatter.Serialize(compressedStream, userRepository);
                }
            }
        }

        public UserRepository DeserializeRepository(string fileName)
        {
            UserRepository userRepository = new UserRepository();

            using (Stream stream = File.Open(fileName, FileMode.OpenOrCreate))
            {
                using (DeflateStream decompressStream = new DeflateStream(stream, CompressionMode.Decompress))
                {
                    BinaryFormatter bFormatter = new BinaryFormatter();

                    if (stream.Length > 0)
                    {
                        userRepository = (UserRepository)bFormatter.Deserialize(decompressStream);
                    }
                }
            }

            return userRepository;
        }

        #endregion
    }
}
