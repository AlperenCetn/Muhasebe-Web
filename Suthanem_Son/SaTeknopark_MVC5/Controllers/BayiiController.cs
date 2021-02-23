using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaTeknopark_MVC5.Controllers
{
    [AuthorizeFilter]
    public class BayiiController : Controller
    {
        // GET: Bayii
        public ActionResult Index()
        {
            using (SqlConnection con2 = new System.Data.SqlClient.SqlConnection(AyarMetot.strcon))
            {

                if (con2.State == ConnectionState.Closed) con2.Open();
                string FirmaID = Session["FirmaID"].ToString();
                string srg = "select ID,ParaBirimi From Cari where FirmaID = " + FirmaID;
                using (SqlCommand csay = new SqlCommand(srg, con2))
                {
                    using (SqlDataReader rdr = csay.ExecuteReader())
                    {

                        while (rdr.Read())
                        {
                            using (SqlConnection conp = new System.Data.SqlClient.SqlConnection(AyarMetot.strcon))
                            {

                                if (conp.State == ConnectionState.Closed) conp.Open();
                                using (SqlCommand calis = new SqlCommand("BakiyeKontrolCari", conp))
                                {
                                    try
                                    {
                                        calis.CommandType = CommandType.StoredProcedure;
                                        calis.Parameters.AddWithValue("@CariID", Convert.ToInt32(rdr["ID"]));
                                        calis.Parameters.AddWithValue("@ParaBirimi", rdr["ParaBirimi"]);
                                        calis.ExecuteNonQuery();
                                    }
                                    catch
                                    { }
                                }
                            }
                        }
                    }
                }
            }
            AyarMetot.Siradaki("", "Bayii", "FirmaKodu", Session["FirmaID"].ToString());
            ViewBag.BayiiKoduSiradaki = AyarMetot.GetNumara;

            return View();
        }
    }
}