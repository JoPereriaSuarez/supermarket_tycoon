using System;
using System.Collections;
using Unity.Mathematics;

namespace STycoon.Products
{
    [Serializable]
    public struct Product
    {
        internal class Comparer : IComparer
        {
            public int Compare(object x, object y)
            {
                if (ReferenceEquals(x, y))
                    return 0;
                
                if(x is not Product xProduct)
                    throw new NullReferenceException(nameof(xProduct));
                if(y is not Product yProduct)
                    throw new NullReferenceException(nameof(yProduct));

                return xProduct.code.CompareTo(yProduct.code);
            }
        }
        
        public ushort code;
        public float3 dimensions;
        public uint price;
    }
}
