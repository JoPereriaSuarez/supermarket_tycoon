using System;
using NUnit.Framework;
using STycoon.Barcodes.Barcodes;

[Category("STycoon")]
public class BarcodeTests
{
    [Test]
    public void barcode_generator_doesnt_throw()
    {
	    // valid barcode
	    Assert.DoesNotThrow(()=> { BarcodeGenerator.Generate(7, 25272, 47070); });
    }
    [TestCase(7, 100, 100),
    TestCase(10,12345,67890),
    TestCase(0,1234500,6007890)]
    public void barcode_generator_throw(int type, int left, int right)
    {
	    Assert.Throws<ArgumentException>(() =>
	    {
		    BarcodeGenerator.Generate((byte)type, (uint)left, (uint)right);
	    });
    }
    
    [TestCase(7, 25272, 47070, 725272470701), 
     Description("test a sample valid barcode: the return check digit should be equal")]
    public void barcode_correct_check_digit(int type, int left, int right, long sample)
    {
	    ulong barcode = BarcodeGenerator.Generate((byte)type, (uint)left, (uint)right);
	    Assert.AreEqual((ulong)sample, barcode);
    }
}
