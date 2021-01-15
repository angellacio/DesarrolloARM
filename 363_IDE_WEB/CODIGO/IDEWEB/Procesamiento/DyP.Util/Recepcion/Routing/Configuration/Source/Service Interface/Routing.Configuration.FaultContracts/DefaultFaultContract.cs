//@(#)SCADE2(W:SKDN08221CO2:SAT.DyP.Routing.Configuration.FaultContracts:DefaultFaultContract:0:21/May/2008[SAT.DyP.Routing.Configuration.FaultContracts:1.0:21/May/2008])
using System;

namespace SAT.DyP.Routing.Configuration.FaultContracts {
    [System.Runtime.Serialization.DataContractAttribute(Namespace = "http://www.sat.gob.mx/scade.net/2007_10_07/routing", Name = "DefaultFaultContract")]
    public class DefaultFaultContract {
        int errorId;
        string errorMessage;
        Guid correlationId;

        public DefaultFaultContract()
            : this(-1, String.Empty, Guid.Empty) {
        }

        public DefaultFaultContract(int errorId, string errorMessage, Guid correlationId) {
            this.errorId = errorId;
            this.errorMessage = errorMessage;
            this.correlationId = correlationId;
        }

        [System.Runtime.Serialization.DataMemberAttribute(IsRequired = true, Name = "ErrorId", Order = 1)]
        public int ErrorId {
            get { return errorId; }
            set { errorId = value; }
        }

        [System.Runtime.Serialization.DataMemberAttribute(IsRequired = true, Name = "ErrorMessage", Order = 2)]
        public string ErrorMessage {
            get { return errorMessage; }
            set { errorMessage = value; }
        }

        [System.Runtime.Serialization.DataMemberAttribute(IsRequired = true, Name = "CorrelationId", Order = 3)]
        public Guid CorrelationId {
            get { return correlationId; }
            set { correlationId = value; }
        }
    }
}