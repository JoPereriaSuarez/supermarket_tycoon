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
int gen();

#ifdef __cplusplus
}
#endif

int gen() 
{
    struct zint_symbol *file;

    file = ZBarcode_Create();
    if (file == NULL) 
    {
        printf("Failed to create barcode symbol.\n");
        return 1;
    }

    file->symbology = BARCODE_UPCA;

    strcpy(file->outfile, "barcode_3.png");

    file->scale = 1.0f;  
    const char *barcode_data = "012345678912";

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

