//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Util.Security:Pkcs7Type:0:21/May/2008[SAT.DyP.Util.Security:1.0:21/May/2008])
using System;

namespace SAT.DyP.Util.Security
{
    public enum Pkcs7Type
    {
        Signature            = 1,
        Envelope             = 2,
        SignatureAndEnvelope = 3
    };
}
