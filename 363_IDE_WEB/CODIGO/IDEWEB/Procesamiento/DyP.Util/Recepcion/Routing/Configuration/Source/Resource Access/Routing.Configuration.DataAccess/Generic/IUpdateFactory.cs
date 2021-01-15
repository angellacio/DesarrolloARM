//@(#)SCADE2(W:SKDN08221CO2:SAT.DyP.Routing.Configuration.DataAccess:IUpdateFactory:0:21/May/2008[SAT.DyP.Routing.Configuration.DataAccess:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace SAT.DyP.Routing.Configuration.DataAccess {
    public interface IUpdateFactory<TDomainObject> : IDbToBusinessEntityNameMapper {
        DbCommand ConstructUpdateCommand(Database db, TDomainObject domainObject);
    }
}
