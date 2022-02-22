<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="employeePage.aspx.cs" Inherits="Garuda_CRUD.Employee" %>

<!DOCTYPE html>

<script src="Scripts/jquery-3.0.0.min.js"></script>
<script src="Scripts/bootstrap.min.js"></script>
<script src="Scripts/popper.min.js"></script>
<link href="Content/bootstrap.min.css" rel="stylesheet"/>
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title></title>
	<style type="text/css">
		.auto-style1 {
			width: 100%;
		}
		.auto-style2 {
            width: 160px;
        }
		.auto-style3 {
			width: 160px;
			height: 29px;
		}
		.auto-style4 {
			height: 29px;
			width: 275px;
		}
		.auto-style5 {
            width: 275px;
        }
		.uppercase
		{
			text-transform: uppercase;
		}
	</style>
	<script lang="Javascript">
		function isNumberKey(evt)
		{
			var charCode = (evt.which) ? evt.which : event.keyCode
			if (charCode > 31 && (charCode < 48 || charCode > 57))
				return false;
			return true;
		}
    </script>
</head>
<body>
    <nav class="navbar navbar-expand-lg navbar-light bg-dark">
		<a class="navbar-brand text-light" href="index.aspx">Home</a>
		<button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
			<span class="navbar-toggler-icon"></span>
		</button>
		<div class="collapse navbar-collapse" id="navbarSupportedContent">
			<ul class="navbar-nav mr-auto">
				<li class="nav-item active">
					<a class="nav-link text-light" href="itemPage.aspx">Item Form <span class="sr-only">(current)</span></a>
				</li>
				<li class="nav-item active">
					<a class="nav-link text-light" href="employeePage.aspx">Employee Form <span class="sr-only">(current)</span></a>
				</li>
			</ul>
		</div>
	</nav>

    <form id="form1" runat="server">
		<div>
			<h1>Employee</h1>
		</div>
		<div class="container">
			<table class="auto-style1">
				<tr>
					<td class="auto-style2">Employee ID</td>
					<td class="auto-style5">
						<asp:TextBox ID="txtEmployeeID" runat="server" MaxLength="6" PlaceHolder="EMPxxx" CssClass="text-uppercase" Width="150px"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td class="auto-style2">Employee Name</td>
					<td class="auto-style5">
						<asp:TextBox ID="txtEmployeeName" runat="server" Width="200px"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td class="auto-style3">Gender</td>
					<td class="auto-style4">
						<asp:DropDownList ID="ddlGender" runat="server" Width="150px">
							<asp:ListItem Value="-1">-Gender-</asp:ListItem>
							<asp:ListItem Value="M">Male</asp:ListItem>
							<asp:ListItem Value="F">Female</asp:ListItem>
						</asp:DropDownList>
					</td>
				</tr>
				<tr>
					<td class="auto-style2">Birth Date</td>
					<td class="auto-style5">
						<asp:TextBox ID="txtBirthDate" runat="server" TextMode="Date" Width="200px"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td class="auto-style2">Address</td>
					<td class="auto-style5">
						<asp:TextBox ID="txtAddress" runat="server" Width="200px" TextMode="MultiLine"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td class="auto-style2">Join Date</td>
					<td class="auto-style5">
						<asp:TextBox ID="txtJoinDate" runat="server" TextMode="Date" Width="200px"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td class="auto-style2">Supervisor ID</td>
					<td class="auto-style5">
						<asp:TextBox ID="txtSupervisorID" runat="server" MaxLength="6" PlaceHolder="SPVxxx" CssClass="text-uppercase" Width="150px"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td class="auto-style2">&nbsp;</td>
					<td class="auto-style5">
						<asp:Button ID="save" runat="server" Text="Save" Width="100px" OnClick="save_Click" /> &emsp;
						<asp:Button ID="cancel" runat="server" Text="Cancel" Width="100px" OnClick="cancel_Click" OnClientClick="return confirm('Apakah anda ingin membatalkan?')"/>
					</td>
				</tr>
		<tr>
			<td class="auto-style2">&nbsp;</td>
			<td class="auto-style5">
				<asp:RadioButtonList ID="Grid_Sort" runat="server" RepeatDirection="Horizontal" Width="268px">
					<asp:ListItem Value ="asc" Selected="True">Ascending</asp:ListItem>
					<asp:ListItem Value ="desc">Descending</asp:ListItem>
				</asp:RadioButtonList>
			</td>
			<td>
				<asp:TextBox ID="txtSearch" runat="server" Width="150px"></asp:TextBox> &emsp;
				<asp:Button ID="btnSearch" runat="server" Text="Search" Width="109px" OnClick="search_Click" />
			</td>
		</tr>
	</table>
			<asp:GridView ID="Grid1" runat="server" AutoGenerateColumns="False" CellPadding="3" HorizontalAlign="Center" Width="100%" style="margin-bottom: 0px" OnRowCommand="Grid1_RowCommand" OnRowDataBound="Grid1_DataBound" OnRowEditing="Grid1_RowEditing" OnRowDeleting="Grid1_RowDeleting">
							<Columns>
								<asp:TemplateField HeaderText="Employee ID">
									<ItemStyle HorizontalAlign="Left" Wrap="False" VerticalAlign="Middle"/>
									<ItemTemplate>
										<div>
											<asp:Label ID="lblID" runat="server" Height="20px" Width="100px"/>
										</div>
									</ItemTemplate>
								</asp:TemplateField>
								
								<asp:TemplateField HeaderText="Employee Name">
									<ItemStyle HorizontalAlign="Left" Wrap="False" VerticalAlign="Middle"/>
									<ItemTemplate>
										<div >
											<asp:Label ID="lblName" runat="server" Height="20px" Width="150px"/>
										</div>
									</ItemTemplate>
								</asp:TemplateField>

								<asp:TemplateField HeaderText="Gender">
									<ItemStyle HorizontalAlign="Left" Wrap="False" VerticalAlign="Middle"/>
									<ItemTemplate>
										<div >
											<asp:Label ID="lblGender" runat="server" Height="20px" Width="50px"/>
										</div>
									</ItemTemplate>
								</asp:TemplateField>

								<asp:TemplateField HeaderText="Birth Date">
									<ItemStyle HorizontalAlign="Left" Wrap="False" VerticalAlign="Middle"/>
									<ItemTemplate>
										<div >
											<asp:Label ID="lblBirthDate" runat="server" Height="20px" Width="100px"/>
										</div>
									</ItemTemplate>
								</asp:TemplateField>
								
								<asp:TemplateField HeaderText="Address">
									<ItemStyle HorizontalAlign="Left" Wrap="False" VerticalAlign="Middle"/>
									<ItemTemplate>
										<div >
											<asp:Label ID="lblAddress" runat="server" Height="20px" Width="150px"/>
										</div>
									</ItemTemplate>
								</asp:TemplateField>

								<asp:TemplateField HeaderText="Join Date">
									<ControlStyle Width="100px" />
									<ItemTemplate>
										<asp:Label ID="lblJoinDate" runat="server" Height="20px" Width="100px"/>
										</div>
									</ItemTemplate>
								</asp:TemplateField>

								<asp:TemplateField HeaderText="Supervisor ID">
									<ControlStyle Width="100px" />
									<ItemTemplate>
										<asp:Label ID="lblSupervisorID" runat="server" Height="20px" Width="100px"/>
										</div>
									</ItemTemplate>
								</asp:TemplateField>

								<asp:TemplateField>
									<ItemTemplate>
										<asp:ImageButton Text="Edit" ID="edit" CommandName="Edit" CommandArgument='<%# Container.DataItemIndex %>' ImageUrl="Edit.png" runat="server"/>
									</ItemTemplate>
								</asp:TemplateField>

								<asp:TemplateField>
									<ItemTemplate>
										<asp:ImageButton Text="Delete" ID="delete" CommandName="Delete" CommandArgument='<%# Container.DataItemIndex %>' ImageUrl="Delete.png" runat="server" OnClientClick="return confirm('Apakah anda ingin menghapus data?')"/>
									</ItemTemplate>
								</asp:TemplateField>
							</Columns>
						</asp:GridView>
		</div>
	</form>
</body>
</html>
