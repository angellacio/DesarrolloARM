//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Util.Security:EncryptionAlgorithm:0:21/May/2008[SAT.DyP.Util.Security:1.0:21/May/2008])
using System;

namespace SAT.DyP.Util.Security
{
    public enum EncryptionAlgorithm
    {
        DES_CBC       = 100,    // DES-CCB (Cipher Block Chaining)
        DES_EDE       = 101,    // DES-EDE
        DES_EDE3      = 102,    // Triple DES-EDE3
        DES_CFB       = 103,    // DES - Cipher Feedback
        DES_EDE_CFB   = 104,    // Triple DES - 2 keys - Cipher Feedback
        DES_EDE3_CFB  = 105,    // Triple DES - 3 keys - Cipher Feedback
        DES_OFB       = 106,    // DES - Output Feedback
        DES_EDE_OFB   = 107,    // Triple DES - 2 keys - Output Feedback
        DES_EDE3_OFB  = 108,    // Triple DES - 3 keys - Output Feedback
        DES_EDE_CBC   = 109,    // Triple DES - 2 keys - Cipher Block Chaining
        DES_EDE3_CBC  = 110,    // Triple DES - 3 keys - Cipher Block Chaining
        DESX_CBC      = 111,    // DES - Cipher Block Chaining (against brute force attacks...)
        RC2_CBC       = 112,    // RC2 - Cipher Block Chaining
        RC2_CFB       = 113,    // RC2 - Cipher Feedback
        RC2_OFB       = 114,    // RC2 - Output Feedback
        RC4           = 115,    // RC4
        RC4_40        = 116,    // RC4
        IDEA_CBC      = 117,    // IDEA - Cipher Block Chaining
        IDEA_CFB      = 118,    // IDEA - Cipher Feedback
        IDEA_OFB      = 119,    // IDEA - Output Feedback
        BF_CBC        = 120,    // BlowFish - Cipher Block Chaining
        BF_CFB        = 121,    // BlowFish - Cipher Feedcack
        BF_OFB        = 122,    // BlowFish - Output Feedback
        CAST5_CBC     = 123,    // CAST5 - Cipher Block Chaining
        CAST5_CFB     = 124,    // CAST5 - Cipher Feedback
        CAST5_OFB     = 125,    // CAST5 - Output Feedback
        RC5_CBC       = 126,    // RC5 - Cipher Block Chaining
        RC5_CFB       = 127,    // RC5 - cipher Feedback
        RC5_OFB       = 128,    // RC5 - Output Feedback
        AES_128_CBC   = 129,    // AES 128 - Cipher Block Chaining
        AES_128_CFB   = 130,    // AES 128 - Cipher Feedback
        AES_128_OFB   = 131,    // AES 128 - Output Feedback
        AES_192_CBC   = 132,    // AES 192 - Cipher Block Chaining
        AES_192_CFB   = 133,    // AES 192 - Cipher Feedback
        AES_192_OFB   = 134,    // AES 192 - Output Feedback
        AES_256_CBC   = 135,    // AES 256 - Cipher Block Chaining
        AES_256_CFB   = 136,    // AES 256 - Cipher Feedback
        AES_256_OFB   = 137     // AES 256 - Output Feedback
    };
}
