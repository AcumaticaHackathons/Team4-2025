using System;
using PX.Objects.IN;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.CR;
using PX.Objects.AR;


namespace ATS20250125
{
    [Serializable]
    [PXCacheName("ATSMaint")]
    public class ATSMaint : PXBqlTable, IBqlTable
    {
        #region Module
        [PXDBString(2, IsKey = true)]
        [PXUIField(DisplayName = "Module")]
        [PXStringList(
            new string[] { "SO", "PO", "QT" },
            new string[] { "Sales Order", "Purchase Order", "Quote" })
        ]
        public virtual string Module { get; set; }
        public abstract class module : PX.Data.BQL.BqlString.Field<module> { }
        #endregion

        #region CustomerPlaceholder
        [PXDBInt()]
        //[PXSelector(typeof(BAccount.bAccountID),
        //        typeof(BAccount.acctName),
        //        DescriptionField = typeof(BAccount.acctName))]
        [Customer(typeof(Search<BAccountR.bAccountID>), Visibility = PXUIVisibility.SelectorVisible, DescriptionField = typeof(Customer.acctName))]
        [PXUIField(DisplayName = "Customer Placeholder")]
        public virtual int? CustomerPlaceholder { get; set; }
        public abstract class customerPlaceholder : PX.Data.BQL.BqlInt.Field<customerPlaceholder> { }
        #endregion

        #region InventoryidPlaceholder
        [PXDBInt]
        [PXDefault]
        [PXUIField(DisplayName = "InventoryidPlaceholder")]
        [PXSelector(typeof(Search<InventoryItem.inventoryID,
                        Where<InventoryItem.inventoryID, IsNotNull>>),
                    DescriptionField = typeof(InventoryItem.inventoryCD),
                    CacheGlobal = true,
                    ValidateValue = false,
                    SubstituteKey = typeof(InventoryItem.inventoryCD))]
        public virtual int? InventoryidPlaceholder { get; set; }
        public abstract class inventoryidPlaceholder : PX.Data.BQL.BqlInt.Field<inventoryidPlaceholder> { }
        #endregion

        #region VendorPlaceholder
        [PXDBInt()]
        [PXUIField(DisplayName = "Vendor Placeholder")]
        [PXSelector(typeof(Vendor.bAccountID),
            typeof(Vendor.acctName),
            DescriptionField = typeof(Vendor.acctName))]
        public virtual int? VendorPlaceholder { get; set; }
        public abstract class vendorPlaceholder : PX.Data.BQL.BqlInt.Field<vendorPlaceholder> { }
        #endregion
    }
}