//@(#)SCADE2(W:SKDN08221CO2:SAT.DyP.Routing.Configuration.DataAccess:ISelectionFactory:0:21/May/2008[SAT.DyP.Routing.Configuration.DataAccess:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace SAT.DyP.Routing.Configuration.DataAccess {
    /// <summary>
    /// This interface provides the signature for the
    /// class that converts a given IdentityObject into
    /// a DbCommand to do a search for the corresponding
    /// result set.
    /// </summary>
    /// <typeparam name="TIdentityObject">Type that identifies the
    /// items to search for.</typeparam>
    public interface ISelectionFactory<TIdentityObject> : IDbToBusinessEntityNameMapper {
        DbCommand ConstructSelectCommand(Database db, TIdentityObject idObject);
    }
}
