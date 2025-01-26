using PX.Data;
using Acumatica.RESTClient.Client;
using System.Linq;
using static Acumatica.RESTClient.ContractBasedApi.ApiClientExtensions;
using Acumatica.RESTClient.ContractBasedApi;

using static Acumatica.RESTClient.AuthApi.AuthApiExtensions;
using PX.Objects.AR;
using PX.Objects.SO;
using PX.Objects.IN;
using Acumatica.Default_24_200_001.Model;

namespace ATS20250125
{
    public class ATSOrderDataMaint : PXGraph<ATSOrderDataMaint>
    {

        public PXAction<ATS20250125.ATSOrder> CreateDocument;

        [PXButton(CommitChanges = true)]
        [PXUIField(DisplayName = "Create Order")]
        protected void createDocument()
        {
            const string SiteURL = "https://hackathon.acumatica.com/Delta/";
            const string Username = "admin";
            const string Password = "team4pickle";
            const string Tenant = null;//"Company";
            const string Branch = null;
            const string Locale = null;

            var client = new ApiClient(SiteURL,
                     ignoreSslErrors: true // this is here to allow testing with self-signed certificates
                    );
            //login
            client.Login(Username, Password, Tenant, Branch, Locale);

            //var Httpclient = new HttpClient();
            //var request = new HttpRequestMessage(HttpMethod.Post, "https://hackathon.acumatica.com/Delta/entity/auth/login");
            //request.Headers.Add("Accept", "application/json");
            //var content = new StringContent("{  \"tenant\": \"Company\", \"name\": \"admin\",  \"password\": \"team4pickle\" \"}", null, "application/json");
            //request.Content = content;
            //var response = await Httpclient.SendAsync(request);
            //response.EnsureSuccessStatusCode();
            ////Console.WriteLine(await response.Content.ReadAsStringAsync());

            ATSOrderDataErrors errorentry = PXGraph.CreateInstance<ATSOrderDataErrors>();
            ATSOrderLog errorlog = new ATSOrderLog();

            var atssetup = PXSelect<ATSMaint>.Select(this);

            var custgraph = PXGraph.CreateInstance<CustomerMaint>();
            var invgraph = PXGraph.CreateInstance<InventoryItemMaint>();
            var soorderentry = PXGraph.CreateInstance<SOOrderEntry>();

            //create order
            foreach (ATSOrder order in DetailsView.Select().ToList())
            {
                SalesOrder salesOrder = new SalesOrder();
                //check if the customer is present
                var customercd = PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.acctCD, Equal<Required<PX.Objects.AR.Customer.acctCD>>>>.Select(custgraph, salesOrder.CustomerID);

                if (customercd.Any())
                {
                    //check if the inventory exists
                    foreach (SalesOrderDetail detail in salesOrder.Details)
                    {
                        var inventorycd = PXSelect<InventoryItem, Where<InventoryItem.inventoryCD, Equal<Required<InventoryItem.inventoryCD>>>>.Select(invgraph, detail.InventoryID);

                        if (!inventorycd.Any())
                        {
                            //request = new HttpRequestMessage(HttpMethod.Put, "https://hackathon.acumatica.com/Delta/entity/DEFAULT/24.200.001/SalesOrder");
                            //request.Headers.Add("Accept", "application/json");
                            //var ordercontent = new StringContent(order.Json, null, "application/json");
                            //request.Content = content;
                            //response = await Httpclient.SendAsync(request);
                            //response.EnsureSuccessStatusCode();
                            ////Console.WriteLine(await response.Content.ReadAsStringAsync());
                            try
                            {
                                var so = client.Put(salesOrder, expand: "Details");
                                //so = client.GetById<SalesOrder>(so.ID, select: "Status");
                                if (so.OrderNbr != null)
                                {
                                    order.Success = true;
                                    DetailsView.Update(order);
                                }
                            }
                            catch(ApiException ex)
                            {
                                order.Success = false;
                                DetailsView.Update(order);
                                //Write ATSOrderLog
                                
                            }
                        }
                        else
                        {
                            //update json to have placeholder stock
                            try
                            {
                                InventoryItem item = PXSelect<InventoryItem, Where<InventoryItem.inventoryID, Equal<Required<InventoryItem.inventoryID>>>>.Select(invgraph, ((ATSMaint)atssetup.FirstOrDefault()).InventoryidPlaceholder);
                                detail.InventoryID = item.InventoryCD;
                                var so = client.Put(salesOrder, expand: "Details");
                                //so = client.GetById<SalesOrder>(so.ID, select: "Status");
                                if (so.OrderNbr != null)
                                {
                                    order.Success = true;
                                    DetailsView.Update(order);
                                }
                            }
                            catch (ApiException ex)
                            {
                                order.Success = false;
                                DetailsView.Update(order);
                                //Write ATSOrderLog

                            }
                        }
                    }
                }
                else
                {
                    //update json to have placeholder customer
                    try
                    {
                        PX.Objects.AR.Customer cust = PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Select(custgraph, ((ATSMaint)atssetup.FirstOrDefault()).CustomerPlaceholder);

                        salesOrder.CustomerID = cust.AcctCD;
                        var so = client.Put(salesOrder, expand: "Details");
                        //so = client.GetById<SalesOrder>(so.ID, select: "Status");
                        if (so.OrderNbr != null)
                        {
                            order.Success = true;
                            DetailsView.Update(order);
                        }
                    }
                    catch (ApiException ex)
                    {
                        order.Success = false;
                        DetailsView.Update(order);
                        //Write ATSOrderLog

                    }
                }
            }

            //logout
            //request = new HttpRequestMessage(HttpMethod.Post, "https://hackathon.acumatica.com/Delta/entity/auth/logout");
            //return Httpclient.SendAsync(request);
            client.TryLogout();
        }



        public PXSave<ATSOrder> Save;
        public PXCancel<ATSOrder> Cancel;


        public PXSelect<ATSOrder> MasterView;
        public PXSelect<ATSOrder> DetailsView;



    }
}