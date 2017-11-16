using PreFinal.App_Code;
using PreFinal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PreFinal.Controllers
{
    public class TitlesController : Controller
    {
        // GET: Titles
        public ActionResult Index()
        {
            List<TitlesModel> list = new List<TitlesModel>();
            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"SELECT t.titleID, p.pubName, a.authorLN, a.authorFN, t.titleName, t.titlePrice, t.titlePubDate, t.titleNotes
                                    FROM titles t
                                    INNER JOIN publishers p ON t.pubID = p.pubID
                                    INNER JOIN authors a ON t.authorID = a.authorID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        using (DataTable dt = new DataTable())
                        {
                            dt.Load(dr);

                            foreach (DataRow row in dt.Rows)
                            {
                                var title = new TitlesModel();
                                title.titleID = Convert.ToInt32(row["titleID"].ToString());
                                title.pubName = row["pubName"].ToString();
                                title.authorName = row["authorLN"].ToString() + " " + row["authorFN"].ToString();
                                title.titleName = row["titleName"].ToString();
                                title.titlePrice = row["titlePrice"].ToString();
                                title.titlePubDate = DateTime.Parse(row["titlePubDate"].ToString());
                                title.titleNotes = row["titleNotes"].ToString();
                                list.Add(title);
                            }
                        }
                    }
                }
            }
                return View(list);
        }

        public List<SelectListItem> GetpubNames()
        {
            List<SelectListItem> items = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"SELECT pubID, pubName FROM publishers ORDER BY pubName";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            items.Add(new SelectListItem
                            {
                                Text = dr["pubName"].ToString(),
                                Value = dr["pubID"].ToString()
                            });
                        }
                    }
                }
            }
            return items;
        }

        public List<SelectListItem> GetauthorNames()
        {
            List<SelectListItem> items = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"SELECT authorID, authorLN, authorFN FROM authors ORDER BY authorLN";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            items.Add(new SelectListItem
                            {
                                Text = dr["authorLN"].ToString() + ", " + dr["authorFN"].ToString(),
                                Value = dr["authorID"].ToString()
                            });
                        }
                    }
                }
            }
            return items;
        }

        public ActionResult Add()
        {
            TitlesModel title = new TitlesModel();
            title.pubNames = GetpubNames();
            title.authorNames = GetauthorNames();
            return View(title);
        }

        [HttpPost]
        public ActionResult Add(TitlesModel title)
        {
            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"INSERT INTO titles VALUES
                    (@pubID, @authorID, @titleName, @titlePrice, @titlePubDate, @titleNotes)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@pubID", title.pubID);
                    cmd.Parameters.AddWithValue("@authorID", title.authorID);
                    cmd.Parameters.AddWithValue("@titleName", title.titleName);
                    cmd.Parameters.AddWithValue("@titlePrice", title.titlePrice);
                    cmd.Parameters.AddWithValue("@titlePubDate", title.titlePubDate);
                    cmd.Parameters.AddWithValue("@titleNotes", title.titleNotes);
                    cmd.ExecuteNonQuery();

                    return RedirectToAction("Index");
                }
            }
        }
    }
}