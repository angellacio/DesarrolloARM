//@(#)SCADE2(W:SKDN08221CO2:SAT.DyP.Routing.Configuration.DataAccess:IDeleteFactory:0:21/May/2008[SAT.DyP.Routing.Configuration.DataAccess:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

namespace SAT.DyP.Routing.Configuration.DataAccess {
    public interface IDeleteFactory<TIdentityObject> : IDbToBusinessEntityNameMapper {
        DbCommand ConstructDeleteCommand(Database db, TIdentityObject identity);
    }
}
