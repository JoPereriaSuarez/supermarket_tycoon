using System;
using System.IO;
using System.Runtime.InteropServices;
using Unity.Profiling;
using UnityEngine;

namespace STycoon.Barcodes
{
    public class TestMono : MonoBehaviour
    {
        private static readonly ProfilerMarker prepareMarker = new("Barcodes.Prepare");
        
        [DllImport("barcode_gen", CallingConvention = CallingConvention.Cdecl)]
        private static extern int generate_barcode(string path, string code);

        public static void GenerateBarcode(string[] codes)
        {
            string path = Path.Combine(Application.dataPath, "barcode.png");
            Debug.Log(path);
            if(generate_barcode(path, codes[0]) != 0)
                Debug.Log($"Error create barcode {codes[0]}");
            else
                Debug.Log($"barcode {codes[0]} success");
        }
        private void Awake()
        {
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
            Console.SetError(new StreamWriter(Console.OpenStandardError()) { AutoFlush = true });
        }

        [SerializeField] private string[] codes;

        [ContextMenu("Gen")]
        private void Gen() => GenerateBarcode(codes);
    }
}
