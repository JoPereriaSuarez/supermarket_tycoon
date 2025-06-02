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
	    Assert.DoesNotThrow(()=> { BarcodeTools.Generate(7, 25272, 47070); });
    }
    [TestCase(7, 100, 100),
    TestCase(10,12345,67890),
    TestCase(0,1234500,6007890)]
    public void barcode_generator_throw(int type, int left, int right)
    {
	    Assert.Throws<ArgumentException>(() =>
	    {
		    BarcodeTools.Generate((byte)type, (uint)left, (uint)right);
	    });
    }
    
    [TestCase(7, 25272, 47070, 725272470701), 
     Description("test a sample valid barcode: the return check digit should be equal")]
    public void barcode_correct_check_digit(int type, int left, int right, long sample)
    {
	    ulong barcode = BarcodeTools.Generate((byte)type, (uint)left, (uint)right);
	    Assert.AreEqual((ulong)sample, barcode);
    }

    [TestCase(123456789012)]
    [TestCase(725272470701)]
    [TestCase(565533629583)]
    [TestCase(365533112364)]
    public void barcode_validates_true(long sample)
    {
	    ulong barcode = (ulong)sample;
	    Assert.IsTrue(BarcodeTools.Validate(barcode));
    }
    [TestCase(123456789013)]
    public void barcode_validates_false(long sample)
    {
	    ulong barcode = (ulong)sample;
	    Assert.IsFalse(BarcodeTools.Validate(barcode));
    }
}
