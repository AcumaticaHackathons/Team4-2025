using System;
using PX.Data;

namespace ATS20250125
{
  [Serializable]
  [PXCacheName("ATSOrder")]
  public class ATSOrder : PXBqlTable, IBqlTable
  {
    #region Id
    [PXDBIdentity(IsKey = true)]
    public virtual int? Id { get; set; }
    public abstract class id : PX.Data.BQL.BqlInt.Field<id> { }
    #endregion

    #region Type
    [PXDBString(2, IsFixed = true, InputMask = "")]
    [PXUIField(DisplayName = "Type")]
    public virtual string Type { get; set; }
    public abstract class type : PX.Data.BQL.BqlString.Field<type> { }
    #endregion

    #region Json
    [PXDBString(InputMask = "")]
    [PXUIField(DisplayName = "Json")]
    public virtual string Json { get; set; }
    public abstract class json : PX.Data.BQL.BqlString.Field<json> { }
    #endregion

    #region Success
    [PXDBBool()]
    [PXUIField(DisplayName = "Success")]
    public virtual bool? Success { get; set; }
    public abstract class success : PX.Data.BQL.BqlBool.Field<success> { }
    #endregion
  }
}