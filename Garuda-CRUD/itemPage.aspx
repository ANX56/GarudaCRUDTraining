<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="itemPage.aspx.cs" Inherits="Garuda_CRUD.itemPage" %>

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
			width: 201px;
		}
		.auto-style3 {
			width: 201px;
			height: 29px;
		}
		.auto-style4 {
			height: 29px;
			width: 352px;
		}
		.auto-style5 {
			width: 352px;
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
			<h1>Item</h1>
		</div>
		<div class="container">
			<table class="auto-style1">
		<tr>
			<td class="auto-style2">Item ID</td>
			<td class="auto-style5">
				<asp:TextBox ID="IDtxt" runat="server" MaxLength="6" PlaceHolder="ITM-XXX" CssClass="uppercase"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td class="auto-style2">Item Name</td>
			<td class="auto-style5">
				<asp:TextBox ID="Nametxt" runat="server"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td class="auto-style3">Type</td>
			<td class="auto-style4">
				<asp:DropDownList ID="Typetxt" runat="server" Height="33px" Width="150px">
					<asp:ListItem Value="-1">-Type-</asp:ListItem>
					<asp:ListItem Value="Drink">Drink</asp:ListItem>
					<asp:ListItem Value="Snacks">Snacks</asp:ListItem>
				</asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td class="auto-style2">Brand Name</td>
			<td class="auto-style5">
				<asp:DropDownList ID="Brandtxt" runat="server" Height="33px" Width="150px">
					<asp:ListItem Value="-1">-Brand Name-</asp:ListItem>
					<asp:ListItem Value="Suntory">Suntory</asp:ListItem>
					<asp:ListItem Value="Garuda Food">Garuda Food</asp:ListItem>
				</asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td class="auto-style2">Expired Date</td>
			<td class="auto-style5">
				<asp:TextBox ID="Exptxt" runat="server" TextMode="Date"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td class="auto-style2">Stock Qty</td>
			<td class="auto-style5">
				<asp:TextBox ID="Stocktxt" runat="server" Width="154px" onkeypress="return isNumberKey(event)"></asp:TextBox>
				/ Pcs</td>
		</tr>
		<tr>
			<td class="auto-style2">&nbsp;</td>
			<td class="auto-style5">
				<asp:Button ID="save" runat="server" Text="Save" Width="82px" OnClick="save_Click" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
				<asp:Button ID="cancel" runat="server" Text="Cancel" Width="90px" OnClick="cancel_Click" style="height: 33px" OnClientClick="return confirm('Apakah anda ingin membatalkan?')"/>
			</td>
		</tr>
		<tr>
			<td class="auto-style2">&nbsp;</td>
			<td class="auto-style5">
				<asp:RadioButtonList ID="Grid_Sort" runat="server" Height="36px" RepeatDirection="Horizontal" Width="268px">
					<asp:ListItem Value ="asc" Selected="True">Ascending</asp:ListItem>
					<asp:ListItem Value ="desc">Descending</asp:ListItem>
				</asp:RadioButtonList>
			</td>
			<td>
				<asp:TextBox ID="searchtxt" runat="server" Height="25px" Width="226px"></asp:TextBox>
&nbsp;&nbsp;
				<asp:Button ID="search" runat="server" Text="Search" Width="109px" OnClick="search_Click" />
			</td>
		</tr>
	</table>
			<asp:GridView ID="Grid1" runat="server" AutoGenerateColumns="False" CellPadding="3" HorizontalAlign="Center" Width="100%" style="margin-bottom: 0px" OnRowCommand="Grid1_RowCommand" OnRowDataBound="Grid1_DataBound" OnRowEditing="Grid1_RowEditing" OnRowDeleting="Grid1_RowDeleting">
							<Columns>
								<asp:TemplateField HeaderText="Item ID">
									<ItemStyle HorizontalAlign="Left" Wrap="False" VerticalAlign="Middle"/>
									<ItemTemplate>
										<div>
											<asp:Label ID="lblid" runat="server" Height="20px" Width="50px"/>
										</div>
									</ItemTemplate>
								</asp:TemplateField>
								
								<asp:TemplateField HeaderText="Item Name">
									<ItemStyle HorizontalAlign="Left" Wrap="False" VerticalAlign="Middle"/>
									<ItemTemplate>
										<div >
											<asp:Label ID="lblName" runat="server" Height="20px" Width="200px"/>
										</div>
									</ItemTemplate>
								</asp:TemplateField>

								<asp:TemplateField HeaderText="Item Type">
									<ItemStyle HorizontalAlign="Left" Wrap="False" VerticalAlign="Middle"/>
									<ItemTemplate>
										<div >
											<asp:Label ID="lblType" runat="server" Height="20px" Width="150px"/>
										</div>
									</ItemTemplate>
								</asp:TemplateField>

								<asp:TemplateField HeaderText="Brand Name">
									<ItemStyle HorizontalAlign="Left" Wrap="False" VerticalAlign="Middle"/>
									<ItemTemplate>
										<div >
											<asp:Label ID="lblBrand" runat="server" Height="20px" Width="100px"/>
										</div>
									</ItemTemplate>
								</asp:TemplateField>
								
								<asp:TemplateField HeaderText="Expired">
									<ItemStyle HorizontalAlign="Left" Wrap="False" VerticalAlign="Middle"/>
									<ItemTemplate>
										<div >
											<asp:Label ID="lblExp" runat="server" Height="20px" Width="100px"/>
										</div>
									</ItemTemplate>
								</asp:TemplateField>

								<asp:TemplateField HeaderText="Qty">
									<ControlStyle Width="100px" />
									<ItemTemplate>
										<asp:Label ID="lblQty" runat="server" Height="20px"/>
										</div>
									</ItemTemplate>
								</asp:TemplateField>

								<asp:TemplateField>
									<ItemTemplate>
										<asp:ImageButton Text="Delete" ID="delete" CommandName="Delete" CommandArgument='<%# Container.DataItemIndex %>' ImageUrl="Delete.png" runat="server" OnClientClick="return confirm('Apakah anda ingin menghapus data?')"/>
									</ItemTemplate>
								</asp:TemplateField>

								<asp:TemplateField>
									<ItemTemplate>
										<asp:ImageButton Text="Edit" ID="edit" CommandName="Edit" CommandArgument='<%# Container.DataItemIndex %>' ImageUrl="Edit.png" runat="server"/>
									</ItemTemplate>
								</asp:TemplateField>
							</Columns>
						</asp:GridView>
		</div>
	</form>
</body>
</html>
