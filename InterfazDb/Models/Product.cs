using System;

namespace InterfazDb.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Descripcion { get; set; }

        public override string ToString()
        {
            return $"{ID} - {Descripcion}";
        }
    }
} 