using System;
using PX.Data;

namespace ATS20250125
{
  public class ATSSetup : PXGraph<ATSSetup>
  {

    public PXSave<ATSMaint> Save;
    public PXCancel<ATSMaint> Cancel;


    public PXSelect<ATSMaint> setup;
    
    
    public virtual void ATSMaint_Module_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
    {
       var setup = (ATSMaint) e.Row;
       if(setup.Module == "SO")
       {
          PXUIFieldAttribute.SetVisible<ATSMaint.vendorPlaceholder>(sender, setup, false);
          PXUIFieldAttribute.SetVisible<ATSMaint.customerPlaceholder>(sender, setup, true);
          PXUIFieldAttribute.SetVisible<ATSMaint.inventoryidPlaceholder>(sender, setup, true);
       }
      else if(setup.Module == "PO")
       {
          PXUIFieldAttribute.SetVisible<ATSMaint.vendorPlaceholder>(sender, setup, true);
          PXUIFieldAttribute.SetVisible<ATSMaint.customerPlaceholder>(sender, setup, false);
          PXUIFieldAttribute.SetVisible<ATSMaint.inventoryidPlaceholder>(sender, setup, false);
       }
    }


  }
}