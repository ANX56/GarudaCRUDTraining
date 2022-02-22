using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Garuda_CRUD
{
    public partial class Employee : System.Web.UI.Page
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

			SqlCommand sc = new SqlCommand("employeeSelect", conect);
			sc.CommandType = CommandType.StoredProcedure;

			conect.Open();
			SqlDataReader sdr = sc.ExecuteReader();
			dt.Load(sdr);
			conect.Close();

			p_SetViewState();

			Grid1.DataSource = dt;
			Grid1.DataBind();
		}

		private void p_search()
		{
			if (txtSearch.Text.Trim() == "")
			{
				Response.Write("Masukkan kata yang ingin dicari!");
			}
			else
			{
				dt = new DataTable();
				SqlCommand data_sort = new SqlCommand("employeeSearch", conect);
				data_sort.CommandType = CommandType.StoredProcedure;

				data_sort.Parameters.Add("search", SqlDbType.VarChar);
				data_sort.Parameters["search"].Value = txtSearch.Text;
				data_sort.Parameters.Add("stat", SqlDbType.VarChar);
				data_sort.Parameters["stat"].Value = Grid_Sort.SelectedValue;

				conect.Open();
				SqlDataReader sdr = data_sort.ExecuteReader();
				dt.Load(sdr);
				conect.Close();

				p_SetViewState();

				Grid1.DataSource = dt;
				Grid1.DataBind();
			}
		}

		private void p_data()
		{

			if (String.IsNullOrEmpty(txtEmployeeID.Text))
			{
				Response.Write("Masukkan ID Employee!");
			}
			else if (String.IsNullOrEmpty(txtEmployeeName.Text))
			{
				Response.Write("Masukkan Nama Employee!.");
			}
			else if (ddlGender.SelectedValue == "-1")
			{
				Response.Write("Pilih Gender!");
			}
			else if (String.IsNullOrEmpty(txtBirthDate.Text))
			{
				Response.Write("Masukkan Birth Date!");
			}
			else if (String.IsNullOrEmpty(txtAddress.Text))
			{
				Response.Write("Masukkan Alamat Employee!");
			}
			else if (String.IsNullOrEmpty(txtJoinDate.Text))
			{
				Response.Write("Masukkan Join Date!");
			}
			else if (String.IsNullOrEmpty(txtSupervisorID.Text))
			{
				Response.Write("Masukkan ID Supervisor!");
			}
			else
			{
				DateTime now = DateTime.Now;
				DateTime birth = DateTime.Parse(txtBirthDate.Text);
				TimeSpan diff = now.Subtract(birth);

				string employee = txtEmployeeID.Text.Substring(0, 3);
				string lastemp = txtEmployeeID.Text.Substring(3, 3);
				bool empvalid = true;
				foreach (Char c in lastemp.ToCharArray())
				{
					if (Char.IsLetter(c))
					{
						empvalid = false;
						break;
					}
				}
				string supervisor = txtSupervisorID.Text.Substring(0, 3);
				string lastsvp = txtSupervisorID.Text.Substring(3, 3);
				bool spvvalid = true;
				foreach (Char c in lastsvp.ToCharArray())
				{
					if (Char.IsLetter(c))
					{
						spvvalid = false;
						break;
					}
				}

				if (employee != "EMP")
				{
					Response.Write("Employee ID harus diawali dengan 'EMP'!");
				}
				else if (empvalid == false)
				{
					Response.Write("3 digit terakhir ID Employee harus angka!");
				}
				else if (diff.TotalDays/365.25 < 17)
				{
					Response.Write("Birth Date harus di atas 17 tahun!");
				}
				else if (supervisor != "SPV")
				{
					Response.Write("Supervisor ID harus diawali dengan 'SPV'!");
				}
				else if (spvvalid == false)
				{
					Response.Write("3 digit terakhir ID SVP harus angka!");
				}
				else
				{
					conect.Open();
					SqlCommand cek = new SqlCommand("employeeSelectID", conect);
					cek.CommandType = CommandType.StoredProcedure;
					cek.Parameters.Add("id", SqlDbType.VarChar);
					cek.Parameters["id"].Value = txtEmployeeID.Text;
					int check = int.Parse(cek.ExecuteScalar().ToString());
					conect.Close();

					conect.Open();
					SqlCommand set = new SqlCommand("employeeAddOrUpdate", conect);
					set.CommandType = CommandType.StoredProcedure;
					set.Parameters.Add("employeeID", SqlDbType.VarChar);
					set.Parameters["employeeID"].Value = txtEmployeeID.Text;
					set.Parameters.Add("employeeName", SqlDbType.VarChar);
					set.Parameters["employeeName"].Value = txtEmployeeName.Text;
					set.Parameters.Add("gender", SqlDbType.Char);
					set.Parameters["gender"].Value = ddlGender.SelectedValue;
					set.Parameters.Add("birthDate", SqlDbType.Date);
					set.Parameters["birthDate"].Value = txtBirthDate.Text;
					set.Parameters.Add("address", SqlDbType.VarChar);
					set.Parameters["address"].Value = txtAddress.Text;
					set.Parameters.Add("joinDate", SqlDbType.Date);
					set.Parameters["joinDate"].Value = txtJoinDate.Text;
					set.Parameters.Add("supervisorID", SqlDbType.VarChar);
					set.Parameters["supervisorID"].Value = txtSupervisorID.Text;
					set.Parameters.Add("if", SqlDbType.Int);
					set.Parameters["if"].Value = check;
					set.ExecuteNonQuery();
					conect.Close();

					Response.Write("Data Berhasil disimpan");
					Fill_Grid();
					reset();
				}
			}
			
		}

		private void reset()
		{
			txtEmployeeID.Text = null;
			txtEmployeeName.Text = null;
			ddlGender.SelectedValue = "-1";
			txtBirthDate.Text= null;
			txtAddress.Text = null;
			txtJoinDate.Text = null;
			txtSupervisorID.Text = null;
			txtSearch.Text = null;
		}

		protected void Grid1_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			reset();
			if (e.CommandName.Equals("Delete", StringComparison.OrdinalIgnoreCase))
			{
				int iind = Convert.ToInt32(e.CommandArgument);
				Label lblId = (Grid1.Rows[iind].FindControl("lblID") as Label);

				conect.Open();
				SqlCommand cmd = new SqlCommand("employeeDelete", conect);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add("id", SqlDbType.VarChar);
				cmd.Parameters["id"].Value = lblId.Text;
				cmd.ExecuteNonQuery();
				conect.Close();

				Response.Write("Data Berhasil Di hapus");
				Fill_Grid();
			}
			else if (e.CommandName.Equals("Edit", StringComparison.OrdinalIgnoreCase))
			{
				reset();
				int iind = Convert.ToInt32(e.CommandArgument);
				txtEmployeeID.Text = dt.Rows[iind]["EmployeeID"].ToString();
				txtEmployeeName.Text = dt.Rows[iind]["EmployeeName"].ToString();
				if (dt.Rows[iind]["Gender"].ToString().Trim() == "M")
				{
					ddlGender.SelectedValue = "M";
				}
				else
				{
					ddlGender.SelectedValue = "F";
				}
				txtBirthDate.Text = Convert.ToDateTime(dt.Rows[iind]["BirthDate"]).ToString("yyyy-MM-dd");
				txtAddress.Text = dt.Rows[iind]["Address"].ToString();
				txtJoinDate.Text = Convert.ToDateTime(dt.Rows[iind]["JoinDate"]).ToString("yyyy-MM-dd");
				txtSupervisorID.Text = dt.Rows[iind]["SupervisorID"].ToString();
			}
		}

		protected void Grid1_DataBound(object sender, GridViewRowEventArgs e)
		{
			try
			{
				if (e.Row.RowType == DataControlRowType.DataRow)
				{
					Label lblID = (e.Row.FindControl("lblID") as Label);
					object id = DataBinder.Eval(e.Row.DataItem, "EmployeeID");
					lblID.Text = Convert.IsDBNull(id) ? string.Empty : Convert.ToString(id);

					Label lblName = (e.Row.FindControl("lblName") as Label);
					object name = DataBinder.Eval(e.Row.DataItem, "EmployeeName");
					lblName.Text = Convert.IsDBNull(name) ? string.Empty : Convert.ToString(name);
					
					Label lblGender = (e.Row.FindControl("lblGender") as Label);
					object gender = DataBinder.Eval(e.Row.DataItem, "Gender");
					lblGender.Text = Convert.IsDBNull(gender) ? string.Empty : Convert.ToString(gender);

					Label lblBirthDate = (e.Row.FindControl("lblBirthDate") as Label);
					object birthdate = DataBinder.Eval(e.Row.DataItem, "BirthDate");
					string birthform = Convert.ToDateTime(birthdate).ToString("dd/MM/yyyy");
					lblBirthDate.Text = Convert.IsDBNull(birthdate) ? string.Empty : Convert.ToString(birthform);

					Label lblAddress = (e.Row.FindControl("lblAddress") as Label);
					object address = DataBinder.Eval(e.Row.DataItem, "Address");
					lblAddress.Text = Convert.IsDBNull(address) ? string.Empty : Convert.ToString(address);

					Label lblJoinDate = (e.Row.FindControl("lblJoinDate") as Label);
					object joindate = DataBinder.Eval(e.Row.DataItem, "JoinDate");
					string joinform = Convert.ToDateTime(birthdate).ToString("dd/MM/yyyy");
					lblJoinDate.Text = Convert.IsDBNull(birthdate) ? string.Empty : Convert.ToString(joinform);

					Label lblSupervisorID = (e.Row.FindControl("lblSupervisorID") as Label);
					object supervisorid = DataBinder.Eval(e.Row.DataItem, "SupervisorID");
					lblSupervisorID.Text = Convert.IsDBNull(supervisorid) ? string.Empty : Convert.ToString(supervisorid);

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
				p_data();
			}
			catch (Exception ex)
			{
				Response.Write("Error: " + ex.Message);
			}
		}

		protected void search_Click(object sender, EventArgs e)
		{
			try
			{
				p_search();
			}
			catch (Exception ex)
			{
				Response.Write("Error : " + ex.Message);
			}
		}

		protected void cancel_Click(object sender, EventArgs e)
		{
			reset();
			Fill_Grid();
		}

		protected void Grid1_RowEditing(object sender, GridViewEditEventArgs e)
		{

		}

		protected void Grid1_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{

		}
	}
}