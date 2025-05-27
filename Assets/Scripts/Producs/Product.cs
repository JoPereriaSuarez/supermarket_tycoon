using System;
using System.Collections;
using STycoon.Products.Utils;
using Unity.Mathematics;
using UnityEngine.Serialization;
using Utils;

namespace STycoon.Products
{
    [Serializable]
    public class Product
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

                return xProduct.barcode.CompareTo(yProduct.barcode);
            }
        }

        public ushort code;
        [Brand] public ushort brandId;
        /// <summary>
        /// Full Barcode, is public for serialization only
        /// should be readonly on the inspector
        /// </summary>
        [Readonly] public ulong barcode;
        public float3 dimensions;
    }
}
