//@(#)SCADE2(W:SKDN08221CO2:SAT.DyP.Routing.Configuration.DataAccess:IDomainObjectFactory:0:21/May/2008[SAT.DyP.Routing.Configuration.DataAccess:1.0:21/May/2008])
using System.Data;
using System.Data.Common;
using System.Text;

namespace SAT.DyP.Routing.Configuration.DataAccess {
    /// <summary>
    /// This interface specifies the signature for a factory that
    /// takes a DataReader and creates a domain object from it.
    /// </summary>
    /// <typeparam name="TDomainObject">type of domain object to create.</typeparam>
    public interface IDomainObjectFactory<TDomainObject> {
        TDomainObject Construct(IDataReader reader);
    }
}
