using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitMongoDB
{
    class MongoElement
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("Elemento")]
        public String Elemento { get; set; }
        [BsonElement("NivelDelElemento")]
        public String NivelDelElemento { get; set;  }

        public MongoElement(string elemento, string nivelDelElemento)
        {
            Elemento = elemento;
            NivelDelElemento = nivelDelElemento;
        }
    }
}
