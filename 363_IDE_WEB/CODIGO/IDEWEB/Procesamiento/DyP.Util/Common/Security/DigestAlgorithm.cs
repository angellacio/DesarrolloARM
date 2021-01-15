//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Util.Security:DigestAlgorithm:0:21/May/2008[SAT.DyP.Util.Security:1.0:21/May/2008])
using System;

namespace SAT.DyP.Util.Security
{
    public enum DigestAlgorithm
    {
        MD5     =  0,   // MD5
        MD4     =  1,   // MD4
        MD2     =  2,   // MD2
        SHA     =  3,   // SHA
        SHA1    =  4,   // SHA1
        RIPE160 =  5,   // RIPEMD160
        SHA224  =  6,   // SHA 224 bits
        SHA256  =  7,   // SHA 256 bits
        SHA384  =  8,   // SHA 385 bits
        SHA512  =  9,   // SHA 512 bits
        MDC2    = 10    // MDC2 128 bits
    };
}
