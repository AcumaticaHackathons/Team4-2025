using System;
using PX.Data;

namespace ATS20250125
{
    [Serializable]
    [PXCacheName("ATSOrderLog")]
    public class ATSOrderLog : PXBqlTable, IBqlTable
    {
        #region Module
        [PXDBString(2, IsFixed = true, InputMask = "")]
        [PXUIField(DisplayName = "Module")]
        public virtual string Module { get; set; }
        public abstract class module : PX.Data.BQL.BqlString.Field<module> { }
        #endregion

        #region DocNbr
        [PXDBString(20, IsFixed = true, InputMask = "")]
        [PXUIField(DisplayName = "Doc Nbr")]
        public virtual string DocNbr { get; set; }
        public abstract class docNbr : PX.Data.BQL.BqlString.Field<docNbr> { }
        #endregion

        #region DocType
        [PXDBString(2, IsFixed = true, InputMask = "")]
        [PXUIField(DisplayName = "Doc Type")]
        public virtual string DocType { get; set; }
        public abstract class docType : PX.Data.BQL.BqlString.Field<docType> { }
        #endregion

        #region InitialSuccess
        [PXDBBool()]
        [PXUIField(DisplayName = "Initial Success")]
        public virtual bool? InitialSuccess { get; set; }
        public abstract class initialSuccess : PX.Data.BQL.BqlBool.Field<initialSuccess> { }
        #endregion

        #region Error
        [PXDBString(InputMask = "")]
        [PXUIField(DisplayName = "Error")]
        public virtual string Error { get; set; }
        public abstract class error : PX.Data.BQL.BqlString.Field<error> { }
        #endregion

        #region cFixed
        [PXDBBool()]
        [PXUIField(DisplayName = "Fixed")]
        public virtual bool? cFixed { get; set; }
        public abstract class cfixed : PX.Data.BQL.BqlBool.Field <cfixed> { }
    #endregion
}
}