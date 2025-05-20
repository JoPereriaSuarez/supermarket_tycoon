using System.Runtime.InteropServices;
using UnityEngine;

namespace STycoon.Barcodes
{
    public class TestMono : MonoBehaviour
    {
        [DllImport("libbarcode_plugin.so", CallingConvention = CallingConvention.Cdecl)]
        private static extern int gen();


        private void Start()
        {
            gen();
        }
    }
}
