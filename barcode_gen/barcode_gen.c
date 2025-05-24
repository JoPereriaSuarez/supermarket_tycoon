#include <zint.h>
#include <stdlib.h>
#include <string.h>
#include <stdio.h>


#ifdef __cplusplus
extern "C" {
    #endif
    
    #ifdef _WIN32
    __declspec(dllexport)
    #else
    __attribute__((visibility("default")))
    #endif
    int generate_barcode(char *folder, char* code);
    
    #ifdef __cplusplus
}
#endif

int generate_barcode(char *path, char* code)
{
    struct zint_symbol *file;
    file = ZBarcode_Create();
    if (file == NULL) 
    {
        printf("Failed to create barcode symbol.\n");
        return 1;
    }

    file->symbology = BARCODE_UPCA;
    file->output_options = 0;
    printf("%s\n",path);
    strcpy(file->outfile, path);

    file->scale = 1.0f;  
    const char *barcode_data = code;

    int result = ZBarcode_Encode_and_Print(file, (unsigned char *)barcode_data, 0, 0);
    if (result != 0)
    {
        printf("Failed to encode barcode: %s\n", file->errtxt);
        ZBarcode_Delete(file);
        return 1;
    }

    printf("Barcode image generated: barcode.png\n");
    ZBarcode_Delete(file);
    return 0;
}

int main(int argc, char *argv[])
{
    if (argc != 3) 
    {
        printf("Usage: %s <output_path> <barcode_data>\n", argv[0]);
        return 1;
    }

    char *output_path = argv[1];
    char *barcode_data = argv[2];

    int result = generate_barcode(output_path, barcode_data);
    if (result == 0) 
    {
        printf("Barcode generation succeeded.\n");
    } 
    else 
    {
        printf("Barcode generation failed.\n");
    }

    return result;
}

