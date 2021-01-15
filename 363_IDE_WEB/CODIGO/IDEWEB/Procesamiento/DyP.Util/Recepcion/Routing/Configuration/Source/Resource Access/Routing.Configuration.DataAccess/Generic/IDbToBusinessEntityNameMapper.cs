//@(#)SCADE2(W:SKDN08221CO2:SAT.DyP.Routing.Configuration.DataAccess:IDbToBusinessEntityNameMapper:0:21/May/2008[SAT.DyP.Routing.Configuration.DataAccess:1.0:21/May/2008])
using System;
namespace SAT.DyP.Routing.Configuration.DataAccess {
    public interface IDbToBusinessEntityNameMapper {
        string MapDbParameterToBusinessEntityProperty(string dbParameter);
    }
}
