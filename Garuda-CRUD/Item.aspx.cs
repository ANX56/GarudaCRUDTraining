using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Garuda_CRUD
{
	public partial class Item : System.Web.UI.Page
	{
		SqlConnection conect = new SqlConnection(@"Data Source=DESKTOP-86LK79A\SQLEXPRESS;Initial Catalog=penjualan;Integrated Security=True");
		DataTable dt = new DataTable();
		
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				Fill_Grid();
			}
			else
			{
				p_GetViewState();
			}
		}

		private void p_GetViewState()
		{
			dt = (DataTable)ViewState["dt"];
		}

		private void p_SetViewState()
		{
			ViewState["dt"] = dt;

		}

		private void Fill_Grid()
		{
			dt = new DataTable();

			SqlCommand sc = new SqlCommand("itemSelect", conect);
			sc.CommandType = CommandType.StoredProcedure;

			conect.Open();
			SqlDataReader sdr = sc.ExecuteReader();
			dt.Load(sdr);
			conect.Close();

			p_SetViewState();
			
			Grid1.DataSource = dt;
			Grid1.DataBind();
		}
		
		private void p_sort()
		{
			dt = new DataTable();

			SqlCommand data_sort = new SqlCommand("itemSearch", conect);
			data_sort.CommandType = CommandType.StoredProcedure;
			data_sort.Parameters.Add("stat", SqlDbType.VarChar);
			data_sort.Parameters["stat"].Value = Grid_Sort.SelectedValue;

			data_sort.Parameters.Add("search", SqlDbType.VarChar);
			data_sort.Parameters["search"].Value = searchtxt.Text;

			conect.Open();
			SqlDataReader sdr = data_sort.ExecuteReader();
			dt.Load(sdr);
			conect.Close();

			p_SetViewState();

			Grid1.DataSource = dt;
			Grid1.DataBind();
		}

		private void p_Data()
		{
			DateTime expiry = DateTime.Now;
			DateTime to = DateTime.Parse(Exptxt.Text);
			double Totaldays = (to - expiry).TotalDays;
			string ITM_format = IDtxt.Text.Substring(0, 3);

			string last3digit = IDtxt.Text.Substring(3, 3);
			bool isvalid = true;

			foreach(Char c in last3digit.ToCharArray())
			{
				if (Char.IsLetter(c))
				{
					isvalid = false;
					break;
				}
			}

			if (String.IsNullOrEmpty(IDtxt.Text))
			{
				Response.Write("Masukkan ID Item!.");
			}
			else if (ITM_format != "ITM")
			{
				Response.Write("Item ID harus diawali dengan ITM");
			}
			else if (String.IsNullOrEmpty(Nametxt.Text))
			{
				Response.Write("Masukkan Nama Item!.");
			}
			else if (Typetxt.SelectedValue == "-1")
			{
				Response.Write("Masukkan Type Item!.");
			}
			else if (Brandtxt.SelectedValue == "-1")
			{
				Response.Write("Masukkan Nama Brand!.");
			}
			else if (String.IsNullOrEmpty(Exptxt.Text))
			{
				Response.Write("Masukkan Expired Date!.");
			}
			else if (Totaldays <= 10)
			{
				Response.Write("Expired Date harus diatas 10 Hari");
			}
			else if (String.IsNullOrEmpty(Stocktxt.Text))
			{
				Response.Write("Masukkan Jumlah Barang!.");
			}
			else if(isvalid == false)
			{
				Response.Write("3 digit terakhir  dari id harus angka");
			}
			else
			{
				conect.Open();
				SqlCommand cek = new SqlCommand("itemSelectID", conect);
				cek.CommandType = CommandType.StoredProcedure;
				cek.Parameters.Add("itemID", SqlDbType.VarChar);
				cek.Parameters["itemID"].Value = IDtxt.Text;
				int check = int.Parse(cek.ExecuteScalar().ToString());
				conect.Close();

				conect.Open();

				SqlCommand set = new SqlCommand("itemAddOrUpdate", conect);
				set.CommandType = CommandType.StoredProcedure;

				set.Parameters.Add("cek", SqlDbType.Int);
				set.Parameters["cek"].Value = check;

				set.Parameters.Add("itemID", SqlDbType.VarChar);
				set.Parameters["itemID"].Value = IDtxt.Text;

				set.Parameters.Add("itemName", SqlDbType.VarChar);
				set.Parameters["itemName"].Value = Nametxt.Text;

				set.Parameters.Add("itemType", SqlDbType.VarChar);
				set.Parameters["itemType"].Value = Typetxt.SelectedValue;

				set.Parameters.Add("brandName", SqlDbType.VarChar);
				set.Parameters["brandName"].Value = Brandtxt.SelectedValue;

				set.Parameters.Add("expiredDate", SqlDbType.DateTime);
				set.Parameters["expiredDate"].Value = Exptxt.Text;

				set.Parameters.Add("stockQty", SqlDbType.Decimal);
				set.Parameters["stockQty"].Value = Stocktxt.Text;

				set.ExecuteNonQuery();
				conect.Close();

				Response.Write("Data Berhasil di Input");
				Fill_Grid();
				reset();
			}
		}
		
		private void reset()
		{
			IDtxt.Text = null;
			Nametxt.Text = null;
			Typetxt.SelectedValue = "-1";
			Brandtxt.SelectedValue = "-1";
			Exptxt.Text = null;
			Stocktxt.Text = null;
			searchtxt.Text = null;
		}

		protected void Grid1_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			reset();
			if (e.CommandName.Equals("Delete", StringComparison.OrdinalIgnoreCase))
			{
				int iind = Convert.ToInt32(e.CommandArgument);
				Label lblId = (Grid1.Rows[iind].FindControl("lblid") as Label);

				conect.Open();
				SqlCommand cmd = new SqlCommand("DeleteItem", conect);
				cmd.CommandType = CommandType.StoredProcedure;

				cmd.Parameters.Add("itemID", SqlDbType.VarChar);
				cmd.Parameters["itemID"].Value = lblId.Text;

				cmd.ExecuteNonQuery();
				conect.Close();

				Response.Write("Data Berhasil Di hapus");
				Fill_Grid();
			}
			else if (e.CommandName.Equals("Edit", StringComparison.OrdinalIgnoreCase))
			{
				reset();
				int iind = Convert.ToInt32(e.CommandArgument);
				IDtxt.Text = dt.Rows[iind]["ItemID"].ToString();
				Nametxt.Text = dt.Rows[iind]["ItemName"].ToString();
				Stocktxt.Text = dt.Rows[iind]["StockQty"].ToString();
				Exptxt.Text = Convert.ToDateTime(dt.Rows[iind]["ExpiredDate"]).ToString("yyyy-MM-dd");

				if (dt.Rows[iind]["BrandName"].ToString().Trim() == "Suntory")
				{
					Brandtxt.SelectedValue = "Suntory";
				}
				else
				{
					Brandtxt.SelectedValue = "Garuda Food";
				}

				if (dt.Rows[iind]["ItemType"].ToString().Trim() == "Drink")
				{
					Typetxt.SelectedValue = "Drink";
				}
				else
				{
					Typetxt.SelectedValue = "Snacks";
				}
			}
		}

		protected void Grid1_DataBound(object sender, GridViewRowEventArgs e)
		{
			try
			{
				if (e.Row.RowType == DataControlRowType.DataRow)
				{
					Label lblID = (e.Row.FindControl("lblid") as Label);
					object id = DataBinder.Eval(e.Row.DataItem, "ItemID");
					lblID.Text = Convert.IsDBNull(id) ? string.Empty : Convert.ToString(id);

					Label lblName = (e.Row.FindControl("lblName") as Label);
					object name = DataBinder.Eval(e.Row.DataItem, "ItemName");
					lblName.Text = Convert.IsDBNull(name) ? string.Empty : Convert.ToString(name);
					
					Label lblType = (e.Row.FindControl("lblType") as Label);
					object type = DataBinder.Eval(e.Row.DataItem, "ItemType");
					lblType.Text = Convert.IsDBNull(type) ? string.Empty : Convert.ToString(type);

					Label lblBrand = (e.Row.FindControl("lblBrand") as Label);
					object brand = DataBinder.Eval(e.Row.DataItem, "BrandName");
					lblBrand.Text = Convert.IsDBNull(brand) ? string.Empty : Convert.ToString(brand);

					Label lblExp = (e.Row.FindControl("lblExp") as Label);
					object exp = DataBinder.Eval(e.Row.DataItem, "ExpiredDate");
					string DateTime = Convert.ToDateTime(exp).ToString("dd/MM/yyyy");
					lblExp.Text = Convert.IsDBNull(exp) ? string.Empty : Convert.ToString(DateTime);

					Label lblQty = (e.Row.FindControl("lblQty") as Label);
					object Qty = DataBinder.Eval(e.Row.DataItem, "StockQty");
					lblQty.Text = Convert.IsDBNull(Qty) ? string.Empty : Convert.ToString(Qty);
				}
			}
			catch (Exception)
			{

			}
		}

		protected void save_Click(object sender, EventArgs e)
		{
			try
			{
				p_Data(); 
			}
			catch (Exception ex)
			{
				Response.Write("Error" + ex.Message);
			}
		}

		protected void search_Click(object sender, EventArgs e)
		{
			try
			{
				p_sort();
			}
			catch (Exception ex)
			{
				Response.Write("Error : " + ex.Message);
			}
		}

		protected void cancel_Click(object sender, EventArgs e)
		{
			reset();
		}

		protected void Grid1_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{

		}

		protected void Grid1_RowEditing(object sender, GridViewEditEventArgs e)
		{

		}

		protected void Grid_Sort_SelectedIndexChanged(object sender, EventArgs e)
		{
			p_sort();
		}
	}
}