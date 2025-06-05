using System;
using System.Collections;
using STycoon.Barcodes.Tools;
using STycoon.Products.Utils;
using Unity.Mathematics;
using Utils;
using Random = System.Random;

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

                return xProduct.barcode.CompareTo(yProduct.barcode);
            }
        }

        public ushort code;
#if UNITY_EDITOR
        public STycoon.Utils.DevID editor_id;
#endif
        public ProductType type;
        [Brand] public ushort brandId;
        /// <summary>
        /// Full Barcode, is public for serialization only
        /// should be readonly on the inspector
        /// </summary>
        [Readonly] public ulong barcode;
        public float3 dimensions;

        public static ushort RandomizeCode()
        {
            Random random = new();
            return (ushort)random.Next(10_000, ushort.MaxValue);
        }

        public bool IsValid() => BarcodeTools.Validate(barcode);
    }
}
