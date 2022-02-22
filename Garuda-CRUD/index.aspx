<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Garuda_CRUD.index" %>

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

	<div class="container">
            <div class="text-center mt-5">
                <h1>GarudaFood Web Forms</h1>
                <p class="lead">Basic CRUD Web Form for GarudaFood</p>
            </div>
        </div>
</body>
</html>
