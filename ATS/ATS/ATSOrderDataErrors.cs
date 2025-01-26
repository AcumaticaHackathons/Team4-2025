using System;
using PX.Data;

namespace ATS20250125
{
  public class ATSOrderDataErrors : PXGraph<ATSOrderDataErrors>
  {

    public PXSave<ATSOrderLog> Save;
    public PXCancel<ATSOrderLog> Cancel;


    public PXSelect<ATSOrderLog> MasterView;
    public PXSelect<ATSOrderLog> DetailsView;

  }
}